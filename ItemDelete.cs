using System.Windows.Forms;

namespace orGenta_NNv
{
    /// <summary>
    /// Summary description for ItemDelete.
    /// </summary>
    public class ItemDelete : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox gbDeleteOptions;
		public System.Windows.Forms.RadioButton btnDeleteFromCat;
		public System.Windows.Forms.RadioButton btnDiscard;
		private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        public Label lblTrashWarning;
		private System.ComponentModel.Container components = null;

		public ItemDelete()
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
            this.gbDeleteOptions = new System.Windows.Forms.GroupBox();
            this.lblTrashWarning = new System.Windows.Forms.Label();
            this.btnDiscard = new System.Windows.Forms.RadioButton();
            this.btnDeleteFromCat = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbDeleteOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDeleteOptions
            // 
            this.gbDeleteOptions.Controls.Add(this.lblTrashWarning);
            this.gbDeleteOptions.Controls.Add(this.btnDiscard);
            this.gbDeleteOptions.Controls.Add(this.btnDeleteFromCat);
            this.gbDeleteOptions.Location = new System.Drawing.Point(13, 12);
            this.gbDeleteOptions.Name = "gbDeleteOptions";
            this.gbDeleteOptions.Size = new System.Drawing.Size(307, 140);
            this.gbDeleteOptions.TabIndex = 0;
            this.gbDeleteOptions.TabStop = false;
            // 
            // lblTrashWarning
            // 
            this.lblTrashWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTrashWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrashWarning.Location = new System.Drawing.Point(0, 0);
            this.lblTrashWarning.Name = "lblTrashWarning";
            this.lblTrashWarning.Size = new System.Drawing.Size(307, 140);
            this.lblTrashWarning.TabIndex = 3;
            this.lblTrashWarning.Text = "Warning: when you delete items in Trash, they become invisible to everyone (excep" +
    "t Admins)";
            this.lblTrashWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTrashWarning.Visible = false;
            // 
            // btnDiscard
            // 
            this.btnDiscard.Location = new System.Drawing.Point(26, 82);
            this.btnDiscard.Name = "btnDiscard";
            this.btnDiscard.Size = new System.Drawing.Size(267, 35);
            this.btnDiscard.TabIndex = 1;
            this.btnDiscard.Text = "Discard Item Entirely from DB";
            // 
            // btnDeleteFromCat
            // 
            this.btnDeleteFromCat.Checked = true;
            this.btnDeleteFromCat.Location = new System.Drawing.Point(26, 35);
            this.btnDeleteFromCat.Name = "btnDeleteFromCat";
            this.btnDeleteFromCat.Size = new System.Drawing.Size(267, 35);
            this.btnDeleteFromCat.TabIndex = 0;
            this.btnDeleteFromCat.TabStop = true;
            this.btnDeleteFromCat.Text = "Remove Item from this Category";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(179, 168);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 35);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(38, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            // 
            // ItemDelete
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(338, 218);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbDeleteOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ItemDelete";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Item Delete Options";
            this.gbDeleteOptions.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
//			this.Close();
        }
	}
}
