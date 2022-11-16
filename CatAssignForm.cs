using System.ComponentModel;
using System.Windows.Forms;

namespace orGenta_NNv
{
    public class CatAssignForm : System.Windows.Forms.Form
	{
		public System.Windows.Forms.CheckedListBox chkListAssignedCats;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtFindCat;
        private System.Windows.Forms.Button btnFind;
        private ToolTip toolTip1;
        private IContainer components;

		public CatAssignForm()
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CatAssignForm));
            this.chkListAssignedCats = new System.Windows.Forms.CheckedListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtFindCat = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // chkListAssignedCats
            // 
            this.chkListAssignedCats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkListAssignedCats.CheckOnClick = true;
            this.chkListAssignedCats.Location = new System.Drawing.Point(13, 12);
            this.chkListAssignedCats.Name = "chkListAssignedCats";
            this.chkListAssignedCats.Size = new System.Drawing.Size(346, 445);
            this.chkListAssignedCats.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(231, 518);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 34);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(103, 518);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 34);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            // 
            // txtFindCat
            // 
            this.txtFindCat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFindCat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFindCat.Location = new System.Drawing.Point(13, 478);
            this.txtFindCat.Name = "txtFindCat";
            this.txtFindCat.Size = new System.Drawing.Size(179, 26);
            this.txtFindCat.TabIndex = 0;
            this.txtFindCat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFindCat_KeyPress);
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.Image = ((System.Drawing.Image)(resources.GetObject("btnFind.Image")));
            this.btnFind.Location = new System.Drawing.Point(205, 477);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(51, 30);
            this.btnFind.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnFind, "Search");
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // CatAssignForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(372, 560);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.txtFindCat);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkListAssignedCats);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CatAssignForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Click on Categories";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnFind_Click(object sender, System.EventArgs e)
		{
			if (this.txtFindCat.Text == "")
			{
				return;
			}
			
			//	find the corresponding category
			int HowManyCats = chkListAssignedCats.Items.Count;
			int CurSelCat = chkListAssignedCats.SelectedIndex;
			string loopCatText = "";

			for (int i = CurSelCat + 1; i < HowManyCats; i++)
			{
				loopCatText = chkListAssignedCats.Items[i].ToString().ToLower();
				if (loopCatText.IndexOf(txtFindCat.Text.ToLower()) >= 0)
				{
					chkListAssignedCats.SelectedIndex = i;
					break;
				}
			}
		}

		private void txtFindCat_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//	on enter key, do the btnFind click
			if (e.KeyChar == (char)13)
			{
				e.Handled=true;
				btnFind_Click(this.txtFindCat, null);
			}
		}
	}
}
