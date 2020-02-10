using System.Windows.Forms;
using TestPipesDLL;

namespace ClientUIForms.Forms
{
   public abstract class PipeGameBaseForm : Form, IHandlesPipeMessages
   {
      /// <summary>
      /// The main chat window to print info to
      /// </summary>
      protected IClientChatWindow ChatWindow { get; set; } = null;
      /// <summary>
      /// PipeMessage sending interface
      /// </summary>
      public IPipeMessageSender PipeSender;
      public bool Exiting { get; set; } = false;
      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="theChatWindow">The chat window to print info to</param>
      /// <param name="thePipeMessageSender">The PipeMessage sending interface</param>
      public PipeGameBaseForm(IClientChatWindow theChatWindow, IPipeMessageSender thePipeMessageSender)
      {
         ChatWindow = theChatWindow;
         PipeSender = thePipeMessageSender;
         // Create the handle so we can make changes to the form before it is shown
         CreateHandle();
         this.FormClosing += OnFormClosing;
      }
      /// <summary>
      /// Event: Triggered when the form is closed (by clicking X button etc)
      /// </summary>
      private void OnFormClosing(object sender, FormClosingEventArgs e)
      {
         // Hide the form and cancel the close event instead. This way it can still be updated and re-opened after "closing"
         this.Hide();
         if (!Exiting)
            e.Cancel = true;
      }

      public abstract void HandleMessage(PipeMessage thePipeMessage);
   }
}