using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TestPipesDLL;

namespace TestPipeServer
{
    class ServerManager
    {
        #region Fields
        protected readonly FormServer itsFormServer;
        private readonly string itsPipeName = "testpipe";
        private const int MaxNumberOfServerInstances = 10;
        private int itsNextServerID = 1;
        private readonly object itsReceiveMessageLock = new object();
        private readonly IDictionary<int, Client> itsClients = new ConcurrentDictionary<int, Client>(); // ConcurrentDictionary is thread safe
        #endregion

        #region Properties
        /// <summary>
        /// Enumerable of all clients who are connected and 'logged in'
        /// </summary>
        internal IEnumerable<Client> Clients => itsClients.Values.Where(x => x.IsConnected && x.HasEnteredWorld);
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        protected ServerManager()
        {
            itsFormServer = new FormServer();
            itsFormServer.Show();
            itsFormServer.FormClosing += ItsFormServer_FormClosing;
            WaitForClientConnection();
        }
        #endregion


        private static PipeMessageText ClientTextMessage = new PipeMessageText(string.Empty, string.Empty);
        public void Send(string theMessage, params Client[] theClients)
        {
            ClientTextMessage.Text = theMessage;
            foreach (Client aClient in theClients)
                aClient.Send(ClientTextMessage);
        }
        internal void SendAll(string theMessage) => Send(theMessage, Clients.ToArray());
        public void Send(PipeMessage thePipeMessage, params Client[] theClients)
        {
            foreach (Client aClient in theClients)
                aClient.Send(thePipeMessage);
        }
        internal void SendAll(PipeMessage thePipeMessage)
        {
            foreach (InternalPipeServer server in Clients)
            {
                server.Send(thePipeMessage);
            }
        }
        internal void Logg(string aString) => itsFormServer.AddLine(aString);

        #region Protected
        protected virtual void HandleInput(Client theSender, PipeMessage theMessage)
        {
            switch (theMessage)
            {
                case PipeMessageUserLogin aPipeMessageUserLogin:

                    if (Clients.Any(x => x.Name == aPipeMessageUserLogin.Name))
                    {
                        aPipeMessageUserLogin.Success = false;
                        theSender.Send(aPipeMessageUserLogin);
                    }
                    else // If the user has logged in with a valid name
                    {
                        aPipeMessageUserLogin.Success = true;
                        theSender.HasEnteredWorld = true;
                        theSender.Name = aPipeMessageUserLogin.Name;
                        theSender.Send(aPipeMessageUserLogin); // Tell them they're logged in

                        // Update server window
                        itsFormServer.AddUser(theSender.Name);
                        itsFormServer.AddLine("[" + theSender.Name + " connected]");

                        // Send the list of current users to the new client
                        PipeMessageUserList aUserListMessage = new PipeMessageUserList((from a in Clients
                                                                                        let usr = new User(a.ID, a.Name)
                                                                                        select usr).ToList());
                        theSender.Send(aUserListMessage);

                        PipeMessageNumberGameUpdate aNumberGameUpdateMessage = new PipeMessageNumberGameUpdate(NumberGame.Instance.Number);
                        theSender.Send(aNumberGameUpdateMessage);

                        // Send a connection message to other users
                        PipeMessageUserEntersWorld anEnterMessage = new PipeMessageUserEntersWorld(new User(theSender.ID, theSender.Name));
                        Send(anEnterMessage, theSender.AllClientsExceptMe());
                    }

                    break;

                case PipeMessageUserDisconnection aPipeMessageDisconnection:
                    StopServer(aPipeMessageDisconnection.ID);
                    if (theSender.HasEnteredWorld)
                    {
                        SendAll(aPipeMessageDisconnection);
                        itsFormServer.RemoveUser(aPipeMessageDisconnection.Name);
                        itsFormServer.AddLine("[" + aPipeMessageDisconnection.Name + " disconnected]");
                    }
                    break;


            }
        }
        #endregion

        #region Private - Boring Network Code
        /// <summary>
        /// Starts a new InternalPipeServer that waits for connection
        /// </summary>
        private void WaitForClientConnection()
        {
            Client aClient = new Client(itsPipeName, MaxNumberOfServerInstances, itsNextServerID++, OnPipeMessageReceived);
            itsClients[aClient.ID] = aClient;

            if (itsClients.Count < MaxNumberOfServerInstances)
                WaitForClientConnection();
        }
        /// <summary>
        /// Stops the server that belongs to the given id
        /// </summary>
        private void StopServer(int theServerID)
        {
            InternalPipeServer anInternalServer = itsClients[theServerID];
            anInternalServer.Stop();
            itsClients.Remove(theServerID);
            // If stopping a server while at max number of servers, start waiting for another connection
            if (itsClients.Count == MaxNumberOfServerInstances - 1)
                WaitForClientConnection();
        }
        /// <summary>
        /// Stops the server, disconnecting all clients
        /// </summary>
        private void StopAllServers()
        {
            foreach (InternalPipeServer server in itsClients.Values)
            {
                try
                {
                    server.Stop();
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to stop server " + server.ID);
                }
            }
            itsClients.Clear();
        }
        #endregion

        #region Events
        /// <summary>
        /// Event: Triggered when a message is received from a client
        /// </summary>
        private void OnPipeMessageReceived(object sender, PipeMessageEventArgs e)
        {
            lock (itsReceiveMessageLock)
            {
                if (sender is Client aSender && e.PipeMessage is PipeMessage aMessage)
                {
                    HandleInput(aSender, aMessage);
                }
            }
        }
        /// <summary>
        /// Event: Triggered when the server form is closing (i.e. by pressing the X button)
        /// </summary>
        private void ItsFormServer_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            PipeMessageKicked aKickMessage = new PipeMessageKicked("Server shutting down.");
            foreach (Client client in Clients)
                client.Send(aKickMessage);
        }
        #endregion
    }
}