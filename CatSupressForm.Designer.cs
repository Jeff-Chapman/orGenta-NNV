
namespace orGenta_NNv
{
    partial class CatSupressForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.ckListCategories = new System.Windows.Forms.CheckedListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblListHeader = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ckListCategories
            // 
            this.ckListCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ckListCategories.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ckListCategories.CheckOnClick = true;
            this.ckListCategories.FormattingEnabled = true;
            this.ckListCategories.Location = new System.Drawing.Point(15, 46);
            this.ckListCategories.Name = "ckListCategories";
            this.ckListCategories.Size = new System.Drawing.Size(173, 322);
            this.ckListCategories.Sorted = true;
            this.ckListCategories.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(105, 327);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 32);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblListHeader
            // 
            this.lblListHeader.AutoSize = true;
            this.lblListHeader.Location = new System.Drawing.Point(12, 16);
            this.lblListHeader.Name = "lblListHeader";
            this.lblListHeader.Size = new System.Drawing.Size(190, 20);
            this.lblListHeader.TabIndex = 2;
            this.lblListHeader.Text = "Create These Categories:";
            // 
            // CatSupressForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(197, 370);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblListHeader);
            this.Controls.Add(this.ckListCategories);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CatSupressForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.CheckedListBox ckListCategories;
        public System.Windows.Forms.Label lblListHeader;
    }
}