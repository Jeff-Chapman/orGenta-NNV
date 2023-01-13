namespace orGenta_NNv
{
    partial class TreeViewForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeViewForm));
            this.tvCategories = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.demoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.promoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.expandNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandOnlyThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.freezeClosedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlAddNode = new System.Windows.Forms.Panel();
            this.btnNewOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNewSister = new System.Windows.Forms.RadioButton();
            this.btnNewChild = new System.Windows.Forms.RadioButton();
            this.tbNewNodeName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrTVdirty = new System.Windows.Forms.Timer(this.components);
            this.pnlEditCat = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOKedit = new System.Windows.Forms.Button();
            this.cbReassign = new System.Windows.Forms.CheckBox();
            this.cbManlOnly = new System.Windows.Forms.CheckBox();
            this.tbEditingCat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbDBisDown = new System.Windows.Forms.PictureBox();
            this.ttDBoffline = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aboutThisKBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteBelowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.pnlAddNode.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlEditCat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDBisDown)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvCategories
            // 
            this.tvCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvCategories.ContextMenuStrip = this.contextMenuStrip1;
            this.tvCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvCategories.Location = new System.Drawing.Point(22, 30);
            this.tvCategories.Name = "tvCategories";
            this.tvCategories.Size = new System.Drawing.Size(357, 600);
            this.tvCategories.TabIndex = 0;
            this.tvCategories.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvCategories_BeforeCollapse);
            this.tvCategories.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvCategories_BeforeExpand);
            this.tvCategories.NodeMouseHover += new System.Windows.Forms.TreeNodeMouseHoverEventHandler(this.tvCategories_NodeMouseHover);
            this.tvCategories.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCategories_AfterSelect);
            this.tvCategories.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvCategories_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddNodeToolStripMenuItem,
            this.editNodeToolStripMenuItem,
            this.deleteNodeToolStripMenuItem,
            this.toolStripMenuItem1,
            this.demoteToolStripMenuItem,
            this.promoteToolStripMenuItem,
            this.toolStripMenuItem2,
            this.copyToolStripMenuItem,
            this.moveToolStripMenuItem,
            this.pasteBelowToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripMenuItem3,
            this.expandNodeToolStripMenuItem,
            this.collapseNodeToolStripMenuItem,
            this.expandOnlyThisToolStripMenuItem,
            this.freezeClosedToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(271, 471);
            // 
            // AddNodeToolStripMenuItem
            // 
            this.AddNodeToolStripMenuItem.Name = "AddNodeToolStripMenuItem";
            this.AddNodeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
            this.AddNodeToolStripMenuItem.Size = new System.Drawing.Size(270, 32);
            this.AddNodeToolStripMenuItem.Text = "Add Category";
            this.AddNodeToolStripMenuItem.Click += new System.EventHandler(this.AddNodeToolStripMenuItem_Click);
            // 
            // editNodeToolStripMenuItem
            // 
            this.editNodeToolStripMenuItem.Name = "editNodeToolStripMenuItem";
            this.editNodeToolStripMenuItem.Size = new System.Drawing.Size(270, 32);
            this.editNodeToolStripMenuItem.Text = "Edit Category";
            this.editNodeToolStripMenuItem.Click += new System.EventHandler(this.editNodeToolStripMenuItem_Click);
            // 
            // deleteNodeToolStripMenuItem
            // 
            this.deleteNodeToolStripMenuItem.Name = "deleteNodeToolStripMenuItem";
            this.deleteNodeToolStripMenuItem.Size = new System.Drawing.Size(270, 32);
            this.deleteNodeToolStripMenuItem.Text = "Delete Category";
            this.deleteNodeToolStripMenuItem.Click += new System.EventHandler(this.deleteNodeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(267, 6);
            // 
            // demoteToolStripMenuItem
            // 
            this.demoteToolStripMenuItem.Name = "demoteToolStripMenuItem";
            this.demoteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.demoteToolStripMenuItem.Size = new System.Drawing.Size(270, 32);
            this.demoteToolStripMenuItem.Text = ">> Demote";
            this.demoteToolStripMenuItem.Click += new System.EventHandler(this.demoteToolStripMenuItem_Click);
            // 
            // promoteToolStripMenuItem
            // 
            this.promoteToolStripMenuItem.Name = "promoteToolStripMenuItem";
            this.promoteToolStripMenuItem.Size = new System.Drawing.Size(270, 32);
            this.promoteToolStripMenuItem.Text = "Promote <<";
            this.promoteToolStripMenuItem.Click += new System.EventHandler(this.promoteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(267, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(270, 32);
            this.copyToolStripMenuItem.Text = "Cross-DB Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // moveToolStripMenuItem
            // 
            this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            this.moveToolStripMenuItem.Size = new System.Drawing.Size(270, 32);
            this.moveToolStripMenuItem.Text = "Move";
            this.moveToolStripMenuItem.Click += new System.EventHandler(this.moveToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(270, 32);
            this.pasteToolStripMenuItem.Text = "Paste Child";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(267, 6);
            // 
            // expandNodeToolStripMenuItem
            // 
            this.expandNodeToolStripMenuItem.Name = "expandNodeToolStripMenuItem";
            this.expandNodeToolStripMenuItem.Size = new System.Drawing.Size(270, 32);
            this.expandNodeToolStripMenuItem.Text = "Expand Category";
            this.expandNodeToolStripMenuItem.Click += new System.EventHandler(this.expandNodeToolStripMenuItem_Click);
            // 
            // collapseNodeToolStripMenuItem
            // 
            this.collapseNodeToolStripMenuItem.Name = "collapseNodeToolStripMenuItem";
            this.collapseNodeToolStripMenuItem.Size = new System.Drawing.Size(270, 32);
            this.collapseNodeToolStripMenuItem.Text = "Collapse Category";
            this.collapseNodeToolStripMenuItem.Click += new System.EventHandler(this.collapseNodeToolStripMenuItem_Click);
            // 
            // expandOnlyThisToolStripMenuItem
            // 
            this.expandOnlyThisToolStripMenuItem.Name = "expandOnlyThisToolStripMenuItem";
            this.expandOnlyThisToolStripMenuItem.Size = new System.Drawing.Size(270, 32);
            this.expandOnlyThisToolStripMenuItem.Text = "Expand Only This";
            this.expandOnlyThisToolStripMenuItem.Click += new System.EventHandler(this.expandOnlyThisToolStripMenuItem_Click);
            // 
            // freezeClosedToolStripMenuItem
            // 
            this.freezeClosedToolStripMenuItem.Name = "freezeClosedToolStripMenuItem";
            this.freezeClosedToolStripMenuItem.Size = new System.Drawing.Size(270, 32);
            this.freezeClosedToolStripMenuItem.Text = "Freeze Closed";
            this.freezeClosedToolStripMenuItem.Click += new System.EventHandler(this.freezeClosedToolStripMenuItem_Click);
            // 
            // pnlAddNode
            // 
            this.pnlAddNode.Controls.Add(this.btnNewOK);
            this.pnlAddNode.Controls.Add(this.groupBox1);
            this.pnlAddNode.Controls.Add(this.tbNewNodeName);
            this.pnlAddNode.Controls.Add(this.label1);
            this.pnlAddNode.Location = new System.Drawing.Point(372, 91);
            this.pnlAddNode.Name = "pnlAddNode";
            this.pnlAddNode.Size = new System.Drawing.Size(335, 250);
            this.pnlAddNode.TabIndex = 1;
            this.pnlAddNode.Visible = false;
            // 
            // btnNewOK
            // 
            this.btnNewOK.Location = new System.Drawing.Point(221, 196);
            this.btnNewOK.Name = "btnNewOK";
            this.btnNewOK.Size = new System.Drawing.Size(88, 39);
            this.btnNewOK.TabIndex = 3;
            this.btnNewOK.Text = "OK";
            this.btnNewOK.UseVisualStyleBackColor = true;
            this.btnNewOK.Click += new System.EventHandler(this.btnNewOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNewSister);
            this.groupBox1.Controls.Add(this.btnNewChild);
            this.groupBox1.Location = new System.Drawing.Point(27, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 103);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // btnNewSister
            // 
            this.btnNewSister.AutoSize = true;
            this.btnNewSister.Checked = true;
            this.btnNewSister.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewSister.Location = new System.Drawing.Point(24, 22);
            this.btnNewSister.Name = "btnNewSister";
            this.btnNewSister.Size = new System.Drawing.Size(227, 29);
            this.btnNewSister.TabIndex = 1;
            this.btnNewSister.TabStop = true;
            this.btnNewSister.Text = "Sister below this node";
            this.btnNewSister.UseVisualStyleBackColor = true;
            // 
            // btnNewChild
            // 
            this.btnNewChild.AutoSize = true;
            this.btnNewChild.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewChild.Location = new System.Drawing.Point(24, 60);
            this.btnNewChild.Name = "btnNewChild";
            this.btnNewChild.Size = new System.Drawing.Size(187, 29);
            this.btnNewChild.TabIndex = 0;
            this.btnNewChild.Text = "Child of this node";
            this.btnNewChild.UseVisualStyleBackColor = true;
            // 
            // tbNewNodeName
            // 
            this.tbNewNodeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNewNodeName.Location = new System.Drawing.Point(87, 20);
            this.tbNewNodeName.Name = "tbNewNodeName";
            this.tbNewNodeName.Size = new System.Drawing.Size(222, 30);
            this.tbNewNodeName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // tmrTVdirty
            // 
            this.tmrTVdirty.Enabled = true;
            this.tmrTVdirty.Interval = 1000;
            this.tmrTVdirty.Tick += new System.EventHandler(this.tmrTVdirty_Tick);
            // 
            // pnlEditCat
            // 
            this.pnlEditCat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEditCat.Controls.Add(this.btnCancel);
            this.pnlEditCat.Controls.Add(this.btnOKedit);
            this.pnlEditCat.Controls.Add(this.cbReassign);
            this.pnlEditCat.Controls.Add(this.cbManlOnly);
            this.pnlEditCat.Controls.Add(this.tbEditingCat);
            this.pnlEditCat.Controls.Add(this.label2);
            this.pnlEditCat.Location = new System.Drawing.Point(372, 120);
            this.pnlEditCat.Name = "pnlEditCat";
            this.pnlEditCat.Size = new System.Drawing.Size(335, 217);
            this.pnlEditCat.TabIndex = 2;
            this.pnlEditCat.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(116, 159);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 38);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOKedit
            // 
            this.btnOKedit.Location = new System.Drawing.Point(219, 159);
            this.btnOKedit.Name = "btnOKedit";
            this.btnOKedit.Size = new System.Drawing.Size(87, 38);
            this.btnOKedit.TabIndex = 6;
            this.btnOKedit.Text = "OK";
            this.btnOKedit.UseVisualStyleBackColor = true;
            this.btnOKedit.Click += new System.EventHandler(this.btnOKedit_Click);
            // 
            // cbReassign
            // 
            this.cbReassign.AutoSize = true;
            this.cbReassign.Location = new System.Drawing.Point(42, 109);
            this.cbReassign.Name = "cbReassign";
            this.cbReassign.Size = new System.Drawing.Size(209, 24);
            this.cbReassign.TabIndex = 5;
            this.cbReassign.Text = "Re-Assign New Matches";
            this.cbReassign.UseVisualStyleBackColor = true;
            // 
            // cbManlOnly
            // 
            this.cbManlOnly.AutoSize = true;
            this.cbManlOnly.Location = new System.Drawing.Point(42, 70);
            this.cbManlOnly.Name = "cbManlOnly";
            this.cbManlOnly.Size = new System.Drawing.Size(174, 24);
            this.cbManlOnly.TabIndex = 4;
            this.cbManlOnly.Text = "Manual Assign Only";
            this.cbManlOnly.UseVisualStyleBackColor = true;
            // 
            // tbEditingCat
            // 
            this.tbEditingCat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEditingCat.Location = new System.Drawing.Point(85, 16);
            this.tbEditingCat.Name = "tbEditingCat";
            this.tbEditingCat.Size = new System.Drawing.Size(222, 30);
            this.tbEditingCat.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Edit:";
            // 
            // pbDBisDown
            // 
            this.pbDBisDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDBisDown.Image = ((System.Drawing.Image)(resources.GetObject("pbDBisDown.Image")));
            this.pbDBisDown.Location = new System.Drawing.Point(370, 2);
            this.pbDBisDown.Name = "pbDBisDown";
            this.pbDBisDown.Size = new System.Drawing.Size(24, 24);
            this.pbDBisDown.TabIndex = 3;
            this.pbDBisDown.TabStop = false;
            this.ttDBoffline.SetToolTip(this.pbDBisDown, "Database Is Offline");
            this.pbDBisDown.Visible = false;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutThisKBToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(196, 36);
            // 
            // aboutThisKBToolStripMenuItem
            // 
            this.aboutThisKBToolStripMenuItem.Name = "aboutThisKBToolStripMenuItem";
            this.aboutThisKBToolStripMenuItem.Size = new System.Drawing.Size(195, 32);
            this.aboutThisKBToolStripMenuItem.Text = "About This KB";
            this.aboutThisKBToolStripMenuItem.Click += new System.EventHandler(this.aboutThisKBToolStripMenuItem_Click);
            // 
            // pasteBelowToolStripMenuItem
            // 
            this.pasteBelowToolStripMenuItem.Name = "pasteBelowToolStripMenuItem";
            this.pasteBelowToolStripMenuItem.Size = new System.Drawing.Size(270, 32);
            this.pasteBelowToolStripMenuItem.Text = "Paste Below";
            this.pasteBelowToolStripMenuItem.Click += new System.EventHandler(this.pasteBelowToolStripMenuItem_Click);
            // 
            // TreeViewForm
            // 
            this.AcceptButton = this.btnNewOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 665);
            this.ContextMenuStrip = this.contextMenuStrip2;
            this.ControlBox = false;
            this.Controls.Add(this.pbDBisDown);
            this.Controls.Add(this.pnlEditCat);
            this.Controls.Add(this.pnlAddNode);
            this.Controls.Add(this.tvCategories);
            this.MaximizeBox = false;
            this.Name = "TreeViewForm";
            this.Text = "(DB Name)";
            this.Activated += new System.EventHandler(this.TreeViewForm_Activated);
            this.Deactivate += new System.EventHandler(this.TreeViewForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TreeViewForm_FormClosing);
            this.Load += new System.EventHandler(this.TreeViewForm_Load);
            this.MouseEnter += new System.EventHandler(this.TreeViewForm_MouseEnter);
            this.contextMenuStrip1.ResumeLayout(false);
            this.pnlAddNode.ResumeLayout(false);
            this.pnlAddNode.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlEditCat.ResumeLayout(false);
            this.pnlEditCat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDBisDown)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TreeView tvCategories;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem AddNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandOnlyThisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem freezeClosedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem expandNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem demoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem promoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.Panel pnlAddNode;
        private System.Windows.Forms.Button btnNewOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton btnNewSister;
        private System.Windows.Forms.RadioButton btnNewChild;
        private System.Windows.Forms.TextBox tbNewNodeName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem editNodeToolStripMenuItem;
        private System.Windows.Forms.Panel pnlEditCat;
        private System.Windows.Forms.Button btnOKedit;
        private System.Windows.Forms.CheckBox cbReassign;
        private System.Windows.Forms.CheckBox cbManlOnly;
        private System.Windows.Forms.TextBox tbEditingCat;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Timer tmrTVdirty;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip ttDBoffline;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem aboutThisKBToolStripMenuItem;
        public System.Windows.Forms.PictureBox pbDBisDown;
        private System.Windows.Forms.ToolStripMenuItem pasteBelowToolStripMenuItem;
    }
}