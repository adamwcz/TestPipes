using System;
using System.Windows.Forms;
using TestPipesDLL;

namespace ClientUIForms.Forms
{
   public partial class NumberGameForm : PipeGameBaseForm
   {
      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="theChatWindow">The chat window to print info to</param>
      /// <param name="thePipeMessageSender">The PipeMessage sending interface</param>
      public NumberGameForm(IClientChatWindow theChatWindow, IPipeMessageSender thePipeMessageSender) : base(theChatWindow, thePipeMessageSender)
      {
         InitializeComponent();
      }
      /// <summary>
      /// Event: Triggered when the + button is clicked
      /// </summary>
      private void itsButtonPlus_Click(object sender, EventArgs e)
      {
         PipeMessageNumberGameAdd aMessageAdd1 = new PipeMessageNumberGameAdd(1);
         PipeSender.SendPipeMessage(aMessageAdd1);
      }

      /// <summary>
      /// Event: Triggered when the - button is clicked
      /// </summary>
      private void itsButtonMinus_Click(object sender, EventArgs e)
      {
         PipeMessageNumberGameAdd aMessageSubtract1 = new PipeMessageNumberGameAdd(-1);
         PipeSender.SendPipeMessage(aMessageSubtract1);
      }

      /// <summary>
      /// Handles a received PipeMessage
      /// </summary>
      /// <param name="thePipeMessage">The received PipeMessage</param>
      public override void HandleMessage(PipeMessage thePipeMessage)
      {
         switch (thePipeMessage)
         {
            case PipeMessageNumberGameUpdate aPipeMessageNumberGameUpdate:
               this?.Invoke(new MethodInvoker(delegate
               {
                  itsLabelNumber.Text = aPipeMessageNumberGameUpdate.NumberUpdate.ToString();
               }));
               break;
         }
      }
   }
}