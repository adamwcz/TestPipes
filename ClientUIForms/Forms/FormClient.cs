using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TestPipesDLL;

namespace ClientUIForms.Forms
{
   public partial class FormClient : Form, IClientChatWindow, IHandlesPipeMessages
   {
      #region Fields
      private UserListForm itsUserListForm;
      private ImageSharingForm itsImageShareForm;
      private NumberGameForm itsNumberGameForm;

      public IPipeMessageSender PipeSender;
      #endregion

      #region Constructor
      /// <summary>
      /// Constructor
      /// </summary>
      public FormClient(IPipeMessageSender thePipeMessageSender)
      {
         PipeSender = thePipeMessageSender;

         InitializeComponent();
         this.FormClosing += FormClient_FormClosing;

         // Initialize the pipe connection object
         itsInputTextBox.KeyDown += ItsInput_KeyDown;

         //Initialize the User List form
         itsUserListForm = new UserListForm(this, PipeSender);
         itsUserListForm.FormClosing += ItsUserListForm_FormClosing;

         // Initialize the Image Sharing form
         itsImageShareForm = new ImageSharingForm(this, PipeSender);
         itsImageShareForm.FormClosing += ItsImageShareForm_FormClosing;

         // Initialize the Number Game form
         itsNumberGameForm = new NumberGameForm(this, PipeSender);
         itsNumberGameForm.FormClosing += ItsNumberGameForm_FormClosing;
      }
      #endregion


      #region Private
      /// <summary>
      /// Main logic fork for each received PipeMessage type
      /// </summary>
      /// <param name="theMessage">The PipeMessage received from the server</param>
      public void HandleMessage(PipeMessage theMessage)
      {
         switch (theMessage)
         {
            case PipeMessageText aPipeMessageText:
               PrintMessage(aPipeMessageText.Text);
               break;

            case PipeMessageImageShare aPipeMessageImageShare when aPipeMessageImageShare.SharedImage is Image:
               itsImageShareForm.HandleMessage(aPipeMessageImageShare);
               break;

            case PipeMessageNumberGame aPipeMessageNumberGame:
               itsNumberGameForm.HandleMessage(aPipeMessageNumberGame);
               break;

            case PipeMessageUserList aPipeMessageUserList:
            case PipeMessageUserEntersWorld aPipeMessageUserEnters:
            case PipeMessageUserDisconnection aPipeMessageDisconnection:
            case PipeMessageUserRenamed aPipeMessageUserRenamed:
               itsUserListForm.HandleMessage(theMessage);
               break;

            case PipeMessageUserLogin aLogin:
               if (aLogin.Success)
               {
                  Enabled = true;

                  this?.Invoke(new MethodInvoker(delegate
                  {
                     itsLabelMyName.Text = "Name: " + aLogin.Name;
                  }));
               }
               break;
         }
      }
      #endregion

      #region Public
      public void SetUserName(string theName)
      {
         this?.Invoke(new MethodInvoker(delegate
         {
            itsLabelMyName.Text = "Name: " + theName;
         }));
      }
      #endregion

      #region IClientMainChatWindow
      /// <summary>
      /// Adds a line of text to the chat window
      /// </summary>
      /// <param name="theText">Text to be printed</param>
      public void PrintMessage(string theText)
      {
         itsOutput?.Invoke(new MethodInvoker(delegate
         {
               // Check whether the scrollbar is currently at the bottom
               bool isAtBottom = itsOutput.IsScrollAtBottom();
               // Add the new line of text
               itsOutput.AppendText(theText + "\n");
               // If the scrollbar was at the bottom, keep it at the bottom
               if (isAtBottom)
               itsOutput.ScrollToCaret();
         }));
      }
      /// <summary>
      /// Clear the chat window
      /// </summary>
      public void Clear()
      {
         itsOutput.Clear();
      }
      #endregion

