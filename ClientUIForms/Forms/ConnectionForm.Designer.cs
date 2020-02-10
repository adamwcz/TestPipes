namespace ClientUIForms.Forms
{
   partial class ConnectionForm
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
         this.itsButtonOK = new System.Windows.Forms.Button();
         this.textBox1 = new System.Windows.Forms.TextBox();
         this.textBox2 = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // itsButtonOK
         // 
         this.itsButtonOK.Location = new System.Drawing.Point(183, 180);
         this.itsButtonOK.Name = "itsButtonOK";
         this.itsButtonOK.Size = new System.Drawing.Size(79, 25);
         this.itsButtonOK.TabIndex = 0;
         this.itsButtonOK.Text = "OK";
         this.itsButtonOK.UseVisualStyleBackColor = true;
         this.itsButtonOK.Click += new System.EventHandler(this.OK_Click);
         // 
         // textBox1
         // 
         this.textBox1.Location = new System.Drawing.Point(116, 50);
         this.textBox1.Name = "textBox1";
         this.textBox1.Size = new System.Drawing.Size(146, 20);
         this.textBox1.TabIndex = 1;
         this.textBox1.Text = "127.0.0.1";
         // 
         // textBox2
         // 
         this.textBox2.Location = new System.Drawing.Point(116, 110);
         this.textBox2.Name = "textBox2";
         this.textBox2.Size = new System.Drawing.Size(146, 20);
         this.textBox2.TabIndex = 2;
         this.textBox2.Text = "testpipe";
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(56, 53);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(54, 13);
         this.label1.TabIndex = 3;
         this.label1.Text = "Server IP:";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(48, 113);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(62, 13);
         this.label2.TabIndex = 4;
         this.label2.Text = "Pipe Name:";
         // 
         // ConnectionForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(324, 241);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.textBox2);
         this.Controls.Add(this.textBox1);
         this.Controls.Add(this.itsButtonOK);
         this.Name = "ConnectionForm";
         this.Text = "ConnectionForm";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button itsButtonOK;
      private System.Windows.Forms.TextBox textBox1;
      private System.Windows.Forms.TextBox textBox2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
   }
}