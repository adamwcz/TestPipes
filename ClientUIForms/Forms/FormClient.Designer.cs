namespace ClientUIForms.Forms
{
   partial class FormClient
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
         this.itsInputTextBox = new System.Windows.Forms.RichTextBox();
         this.itsButtonSend = new System.Windows.Forms.Button();
         this.itsLabelMyName = new System.Windows.Forms.Label();
         this.itsOutput = new System.Windows.Forms.RichTextBox();
         this.itsButtonClear = new System.Windows.Forms.Button();
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.userListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.imageShareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.numberGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.menuStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // itsInputTextBox
         // 
         this.itsInputTextBox.Location = new System.Drawing.Point(48, 344);
         this.itsInputTextBox.Name = "itsInputTextBox";
         this.itsInputTextBox.Size = new System.Drawing.Size(675, 59);
         this.itsInputTextBox.TabIndex = 0;
         this.itsInputTextBox.Text = "";
         // 
         // itsButtonSend
         // 
         this.itsButtonSend.Location = new System.Drawing.Point(694, 415);
         this.itsButtonSend.Name = "itsButtonSend";
         this.itsButtonSend.Size = new System.Drawing.Size(75, 23);
         this.itsButtonSend.TabIndex = 1;
         this.itsButtonSend.Text = "SEND";
         this.itsButtonSend.UseVisualStyleBackColor = true;
         this.itsButtonSend.Click += new System.EventHandler(this.SendButton_Click);
         // 
         // itsLabelMyName
         // 
         this.itsLabelMyName.AutoSize = true;
         this.itsLabelMyName.Location = new System.Drawing.Point(45, 328);
         this.itsLabelMyName.Name = "itsLabelMyName";
         this.itsLabelMyName.Size = new System.Drawing.Size(35, 13);
         this.itsLabelMyName.TabIndex = 2;
         this.itsLabelMyName.Text = "label1";
         // 
         // itsOutput
         // 
         this.itsOutput.Location = new System.Drawing.Point(48, 50);
         this.itsOutput.Name = "itsOutput";
         this.itsOutput.ReadOnly = true;
         this.itsOutput.Size = new System.Drawing.Size(675, 256);
         this.itsOutput.TabIndex = 3;
         this.itsOutput.Text = "";
         // 
         // itsButtonClear
         // 
         this.itsButtonClear.Location = new System.Drawing.Point(729, 286);
         this.itsButtonClear.Name = "itsButtonClear";
         this.itsButtonClear.Size = new System.Drawing.Size(20, 20);
         this.itsButtonClear.TabIndex = 4;
         this.itsButtonClear.Text = "C";
         this.itsButtonClear.UseVisualStyleBackColor = true;
         this.itsButtonClear.Click += new System.EventHandler(this.ClearButton_Click);
         // 
         // menuStrip1
         // 
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowsToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Size = new System.Drawing.Size(800, 24);
         this.menuStrip1.TabIndex = 5;
         this.menuStrip1.Text = "menuStrip1";
         // 
         // windowsToolStripMenuItem
         // 
         this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userListToolStripMenuItem, this.imageShareToolStripMenuItem, this.numberGameToolStripMenuItem});
         this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
         this.windowsToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
         this.windowsToolStripMenuItem.Text = "Windows";
         // 
         // userListToolStripMenuItem
         // 
         this.userListToolStripMenuItem.Name = "userListToolStripMenuItem";
         this.userListToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
         this.userListToolStripMenuItem.Text = "User List";
         this.userListToolStripMenuItem.Click += new System.EventHandler(this.OnClickToolStripMenuItemUserList);
         // 
         // imageShareToolStripMenuItem
         // 
         this.imageShareToolStripMenuItem.Name = "imageShareToolStripMenuItem";
         this.imageShareToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
         this.imageShareToolStripMenuItem.Text = "Image Sharing";
         this.imageShareToolStripMenuItem.Click += new System.EventHandler(this.OnClickToolStripMenuItemImageShare);
         // 
         // numberGameToolStripMenuItem
         // 
         this.numberGameToolStripMenuItem.Name = "numberGameToolStripMenuItem";
         this.numberGameToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
         this.numberGameToolStripMenuItem.Text = "Number Game";
         this.numberGameToolStripMenuItem.Click += new System.EventHandler(this.OnClickToolStripMenuItemNumberGame);
         // 
         // FormClient
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(800, 450);
         this.Controls.Add(this.itsButtonClear);
         this.Controls.Add(this.itsOutput);
         this.Controls.Add(this.itsLabelMyName);
         this.Controls.Add(this.itsButtonSend);
         this.Controls.Add(this.itsInputTextBox);
         this.Controls.Add(this.menuStrip1);
         this.MainMenuStrip = this.menuStrip1;
         this.Name = "FormClient";
         this.Text = "Test Pipes Client";
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.RichTextBox itsInputTextBox;
      private System.Windows.Forms.Button itsButtonSend;
      private System.Windows.Forms.Label itsLabelMyName;
      private System.Windows.Forms.RichTextBox itsOutput;
      private System.Windows.Forms.Button itsButtonClear;
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem userListToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem imageShareToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem numberGameToolStripMenuItem;
   }
}