      #region Events
      /// <summary>
      /// Event: Triggered when the user presses down a key in the text box
      /// </summary>
      private void ItsInput_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Enter)
         {
            itsButtonSend.PerformClick();
            e.Handled = true; // Stops the Enter newline from being typed in the text box
         }
      }
      /// <summary>
      /// Event: Triggered when the Send button is clicked
      /// </summary>
      private void SendButton_Click(object sender, EventArgs e)
      {
         if (string.IsNullOrEmpty(itsInputTextBox.Text))
            return;

         PipeSender.SendPipeMessage(itsInputTextBox.Text.ConvertToPipeMessage());

         itsInputTextBox.Clear();
         itsInputTextBox.Focus();
      }
      /// <summary>
      /// Event: Triggered when the Clear button is clicked
      /// </summary>
      private void ClearButton_Click(object sender, EventArgs e)
      {
         Clear();
      }
      /// <summary>
      /// Event: Triggered when the User List menu item is clicked
      /// </summary>
      private void OnClickToolStripMenuItemUserList(object sender, EventArgs e)
      {
         if (itsUserListForm.Visible)
         {
            itsUserListForm.Close();
         }
         else
         {
            itsUserListForm.Show();
            this.userListToolStripMenuItem.Checked = true;
         }
      }
      /// <summary>
      /// Event: Triggered when the Image Sharing menu item is clicked
      /// </summary>
      private void OnClickToolStripMenuItemImageShare(object sender, EventArgs e)
      {
         if (itsImageShareForm.Visible)
         {
            itsImageShareForm.Close();
         }
         else
         {
            itsImageShareForm.Show();
            this.imageShareToolStripMenuItem.Checked = true;
         }
      }
      /// <summary>
      /// Event: Triggered when the Image Sharing menu item is clicked
      /// </summary>
      private void OnClickToolStripMenuItemNumberGame(object sender, EventArgs e)
      {
         if (itsNumberGameForm.Visible)
         {
            itsNumberGameForm.Close();
         }
         else
         {
            itsNumberGameForm.Show();
            this.numberGameToolStripMenuItem.Checked = true;
         }
      }
      /// <summary>
      /// Event: Triggered when the User List form is closing
      /// </summary>
      private void ItsUserListForm_FormClosing(object sender, FormClosingEventArgs e)
      {
         this.userListToolStripMenuItem.Checked = false;
      }
      /// <summary>
      /// Event: Triggered when the Image Sharing form is closing
      /// </summary>
      private void ItsImageShareForm_FormClosing(object sender, FormClosingEventArgs e)
      {
         this.imageShareToolStripMenuItem.Checked = false;
      }
      /// <summary>
      /// Event: Triggered when the Number Game form is closing
      /// </summary>
      private void ItsNumberGameForm_FormClosing(object sender, FormClosingEventArgs e)
      {
         this.numberGameToolStripMenuItem.Checked = false;
      }
      /// <summary>
      /// Event: Triggered when the client is closing
      /// </summary>
      private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
      {
         // Usually the sub windows hide instead of closing, but here we want them to close for real
         itsUserListForm.Exiting = true;
         itsImageShareForm.Exiting = true;
         itsNumberGameForm.Exiting = true;
      }
      #endregion
   }
   public static class Extensions
   {
      #region Get ScrollBar Info
      private struct SCROLLINFO
      {
         public uint cbSize;
         public uint fMask;
         public int nMin;
         public int nMax;
         public uint nPage;
         public int nPos;
         public int nTrackPos;
      }
      [DllImport("user32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      private static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref SCROLLINFO lpsi);
      /// <summary>
      /// Returns true if the control's vertical scrollbar is currently scrolled to the bottom
      /// </summary>
      public static bool IsScrollAtBottom(this Control theControl)
      {
         // Get current scroller posion
         SCROLLINFO si = new SCROLLINFO();
         si.cbSize = (uint)Marshal.SizeOf(si);
         si.fMask = (uint)(0x1 + 0x2 + 0x4 + 0x10);
         GetScrollInfo(theControl.Handle, 1, ref si);

         return si.nPos + si.nPage >= si.nMax;
      }
      #endregion
   }
}