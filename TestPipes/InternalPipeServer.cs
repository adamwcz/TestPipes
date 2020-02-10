using System;
using System.IO.Pipes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Threading.Tasks;
using TestPipesDLL;

namespace TestPipeServer
{
   public class InternalPipeServer
   {
      private NamedPipeServerStream itsPipeServer;
      private BinaryFormatter itsFormatter = new BinaryFormatter();
      private bool itsIsStopping = false;
      private readonly object itsLock = new object();
      private readonly string itsPipeName;
      private readonly int itsMaxNumberOfServerInstances;
      public int ID { get; private set; }
      public bool IsConnected => itsPipeServer != null && itsPipeServer.IsConnected;

      /// <summary>
      /// Constructor 
      /// </summary>
      protected InternalPipeServer(string thePipeName, int theMaxNumberOfServerInstances, int theServerID, EventHandler<PipeMessageEventArgs> theMessageReceivedEvent)
      {
         itsPipeName = thePipeName;
         itsMaxNumberOfServerInstances = theMaxNumberOfServerInstances;
         ID = theServerID;
         OnPipeMessageReceivedEvent += theMessageReceivedEvent;
         try
         {
            // Initialize the NamedPipeServerStream
            itsPipeServer = new NamedPipeServerStream(itsPipeName, PipeDirection.InOut, itsMaxNumberOfServerInstances,
               PipeTransmissionMode.Byte, PipeOptions.Asynchronous, int.MaxValue, int.MaxValue/*, CreateSystemIoPipeSecurity()*/);
            // Start a new async Task in which to start a server instance
            Task aStartTask = new Task(ServerThread);
            aStartTask.Start();
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
            throw;
         }
      }
      /// <summary>
      /// Creates a security profile for the pipes (Was used in .NET Framework.. No longer needed in Core 3? (Crashes))
      /// </summary>
      private static PipeSecurity CreateSystemIoPipeSecurity()
      {
         PipeSecurity pipe_security = new PipeSecurity();
         PipeAccessRule access_rule = new PipeAccessRule("Everyone", PipeAccessRights.ReadWrite, AccessControlType.Allow);
         pipe_security.AddAccessRule(access_rule);
         PipeAccessRule access_rule2 = new PipeAccessRule("Everyone", PipeAccessRights.FullControl, AccessControlType.Allow);
         pipe_security.AddAccessRule(access_rule2);
         return pipe_security;
      }
      /// <summary>
      /// Asynchronous thread used to receive messages from the client
      /// </summary>
      private async void ServerThread()
      {
         // Wait for a client to connect
         await itsPipeServer.WaitForConnectionAsync();

         // Send the client its ID assignment message as the first communication
         PipeMessageAssignID aPipeMessageAssignID = new PipeMessageAssignID(this.ID);
         Send(aPipeMessageAssignID);

         // Send a blank UserLogin message to prompt their name selection
         PipeMessageUserLogin aPipeMessageLogin = new PipeMessageUserLogin("");
         Send(aPipeMessageLogin);

         // Receive messages from the InternalPipeClient until they disconnect from this InternalPipeServer
         while (!itsIsStopping && itsPipeServer.IsConnected)
         {
            try
            {
               PipeMessage aMessage = (PipeMessage)itsFormatter.Deserialize(itsPipeServer);
               aMessage.ID = ID;
               OnPipeMessageReceivedEvent.Invoke(this, new PipeMessageEventArgs(aMessage));
            }
            catch (Exception theException)
            {
               Console.WriteLine(theException.Message);
            }
         }
      }

      /// <summary>
      /// Sends a PipeMessage to the InternalPipeClient associated with this InternalPipeServer
      /// </summary>
      public void Send(PipeMessage theMessage)
      {
         lock (itsLock)
         {
            if (itsPipeServer.IsConnected)
            {
               if (theMessage.ID == -1)
                  theMessage.ID = this.ID;
               itsFormatter.Serialize(itsPipeServer, theMessage);
            }
            else
            {
               Console.WriteLine("Cannot send message, pipe is not connected");
               //throw new IOException("pipe is not connected");
            }
         }
      }
      public void Stop()
      {
         itsIsStopping = true;
         itsPipeServer.Close();
         OnPipeMessageReceivedEvent = null;
      }

      private event EventHandler<PipeMessageEventArgs> OnPipeMessageReceivedEvent;
   }
   public class Client : InternalPipeServer
   {
      public string Name { get; set; }
      public bool HasEnteredWorld { get; set; } = false;
      public Client(string thePipeName, int theMaxNumberOfServerInstances, int theServerID, EventHandler<PipeMessageEventArgs> theMessageReceivedEvent)
         : base(thePipeName, theMaxNumberOfServerInstances, theServerID, theMessageReceivedEvent)
      {

      }
      new public string ToString() => ID + " | " + Name;
   }
}