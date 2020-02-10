using System;
using System.Windows.Forms;
using TestPipesDLL;
using ClientUIForms.Forms;

namespace ClientUIForms
{
   public class FormsUI : IClientUI
   {
      private FormClient itsFormClient;
      public IClientChatWindow ChatWindow => itsFormClient;
      public Action<bool> OnExiting { get; set; }

      public FormsUI()
      {

      }
      public void Start(IPipeMessageSender thePipeMessageSender)
      {
         itsFormClient = new FormClient(thePipeMessageSender);
         itsFormClient.Show();
         itsFormClient.FormClosing += FormClient_FormClosing;
      }

      private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
      {
         OnExiting(true);
         Application.Exit();
      }

      public void HandleMessage(PipeMessage theMessage)
      {
         itsFormClient.HandleMessage(theMessage);
      }

      public (string theAddress, string thePipeName) GetConnectionInfo()
      {
         ConnectionForm aConnectionForm = new ConnectionForm();
         if (aConnectionForm.ShowDialog() == DialogResult.OK)
         {
            return (theAddress: aConnectionForm.ServerName, thePipeName: aConnectionForm.PipeName);
         }
         return (null, null);
      }

      public string GetNameInfo()
      {
         NameInputForm aNameForm = new NameInputForm();

         bool aNameFormOK = false;
         itsFormClient?.Invoke(new MethodInvoker(delegate
         {
            aNameFormOK = aNameForm.ShowDialog(itsFormClient) == DialogResult.OK;
         }));

         if (aNameFormOK)
         {
            return aNameForm.NameChoice;
         }
         else // If we closed the name form, close the client form
         {
            Application.Exit();
            return null;
         }
      }

      public void SetUserName(string theName)
      {
         itsFormClient.SetUserName(theName);
      }
   }
}