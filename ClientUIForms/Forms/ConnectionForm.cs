using System;
using System.Windows.Forms;

namespace ClientUIForms.Forms
{
   public partial class ConnectionForm : Form
   {
      public string ServerName => textBox1.Text;
      public string PipeName => textBox2.Text;
      public ConnectionForm()
      {
         InitializeComponent();
         itsButtonOK.DialogResult = DialogResult.OK;
         this.AcceptButton = itsButtonOK;
         this.Text = "Client Connection Info";
         this.FormClosing += ConnectionForm_FormClosing;
      }

      private void ConnectionForm_FormClosing(object sender, FormClosingEventArgs e)
      {
         if (string.IsNullOrEmpty(textBox1.Text))
            textBox1.Text = "127.0.0.1";
         if (string.IsNullOrEmpty(textBox2.Text))
            textBox2.Text = "testpipe";
      }

      private void OK_Click(object sender, EventArgs e)
      {

      }
   }
}