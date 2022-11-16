namespace orGenta_NNv
{
    partial class SideUtilBox
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
            this.lbOpenWindows = new System.Windows.Forms.ListBox();
            this.lbCatList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbOpenWindows
            // 
            this.lbOpenWindows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOpenWindows.FormattingEnabled = true;
            this.lbOpenWindows.ItemHeight = 20;
            this.lbOpenWindows.Location = new System.Drawing.Point(5, 10);
            this.lbOpenWindows.Name = "lbOpenWindows";
            this.lbOpenWindows.Size = new System.Drawing.Size(274, 464);
            this.lbOpenWindows.Sorted = true;
            this.lbOpenWindows.TabIndex = 0;
            this.lbOpenWindows.Click += new System.EventHandler(this.lbOpenWindows_Click);
            // 
            // lbCatList
            // 
            this.lbCatList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCatList.FormattingEnabled = true;
            this.lbCatList.ItemHeight = 20;
            this.lbCatList.Location = new System.Drawing.Point(5, 10);
            this.lbCatList.Name = "lbCatList";
            this.lbCatList.Size = new System.Drawing.Size(274, 464);
            this.lbCatList.Sorted = true;
            this.lbCatList.TabIndex = 1;
            this.lbCatList.Visible = false;
            this.lbCatList.Click += new System.EventHandler(this.lbCatList_Click);
            // 
            // SideUtilBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 520);
            this.ControlBox = false;
            this.Controls.Add(this.lbCatList);
            this.Controls.Add(this.lbOpenWindows);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SideUtilBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Open Windows";
            this.Load += new System.EventHandler(this.SideUtilBox_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox lbOpenWindows;
        public System.Windows.Forms.ListBox lbCatList;

    }
}