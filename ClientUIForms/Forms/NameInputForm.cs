using System;
using System.Globalization;
using System.Windows.Forms;

namespace ClientUIForms.Forms
{
   public partial class NameInputForm : Form
   {
      public string NameChoice => textBox1.Text;
      public NameInputForm()
      {
         InitializeComponent();
         itsButtonOK.DialogResult = DialogResult.OK;
         this.AcceptButton = itsButtonOK;
      }

      private void OK_Click(object sender, EventArgs e)
      {
         if (string.IsNullOrEmpty(textBox1.Text))
            textBox1.Text = "Name" + (new Random().Next(1, 1000)).ToString(CultureInfo.InvariantCulture);
      }
   }
}