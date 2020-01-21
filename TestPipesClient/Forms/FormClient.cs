using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TestPipesClient.Forms;
using TestPipesDLL;

namespace TestPipesClient
{
    public partial class FormClient : Form
    {
        #region Fields
        private InternalPipeClient itsPipeClient;
        private UserListForm itsUserListForm;
        private ImageSharingForm itsImageShareForm;
        private NumberGameForm itsNumberGameForm;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public FormClient()
        {
            InitializeComponent();

            ConnectionForm aConnectionForm = new ConnectionForm();
            if (aConnectionForm.ShowDialog(this) == DialogResult.OK)
            {

            }
            else // If we closed the connection form, close the client form
            {
                Load += (s, e) => Close();
                return;
            }


            // Initialize the pipe connection object
            itsPipeClient = new InternalPipeClient(aConnectionForm.ServerName, aConnectionForm.PipeName, ItsPipeClient_OnPipeMessageReceived);
            itsInputTextBox.KeyDown += ItsInput_KeyDown;
            this.FormClosing += FormClient_FormClosing;

            //Initialize the User List form
            itsUserListForm = new UserListForm(this, itsPipeClient);
            itsUserListForm.FormClosing += ItsUserListForm_FormClosing;

            // Initialize the Image Sharing form
            itsImageShareForm = new ImageSharingForm(this, itsPipeClient);
            itsImageShareForm.FormClosing += ItsImageShareForm_FormClosing;

            // Initialize the Number Game form
            itsNumberGameForm = new NumberGameForm(this, itsPipeClient);
            itsNumberGameForm.FormClosing += ItsNumberGameForm_FormClosing;
        }
        #endregion

        #region Private
        /// <summary>
        /// Main logic fork for each received PipeMessage type
        /// </summary>
        /// <param name="theMessage">The PipeMessage received from the server</param>
        private void HandleMessage(PipeMessage theMessage)
        {
            switch (theMessage)
            {
                case PipeMessageText aPipeMessageText:
                    AddChatWindowLine(aPipeMessageText.Text);
                    break;

                case PipeMessageImageShare aPipeMessageImageShare when aPipeMessageImageShare.SharedImage is Image:
                    itsImageShareForm.HandleMessage(aPipeMessageImageShare);
                    break;

                case PipeMessageNumberGame aPipeMessageNumberGame:
                    itsNumberGameForm.HandleMessage(aPipeMessageNumberGame);
                    break;

                case PipeMessageKicked aPipeMessageKicked:
                    AddChatWindowLine("[Disconnected: " + aPipeMessageKicked.Reason + "]");
                    itsPipeClient.Stop(false);
                    break;

                case PipeMessageUserList aPipeMessageUserList:
                case PipeMessageUserEntersWorld aPipeMessageUserEnters:
                case PipeMessageUserDisconnection aPipeMessageDisconnection:
                case PipeMessageUserRenamed aPipeMessageUserRenamed:
                    itsUserListForm.HandleMessage(theMessage);
                    break;

                case PipeMessageAssignID aPipeMessageAssignID:
                    itsPipeClient.ID = aPipeMessageAssignID.ID;
                    break;

                case PipeMessageUserLogin aLogin:
                    if (aLogin.Success)
                    {
                        Enabled = true;
                        itsPipeClient.Username = aLogin.Name;
                        this?.Invoke(new MethodInvoker(delegate
                        {
                            itsLabelMyName.Text = "Name: " + aLogin.Name;
                        }));
                    }
                    else
                    {
                        NameInputForm aNameForm = new NameInputForm();

                        bool aNameFormOK = false;
                        this?.Invoke(new MethodInvoker(delegate
                        {
                            aNameFormOK = aNameForm.ShowDialog(this) == DialogResult.OK;
                        }));

                        if (aNameFormOK)
                        {
                            PipeMessageUserLogin test = new PipeMessageUserLogin(aNameForm.NameChoice);
                            itsPipeClient.Send(test);
                        }
                        else // If we closed the name form, close the client form
                        {
                            itsPipeClient.Stop();
                            Application.Exit();
                            return;
                        }
                    }
                    break;
            }
        }
        #endregion

        #region Public
        /// <summary>
        /// Adds a line of text to the chat window
        /// </summary>
        /// <param name="theText">Text to be printed</param>
        public void AddChatWindowLine(string theText)
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
        public void SetName(string theName)
        {
            this?.Invoke(new MethodInvoker(delegate
            {
                itsLabelMyName.Text = "Name: " + theName;
            }));
        }
        #endregion

        #region Events
        /// <summary>
        /// Event: Triggered when the client receives a PipeMessage
        /// </summary>
        private void ItsPipeClient_OnPipeMessageReceived(object sender, PipeMessageEventArgs e)
        {
            if (e.PipeMessage is PipeMessage)
                HandleMessage(e.PipeMessage);
        }
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

            if (itsPipeClient.IsConnected)
            {
                PipeMessage aMessage = null;
                if (itsInputTextBox.Text[0] == '/')
                {
                    string aCommandString = itsInputTextBox.Text.Substring(1);
                    aMessage = new PipeMessageCommand(itsInputTextBox.Text, itsPipeClient.Username, aCommandString.Split(' '));
                }
                else
                    aMessage = new PipeMessageText(itsInputTextBox.Text ?? string.Empty, itsPipeClient.Username);

                itsPipeClient.Send(aMessage);
                itsInputTextBox.Clear();
                itsInputTextBox.Focus();
            }
            else
            {
                AddChatWindowLine("Not connected to server.");
            }
        }
        /// <summary>
        /// Event: Triggered when the Clear button is clicked
        /// </summary>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            itsOutput.Clear();
        }
        /// <summary>
        /// Event: Triggered when the form is closing by clicking the X or whatever
        /// </summary>
        private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            itsPipeClient.Stop();
            Application.Exit();
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