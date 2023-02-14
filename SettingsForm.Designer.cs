namespace orGenta_NNv
{
    partial class SettingsForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbLongErrs = new System.Windows.Forms.CheckBox();
            this.tbTVinterval1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTVinterval2 = new System.Windows.Forms.TextBox();
            this.cbWrapText = new System.Windows.Forms.CheckBox();
            this.cbAdjustItemsToP = new System.Windows.Forms.CheckBox();
            this.cbTrayCreateCats = new System.Windows.Forms.CheckBox();
            this.cbHighlightCats = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(299, 344);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(103, 39);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(178, 342);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 39);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbLongErrs
            // 
            this.cbLongErrs.AutoSize = true;
            this.cbLongErrs.Checked = true;
            this.cbLongErrs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLongErrs.Location = new System.Drawing.Point(30, 18);
            this.cbLongErrs.Name = "cbLongErrs";
            this.cbLongErrs.Size = new System.Drawing.Size(269, 24);
            this.cbLongErrs.TabIndex = 2;
            this.cbLongErrs.Text = "Log Longer Error Message Detail";
            this.cbLongErrs.UseVisualStyleBackColor = true;
            this.cbLongErrs.Click += new System.EventHandler(this.cbLongErrs_Click);
            // 
            // tbTVinterval1
            // 
            this.tbTVinterval1.Location = new System.Drawing.Point(30, 241);
            this.tbTVinterval1.Name = "tbTVinterval1";
            this.tbTVinterval1.Size = new System.Drawing.Size(53, 26);
            this.tbTVinterval1.TabIndex = 3;
            this.tbTVinterval1.Text = "10";
            this.tbTVinterval1.TextChanged += new System.EventHandler(this.tbTVinterval1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Cateogry Initial Update (secs)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 297);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Secondary Cateogry Update (secs)";
            // 
            // tbTVinterval2
            // 
            this.tbTVinterval2.Location = new System.Drawing.Point(30, 293);
            this.tbTVinterval2.Name = "tbTVinterval2";
            this.tbTVinterval2.Size = new System.Drawing.Size(53, 26);
            this.tbTVinterval2.TabIndex = 5;
            this.tbTVinterval2.Text = "30";
            this.tbTVinterval2.TextChanged += new System.EventHandler(this.tbTVinterval2_TextChanged);
            // 
            // cbWrapText
            // 
            this.cbWrapText.AutoSize = true;
            this.cbWrapText.Checked = true;
            this.cbWrapText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWrapText.Location = new System.Drawing.Point(30, 62);
            this.cbWrapText.Name = "cbWrapText";
            this.cbWrapText.Size = new System.Drawing.Size(226, 24);
            this.cbWrapText.TabIndex = 7;
            this.cbWrapText.Text = "Wrap Text Shown on Items";
            this.cbWrapText.UseVisualStyleBackColor = true;
            this.cbWrapText.Click += new System.EventHandler(this.cbWrapText_Click);
            // 
            // cbAdjustItemsToP
            // 
            this.cbAdjustItemsToP.AutoSize = true;
            this.cbAdjustItemsToP.Checked = true;
            this.cbAdjustItemsToP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAdjustItemsToP.Location = new System.Drawing.Point(30, 106);
            this.cbAdjustItemsToP.Name = "cbAdjustItemsToP";
            this.cbAdjustItemsToP.Size = new System.Drawing.Size(262, 24);
            this.cbAdjustItemsToP.TabIndex = 8;
            this.cbAdjustItemsToP.Text = "Adjust Items to Fit Main Window";
            this.cbAdjustItemsToP.UseVisualStyleBackColor = true;
            this.cbAdjustItemsToP.Click += new System.EventHandler(this.cbAdjustItemsToP_Click);
            // 
            // cbTrayCreateCats
            // 
            this.cbTrayCreateCats.AutoSize = true;
            this.cbTrayCreateCats.Checked = true;
            this.cbTrayCreateCats.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTrayCreateCats.Location = new System.Drawing.Point(30, 150);
            this.cbTrayCreateCats.Name = "cbTrayCreateCats";
            this.cbTrayCreateCats.Size = new System.Drawing.Size(296, 24);
            this.cbTrayCreateCats.TabIndex = 9;
            this.cbTrayCreateCats.Text = "Auto Create Categories in Tray Mode";
            this.cbTrayCreateCats.UseVisualStyleBackColor = true;
            this.cbTrayCreateCats.Click += new System.EventHandler(this.cbTrayCreateCats_Click);
            // 
            // cbHighlightCats
            // 
            this.cbHighlightCats.AutoSize = true;
            this.cbHighlightCats.Checked = true;
            this.cbHighlightCats.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHighlightCats.Location = new System.Drawing.Point(30, 194);
            this.cbHighlightCats.Name = "cbHighlightCats";
            this.cbHighlightCats.Size = new System.Drawing.Size(272, 24);
            this.cbHighlightCats.TabIndex = 10;
            this.cbHighlightCats.Text = "Highlight Cateogries in Tray Mode";
            this.cbHighlightCats.UseVisualStyleBackColor = true;
            this.cbHighlightCats.Click += new System.EventHandler(this.cbHighlightCats_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 395);
            this.Controls.Add(this.cbHighlightCats);
            this.Controls.Add(this.cbTrayCreateCats);
            this.Controls.Add(this.cbAdjustItemsToP);
            this.Controls.Add(this.cbWrapText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbTVinterval2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTVinterval1);
            this.Controls.Add(this.cbLongErrs);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "orGenta Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbLongErrs;
        private System.Windows.Forms.TextBox tbTVinterval1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTVinterval2;
        private System.Windows.Forms.CheckBox cbWrapText;
        private System.Windows.Forms.CheckBox cbAdjustItemsToP;
        private System.Windows.Forms.CheckBox cbTrayCreateCats;
        private System.Windows.Forms.CheckBox cbHighlightCats;
    }
}