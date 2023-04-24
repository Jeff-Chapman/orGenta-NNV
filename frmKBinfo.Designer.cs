namespace orGenta_NNv
{
    partial class frmKBinfo
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
            this.tbAboutTheKB = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.cbAlwaysOpen = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbAboutTheKB
            // 
            this.tbAboutTheKB.Location = new System.Drawing.Point(12, 12);
            this.tbAboutTheKB.Multiline = true;
            this.tbAboutTheKB.Name = "tbAboutTheKB";
            this.tbAboutTheKB.ReadOnly = true;
            this.tbAboutTheKB.Size = new System.Drawing.Size(535, 160);
            this.tbAboutTheKB.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(456, 178);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(91, 31);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cbAlwaysOpen
            // 
            this.cbAlwaysOpen.AutoSize = true;
            this.cbAlwaysOpen.Location = new System.Drawing.Point(32, 183);
            this.cbAlwaysOpen.Name = "cbAlwaysOpen";
            this.cbAlwaysOpen.Size = new System.Drawing.Size(152, 24);
            this.cbAlwaysOpen.TabIndex = 2;
            this.cbAlwaysOpen.Text = "Always Open KB";
            this.cbAlwaysOpen.UseVisualStyleBackColor = true;
            // 
            // frmKBinfo
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 218);
            this.Controls.Add(this.cbAlwaysOpen);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbAboutTheKB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmKBinfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About [KB]";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.TextBox tbAboutTheKB;
        public System.Windows.Forms.CheckBox cbAlwaysOpen;
    }
}