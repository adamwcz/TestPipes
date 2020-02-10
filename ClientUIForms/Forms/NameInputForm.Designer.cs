namespace ClientUIForms.Forms
{
   partial class NameInputForm
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
         this.label1 = new System.Windows.Forms.Label();
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.itsButtonOK = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(73, 63);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(117, 13);
         this.label1.TabIndex = 0;
         this.label1.Text = "Enter Your Name Here:";
         // 
         // textBox1
         // 
         this.textBox1.Location = new System.Drawing.Point(76, 102);
         this.textBox1.Name = "textBox1";
         this.textBox1.Size = new System.Drawing.Size(150, 20);
         this.textBox1.TabIndex = 1;
         // 
         // itsButtonOK
         // 
         this.itsButtonOK.Location = new System.Drawing.Point(300, 156);
         this.itsButtonOK.Name = "itsButtonOK";
         this.itsButtonOK.Size = new System.Drawing.Size(75, 23);
         this.itsButtonOK.TabIndex = 2;
         this.itsButtonOK.Text = "\"OK\"";
         this.itsButtonOK.UseVisualStyleBackColor = true;
         this.itsButtonOK.Click += new System.EventHandler(this.OK_Click);
         // 
         // NameInputForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(454, 215);
         this.Controls.Add(this.itsButtonOK);
         this.Controls.Add(this.textBox1);
         this.Controls.Add(this.label1);
         this.Name = "NameInputForm";
         this.Text = "NameInputForm";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.Button itsButtonOK;
   }
}