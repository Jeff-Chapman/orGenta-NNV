namespace orGenta_NNv
{
    /// <summary>
    /// Summary description for ZoomedItem.
    /// </summary>
    public class ZoomedItem : System.Windows.Forms.Form
	{
		public System.Windows.Forms.TextBox txtZoomBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ZoomedItem()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.txtZoomBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtZoomBox
            // 
            this.txtZoomBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtZoomBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZoomBox.Location = new System.Drawing.Point(0, 0);
            this.txtZoomBox.Multiline = true;
            this.txtZoomBox.Name = "txtZoomBox";
            this.txtZoomBox.Size = new System.Drawing.Size(520, 157);
            this.txtZoomBox.TabIndex = 0;
            this.txtZoomBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZoomBox_KeyPress);
            // 
            // ZoomedItem
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(520, 157);
            this.Controls.Add(this.txtZoomBox);
            this.Name = "ZoomedItem";
            this.Text = "Zoomed Item";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void txtZoomBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)27)
			{
				this.Close();
			}
		}
	}
}
