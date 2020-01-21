using System;
using System.IO;
using System.Windows.Forms;

namespace TestPipeServer
{
    public partial class FormServer : Form
    {
        #region Fields
        private readonly object itsLock = new object();
        private string itsLogFile = "C:/testlog/testpipeslog" + DateTime.Now.ToOADate() + ".txt";
        private bool itsIsLogging = false;
        private bool exit = false;
        #endregion

        #region Constructor
        public FormServer()
        {
            InitializeComponent();
            CreateFile(itsLogFile);
            Log("**********" + Environment.NewLine + "SERVER START @ " + DateTime.Now + Environment.NewLine + "**********" + Environment.NewLine + Environment.NewLine);
            FormClosing += FormServer_FormClosing;
        }
        #endregion

        #region Public
        /// <summary>
        /// Adds text to the textbox
        /// </summary>
        public void AddLine(string theText)
        {
            lock (itsLock)
            {
                this.Invoke(new MethodInvoker(delegate { richTextBox1.AppendText(theText + "\n"); }));
                Log(theText + Environment.NewLine);
            }
        }
        /// <summary>
        /// Adds a name to the user list
        /// </summary>
        public void AddUser(string theName)
        {
            lock (itsLock)
            {
                this.Invoke(new MethodInvoker(delegate { itsUserListBox.Items.Add(theName); }));
            }
        }
        /// <summary>
        /// Removes a name from the user list
        /// </summary>
        public void RemoveUser(string theName)
        {
            lock (itsLock)
            {
                this.Invoke(new MethodInvoker(delegate { itsUserListBox.Items.Remove(theName); }));
            }
        }
        #endregion

        #region Private
        /// <summary>
        /// Creates a file. And the directory to the file if the directory doesn't exist.
        /// </summary>
        /// <param name="theFile"></param>
        private static void CreateFile(string theFile)
        {
            if (Path.GetDirectoryName(theFile) is string aLogDirectory && !Directory.Exists(aLogDirectory))
                Directory.CreateDirectory(aLogDirectory);
            if (!File.Exists(theFile))
                File.Create(theFile).Close();
        }
        /// <summary>
        /// Writes text to the log file
        /// </summary>
        private void Log(string theText)
        {
            if (itsIsLogging)
                File.AppendAllText(itsLogFile, theText);
        }
        #endregion

        #region Events
        /// <summary>
        /// Event: Triggered when the form is closing
        /// </summary>
        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!exit)
            {
                exit = true;
                Application.Exit();
                Log(Environment.NewLine + "**********" + Environment.NewLine + "SERVER STOPPED @ " + DateTime.Now + Environment.NewLine + "**********" + Environment.NewLine + Environment.NewLine);
            };
        }
        #endregion
    }
}