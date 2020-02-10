namespace ClientUIForms.Forms
{
   partial class UserListForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.UserListBox = new System.Windows.Forms.ListBox();
         this.SuspendLayout();
         // 
         // UserListBox
         // 
         this.UserListBox.Dock = System.Windows.Forms.DockStyle.Fill;
         this.UserListBox.FormattingEnabled = true;
         this.UserListBox.Location = new System.Drawing.Point(0, 0);
         this.UserListBox.Name = "UserListBox";
         this.UserListBox.Size = new System.Drawing.Size(124, 181);
         this.UserListBox.TabIndex = 1;
         // 
         // UserListForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(124, 181);
         this.Controls.Add(this.UserListBox);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "UserListForm";
         this.ShowIcon = false;
         this.Text = "Person List";
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.ListBox UserListBox;
   }
}