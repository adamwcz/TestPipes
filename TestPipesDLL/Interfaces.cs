using System;

namespace TestPipesDLL
{
   #region Server Interfaces
   /// <summary>
   /// Interface to control a server UI
   /// </summary>
   public interface IServerUI
   {
      /// <summary>
      /// Adds a line of text to the server log window
      /// </summary>
      /// <param name="theText">Text to add to the log</param>
      public void AddLogText(string theText);
      /// <summary>
      /// Adds a user to the user list
      /// </summary>
      /// <param name="theName">Name of the user to add to the user list</param>
      public void UserListAddUser(string theName);
      /// <summary>
      /// Removes a user to the user list
      /// </summary>
      /// <param name="theName">Name of the user to remove from the user list</param>
      public void UserListRemoveUser(string theName);
      /// <summary>
      /// Action: Triggered when the server is exiting.
      /// </summary>
      public Action ServerUIClosing { get; set; }
   }
   #endregion

   #region Client Interfaces
   /// <summary>
   /// Interface to control a client UI
   /// </summary>
   public interface IClientUI : IHandlesPipeMessages
   {
      /// <summary>
      /// The main chat window used to display messages to the client user
      /// </summary>
      public IClientChatWindow ChatWindow { get; }
      /// <summary>
      /// Starts the UI after connecting so the user may begin interacting
      /// </summary>
      /// <param name="thePipeMessageSender">The interface used to send PipeMessages to the server</param>
      public void Start(IPipeMessageSender thePipeMessageSender);
      /// <summary>
      /// Gets connection info from the user: (ServerAddress, PipeName)
      /// </summary>
      /// <returns>(ServerAddress, PipeName)</returns>
      public (string theAddress, string thePipeName) GetConnectionInfo();
      /// <summary>
      /// Gets a Name selection from the user.
      /// </summary>
      public string GetNameInfo();
      /// <summary>
      /// Sets the client's Username
      /// </summary>
      /// <param name="theUserName">The new Username for the client</param>
      public void SetUserName(string theUserName);
      /// <summary>
      /// Action: Triggered when the client is exiting. True to send a disconnect message to server
      /// </summary>
      public Action<bool> OnExiting { get; set; }
   }
   /// <summary>
   /// Interface to control the chat window used by a client UI
   /// </summary>
   public interface IClientChatWindow
   {
      /// <summary>
      /// Prints a message to the chat window
      /// </summary>
      /// <param name="theMessage">The message to print</param>
      public void PrintMessage(string theMessage);
      /// <summary>
      /// Clears the chat window
      /// </summary>
      public void Clear();
   }
   /// <summary>
   /// Interface to control a class which accepts PipeMessage input and decides what to do with it
   /// </summary>
   public interface IHandlesPipeMessages
   {
      /// <summary>
      /// Main logic handling method, reacts to any received PipeMessage and passes it off to the appropriate part of the UI
      /// </summary>
      /// <param name="theMessage">The PipeMessage received from the server</param>
      public void HandleMessage(PipeMessage theMessage);
   }
   /// <summary>
   /// Interface to control sending pipe messages to the server
   /// </summary>
   public interface IPipeMessageSender
   {
      /// <summary>
      /// Action used to send a PipeMessage to the server
      /// </summary>
      public Action<PipeMessage> SendPipeMessage { get; set; }
   }
   #endregion
}