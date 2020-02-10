using System;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using TestPipesDLL;

namespace TestPipesClient
{
   public class InternalPipeClient : IPipeMessageSender
   {
      #region Fields
      private NamedPipeClientStream itsPipeClient;
      private BinaryFormatter itsFormatter;
      private bool itsIsStopping = false;
      private readonly object itsReceiveLock = new object();
      #endregion

      #region Properties
      /// <summary>
      /// The UI being used to control the client
      /// </summary>
      public IClientUI TheUI { get; private set; }
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
      public bool IsConnected => itsPipeClient != null && itsPipeClient.IsConnected;
      /// <summary>
      /// Action used to send a PipeMessage to the server
      /// </summary>
      public Action<PipeMessage> SendPipeMessage { get; set; }
      #endregion

      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="theClientUI">The UI to use</param>
      public InternalPipeClient(IClientUI theClientUI)
      {
         SendPipeMessage += Send;
         TheUI = theClientUI;
         TheUI.OnExiting += Stop;
         // Initialize the BinaryFormatter used to serialize the messages
         itsFormatter = new BinaryFormatter();

         var (anAddress, aPipeName) = TheUI.GetConnectionInfo();
         if (!string.IsNullOrEmpty(anAddress) && !string.IsNullOrEmpty(aPipeName))
         {
            ServerName = anAddress;
            PipeName = aPipeName;

            TheUI.Start(this as IPipeMessageSender);

            // Initialize the NamedPipeClientStream
            itsPipeClient = new NamedPipeClientStream(ServerName, PipeName, PipeDirection.InOut, PipeOptions.Asynchronous);

            // Start a new async Task in which to connect to the server
            Task aClientTask = new Task(ClientThread);
            aClientTask.Start();
         }
      }
      /// <summary>
      /// Asynchronous thread used to receive messages from the server until disconnecting
      /// </summary>
      private async void ClientThread()
      {
         // Wait for initial connection
         await itsPipeClient.ConnectAsync(2000);

         // Receive messages from the server until disconnected
         while (!itsIsStopping && itsPipeClient.IsConnected)
         {
            lock (itsReceiveLock)
            {
               try
               {
                  // Deserialize the PipeMessage
                  PipeMessage aMessage = (PipeMessage)itsFormatter.Deserialize(itsPipeClient);

                  // Invoke the OnPipeMessageRecived event to handle the received message
                  HandleMessage(aMessage);
               }
               catch (Exception exception) { Console.WriteLine("Exception: " + exception.Message); }
            }
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
         if (IsConnected)
         {
            itsFormatter.Serialize(itsPipeClient, theMessage);
         }
         else
         {
            TheUI.ChatWindow.PrintMessage("Not connected to server");
         }
      }
      private void HandleMessage(PipeMessage theMessage)
      {
         switch (theMessage)
         {
            case PipeMessageKicked aPipeMessageKicked:
               Stop(false);
               TheUI.ChatWindow.PrintMessage("[Disconnected: " + aPipeMessageKicked.Reason + "]");
               break;

            case PipeMessageUserRenamed aPipeMessageUserRenamed:
               // If this is the user being renamed, update their local name
               if (ID == aPipeMessageUserRenamed.ID)
               {
                  Username = aPipeMessageUserRenamed.NewName;
                  TheUI.SetUserName(aPipeMessageUserRenamed.NewName);
               }
               break;

            case PipeMessageAssignID aPipeMessageAssignID:
               ID = aPipeMessageAssignID.ID;
               break;

            case PipeMessageUserLogin aLogin:
               if (aLogin.Success)
               {
                  Username = aLogin.Name;
               }
               else
               {
                  string aNameInfo = TheUI.GetNameInfo();
                  if (!string.IsNullOrWhiteSpace(aNameInfo))
                  {
                     aLogin.Name = aNameInfo;
                     Send(aLogin);
                  }
               }
               break;
         }
         TheUI.HandleMessage(theMessage);
      }
   }
}