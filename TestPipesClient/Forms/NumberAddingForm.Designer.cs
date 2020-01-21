namespace TestPipesClient.Forms
{
    partial class NumberGameForm
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
            this.itsButtonMinus = new System.Windows.Forms.Button();
            this.itsButtonPlus = new System.Windows.Forms.Button();
            this.itsLabelNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // itsButtonMinus
            // 
            this.itsButtonMinus.Location = new System.Drawing.Point(49, 147);
            this.itsButtonMinus.Name = "itsButtonMinus";
            this.itsButtonMinus.Size = new System.Drawing.Size(57, 54);
            this.itsButtonMinus.TabIndex = 0;
            this.itsButtonMinus.Text = "-1";
            this.itsButtonMinus.UseVisualStyleBackColor = true;
            this.itsButtonMinus.Click += new System.EventHandler(this.itsButtonMinus_Click);
            // 
            // itsButtonPlus
            // 
            this.itsButtonPlus.Location = new System.Drawing.Point(49, 25);
            this.itsButtonPlus.Name = "itsButtonPlus";
            this.itsButtonPlus.Size = new System.Drawing.Size(57, 54);
            this.itsButtonPlus.TabIndex = 1;
            this.itsButtonPlus.Text = "+1";
            this.itsButtonPlus.UseVisualStyleBackColor = true;
            this.itsButtonPlus.Click += new System.EventHandler(this.itsButtonPlus_Click);
            // 
            // itsLabelNumber
            // 
            this.itsLabelNumber.AutoSize = true;
            this.itsLabelNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itsLabelNumber.Location = new System.Drawing.Point(63, 101);
            this.itsLabelNumber.Name = "itsLabelNumber";
            this.itsLabelNumber.Size = new System.Drawing.Size(24, 25);
            this.itsLabelNumber.TabIndex = 2;
            this.itsLabelNumber.Text = "0";
            // 
            // NumberAddingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(157, 251);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Controls.Add(this.itsLabelNumber);
            this.Controls.Add(this.itsButtonPlus);
            this.Controls.Add(this.itsButtonMinus);
            this.Name = "NumberAddingForm";
            this.Text = "Number Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button itsButtonMinus;
        private System.Windows.Forms.Button itsButtonPlus;
        private System.Windows.Forms.Label itsLabelNumber;
    }
}