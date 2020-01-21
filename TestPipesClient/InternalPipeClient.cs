using System;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using TestPipesDLL;

namespace TestPipesClient
{
    public class InternalPipeClient
    {
        #region Fields
        private NamedPipeClientStream itsPipeClient;
        private BinaryFormatter itsFormatter;
        private bool itsIsStopping = false;
        private readonly object itsLock = new object();
        private event EventHandler<PipeMessageEventArgs> OnPipeMessageReceivedEvent;
        #endregion

        #region Properties
        /// <summary>
        /// IP Address / Connection info for the server
        /// </summary>
        public string ServerName { get; private set; }
        /// <summary>
        /// Name of the pipe we are connecting to
        /// </summary>
        public string PipeName { get; private set; }
        /// <summary>
        /// Username of the client
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// ID # of the client
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Is the client's PipeStream currently connected to the server?
        /// </summary>
        public bool IsConnected => itsPipeClient.IsConnected;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="theServerName">IP Address / Connection info for the server</param>
        /// <param name="thePipeName">Name of the pipe we are connecting to</param>
        /// <param name="theMessageReceivedEvent">Function to call when a PipeMessage is received</param>
        public InternalPipeClient(string theServerName, string thePipeName, EventHandler<PipeMessageEventArgs> theMessageReceivedEvent)
        {
            // Initialize the BinaryFormatter used to serialize the messages
            itsFormatter = new BinaryFormatter();

            // Constructor Injection
            ServerName = theServerName;
            PipeName = thePipeName;
            OnPipeMessageReceivedEvent += theMessageReceivedEvent;

            // Initialize the NamedPipeClientStream
            itsPipeClient = new NamedPipeClientStream(ServerName, PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);

            // Start a new async Task in which to connect to the server
            Task aClientTask = new Task(ClientThread);
            aClientTask.Start();
        }

        /// <summary>
        /// Asynchronous thread used to receive messages from the server until disconnecting
        /// </summary>
        private async void ClientThread()
        {
            // Wait for initial connection
            await itsPipeClient.ConnectAsync(2000);

            // After connecting, send connected message
            PipeMessageUserConnection aConnectionMessage = new PipeMessageUserConnection();
            itsFormatter.Serialize(itsPipeClient, aConnectionMessage);

            // Receive messages from the server until disconnected
            while (!itsIsStopping && itsPipeClient.IsConnected)
            {
                try
                {
                    // Deserialize the PipeMessage
                    PipeMessage aMessage = (PipeMessage)itsFormatter.Deserialize(itsPipeClient);

                    // Invoke the OnPipeMessageRecived event to handlf the received message
                    OnPipeMessageReceivedEvent.Invoke(this, new PipeMessageEventArgs(aMessage));
                }
                catch (Exception exception) { Console.WriteLine("Exception: " + exception.Message); }
            }
        }
        /// <summary>
        /// Stop the client and disconnect from the server
        /// </summary>
        /// <param name="theSendMessage">Send a disconnection message to the server? If false, we were probably kicked, etc.</param>
        public void Stop(bool theSendMessage = true)
        {
            if (itsIsStopping)
                return;
            if (theSendMessage)
            {
                PipeMessageUserDisconnection aMessageUserDisconnection = new PipeMessageUserDisconnection(Username);
                Send(aMessageUserDisconnection);
            }
            itsIsStopping = true;
            itsPipeClient.Flush();
            itsPipeClient.Close();
        }
        /// <summary>
        /// Send a PipeMessage to the server associated with this client
        /// </summary>
        public void Send(PipeMessage theMessage)
        {
            if (itsPipeClient.IsConnected)
            {
                itsFormatter.Serialize(itsPipeClient, theMessage);
            }
        }
    }
}