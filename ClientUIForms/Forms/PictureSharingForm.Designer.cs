namespace ClientUIForms.Forms
{
   partial class ImageSharingForm
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
         this.itsPictureBox = new System.Windows.Forms.PictureBox();
         this.itsButtonChoose = new System.Windows.Forms.Button();
         this.itsButtonSaveAs = new System.Windows.Forms.Button();
         this.itsLabelSenderName = new System.Windows.Forms.Label();
         this.itsLabelFileName = new System.Windows.Forms.Label();
         this.itsLabelFileSize = new System.Windows.Forms.Label();
         ((System.ComponentModel.ISupportInitialize)(this.itsPictureBox)).BeginInit();
         this.SuspendLayout();
         // 
         // itsPictureBox
         // 
         this.itsPictureBox.Location = new System.Drawing.Point(35, 51);
         this.itsPictureBox.Name = "itsPictureBox";
         this.itsPictureBox.Size = new System.Drawing.Size(256, 224);
         this.itsPictureBox.TabIndex = 0;
         this.itsPictureBox.TabStop = false;
         this.itsPictureBox.Click += new System.EventHandler(this.pictureBox1_Click);
         // 
         // itsButtonChoose
         // 
         this.itsButtonChoose.Location = new System.Drawing.Point(115, 12);
         this.itsButtonChoose.Name = "itsButtonChoose";
         this.itsButtonChoose.Size = new System.Drawing.Size(95, 33);
         this.itsButtonChoose.TabIndex = 1;
         this.itsButtonChoose.Text = "Send An Image";
         this.itsButtonChoose.UseVisualStyleBackColor = true;
         this.itsButtonChoose.Click += new System.EventHandler(this.itsButtonChoose_Click);
         // 
         // itsButtonSaveAs
         // 
         this.itsButtonSaveAs.Enabled = false;
         this.itsButtonSaveAs.Location = new System.Drawing.Point(216, 311);
         this.itsButtonSaveAs.Name = "itsButtonSaveAs";
         this.itsButtonSaveAs.Size = new System.Drawing.Size(75, 23);
         this.itsButtonSaveAs.TabIndex = 2;
         this.itsButtonSaveAs.Text = "Save As..";
         this.itsButtonSaveAs.UseVisualStyleBackColor = true;
         this.itsButtonSaveAs.Click += new System.EventHandler(this.itsButtonSaveAs_Click);
         // 
         // itsLabelSenderName
         // 
         this.itsLabelSenderName.AutoSize = true;
         this.itsLabelSenderName.Location = new System.Drawing.Point(32, 316);
         this.itsLabelSenderName.Name = "itsLabelSenderName";
         this.itsLabelSenderName.Size = new System.Drawing.Size(39, 13);
         this.itsLabelSenderName.TabIndex = 3;
         this.itsLabelSenderName.Text = "sender";
         // 
         // itsLabelFileName
         // 
         this.itsLabelFileName.AutoSize = true;
         this.itsLabelFileName.Location = new System.Drawing.Point(32, 291);
         this.itsLabelFileName.Name = "itsLabelFileName";
         this.itsLabelFileName.Size = new System.Drawing.Size(23, 13);
         this.itsLabelFileName.TabIndex = 4;
         this.itsLabelFileName.Text = "title";
         // 
         // itsLabelFileSize
         // 
         this.itsLabelFileSize.AutoSize = true;
         this.itsLabelFileSize.Location = new System.Drawing.Point(224, 291);
         this.itsLabelFileSize.Name = "itsLabelFileSize";
         this.itsLabelFileSize.Size = new System.Drawing.Size(0, 13);
         this.itsLabelFileSize.TabIndex = 5;
         // 
         // ImageSharingForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(318, 345);
         this.Controls.Add(this.itsLabelFileSize);
         this.Controls.Add(this.itsLabelFileName);
         this.Controls.Add(this.itsLabelSenderName);
         this.Controls.Add(this.itsButtonSaveAs);
         this.Controls.Add(this.itsButtonChoose);
         this.Controls.Add(this.itsPictureBox);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.Location = new System.Drawing.Point(0, 0);
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "ImageSharingForm";
         this.Text = "Image Sharing";
         ((System.ComponentModel.ISupportInitialize)(this.itsPictureBox)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.PictureBox itsPictureBox;
      private System.Windows.Forms.Button itsButtonChoose;
      private System.Windows.Forms.Button itsButtonSaveAs;
      private System.Windows.Forms.Label itsLabelSenderName;
      private System.Windows.Forms.Label itsLabelFileName;
      private System.Windows.Forms.Label itsLabelFileSize;
   }
}