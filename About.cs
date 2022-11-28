using System;
using System.Windows.Forms;

namespace orGenta_NNv
{

    public class About : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnOK;
		public System.Windows.Forms.TextBox txtBuildNumber;
		public System.Windows.Forms.TextBox txtVersionNumber;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.TextBox txtDotNetVsn;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private LinkLabel btnCheckUpdates;
        public TextBox tbIntialized;
		private System.ComponentModel.Container components = null;

		public About()
		{
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtBuildNumber = new System.Windows.Forms.TextBox();
            this.txtVersionNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDotNetVsn = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCheckUpdates = new System.Windows.Forms.LinkLabel();
            this.tbIntialized = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(77, 28);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(371, 117);
            this.textBox1.TabIndex = 0;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "Copyright (c) 2006, 2021 J.D. Chapman\r\n\r\nDistribution Licensed to Izard Explorato" +
    "ry LLC\r\n\r\nReleased under the GNU LGPL v3.0";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(167, 298);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 33);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            // 
            // txtBuildNumber
            // 
            this.txtBuildNumber.AcceptsReturn = true;
            this.txtBuildNumber.BackColor = System.Drawing.SystemColors.Control;
            this.txtBuildNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuildNumber.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuildNumber.Location = new System.Drawing.Point(255, 229);
            this.txtBuildNumber.Name = "txtBuildNumber";
            this.txtBuildNumber.ReadOnly = true;
            this.txtBuildNumber.Size = new System.Drawing.Size(193, 24);
            this.txtBuildNumber.TabIndex = 3;
            this.txtBuildNumber.TabStop = false;
            this.txtBuildNumber.Text = "(Build Num)";
            this.txtBuildNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtVersionNumber
            // 
            this.txtVersionNumber.AcceptsReturn = true;
            this.txtVersionNumber.BackColor = System.Drawing.SystemColors.Control;
            this.txtVersionNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtVersionNumber.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVersionNumber.Location = new System.Drawing.Point(167, 199);
            this.txtVersionNumber.Name = "txtVersionNumber";
            this.txtVersionNumber.ReadOnly = true;
            this.txtVersionNumber.Size = new System.Drawing.Size(281, 24);
            this.txtVersionNumber.TabIndex = 4;
            this.txtVersionNumber.TabStop = false;
            this.txtVersionNumber.Text = "(sw Vsn)";
            this.txtVersionNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(13, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(435, 4);
            this.label1.TabIndex = 5;
            // 
            // txtDotNetVsn
            // 
            this.txtDotNetVsn.AcceptsReturn = true;
            this.txtDotNetVsn.BackColor = System.Drawing.SystemColors.Control;
            this.txtDotNetVsn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDotNetVsn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDotNetVsn.Location = new System.Drawing.Point(12, 199);
            this.txtDotNetVsn.Name = "txtDotNetVsn";
            this.txtDotNetVsn.ReadOnly = true;
            this.txtDotNetVsn.Size = new System.Drawing.Size(210, 24);
            this.txtDotNetVsn.TabIndex = 6;
            this.txtDotNetVsn.TabStop = false;
            this.txtDotNetVsn.Text = "(dotnet vsn here)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(19, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(51, 47);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(14, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(436, 4);
            this.label2.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(202, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Icon Designs by Techlogica";
            // 
            // btnCheckUpdates
            // 
            this.btnCheckUpdates.AutoSize = true;
            this.btnCheckUpdates.Location = new System.Drawing.Point(11, 233);
            this.btnCheckUpdates.Name = "btnCheckUpdates";
            this.btnCheckUpdates.Size = new System.Drawing.Size(159, 20);
            this.btnCheckUpdates.TabIndex = 11;
            this.btnCheckUpdates.TabStop = true;
            this.btnCheckUpdates.Text = "Check For Updates...";
            this.btnCheckUpdates.Click += new System.EventHandler(this.btnCheckUpdates_Click);
            // 
            // tbIntialized
            // 
            this.tbIntialized.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbIntialized.Location = new System.Drawing.Point(185, 265);
            this.tbIntialized.Name = "tbIntialized";
            this.tbIntialized.Size = new System.Drawing.Size(262, 19);
            this.tbIntialized.TabIndex = 12;
            this.tbIntialized.Text = "(Initialized)";
            this.tbIntialized.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // About
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(469, 355);
            this.ControlBox = false;
            this.Controls.Add(this.tbIntialized);
            this.Controls.Add(this.btnCheckUpdates);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtDotNetVsn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtVersionNumber);
            this.Controls.Add(this.txtBuildNumber);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About orGenta";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        private void btnCheckUpdates_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://izardsoftware.wordpress.com/orgenta/");
        }

	}
}
