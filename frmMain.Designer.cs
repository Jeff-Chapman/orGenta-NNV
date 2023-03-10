namespace orGenta_NNv
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuOpenKB = new System.Windows.Forms.MenuItem();
            this.menuCloseKB = new System.Windows.Forms.MenuItem();
            this.menuItem41 = new System.Windows.Forms.MenuItem();
            this.menuItemPrint = new System.Windows.Forms.MenuItem();
            this.menuAdvPrint = new System.Windows.Forms.MenuItem();
            this.menuPrinterSetup = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuTrayed = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuExit = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuFindCategory = new System.Windows.Forms.MenuItem();
            this.menuFindItem = new System.Windows.Forms.MenuItem();
            this.menuFindNext = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuAssignTo = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuZoomItem = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuCloseItems = new System.Windows.Forms.MenuItem();
            this.menuItem25 = new System.Windows.Forms.MenuItem();
            this.menuExpandNode = new System.Windows.Forms.MenuItem();
            this.menuCollapseNode = new System.Windows.Forms.MenuItem();
            this.menuExpandThis = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuExpandAll = new System.Windows.Forms.MenuItem();
            this.menuCollapseAll = new System.Windows.Forms.MenuItem();
            this.menuItem28 = new System.Windows.Forms.MenuItem();
            this.menuImportItems = new System.Windows.Forms.MenuItem();
            this.menuExportCSV = new System.Windows.Forms.MenuItem();
            this.menuItems2email = new System.Windows.Forms.MenuItem();
            this.menuItem34 = new System.Windows.Forms.MenuItem();
            this.menuNewKDB = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuAutoAssign = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuSettings = new System.Windows.Forms.MenuItem();
            this.menuItem37 = new System.Windows.Forms.MenuItem();
            this.menuOnlineHelp = new System.Windows.Forms.MenuItem();
            this.menuAbout = new System.Windows.Forms.MenuItem();
            this.menuShare = new System.Windows.Forms.MenuItem();
            this.menuFeedback = new System.Windows.Forms.MenuItem();
            this.cbTesting = new System.Windows.Forms.CheckBox();
            this.trayIconTrayed = new System.Windows.Forms.NotifyIcon(this.components);
            this.tBarQuickBtns = new System.Windows.Forms.ToolBar();
            this.tBBtrayed = new System.Windows.Forms.ToolBarButton();
            this.blank1 = new System.Windows.Forms.ToolBarButton();
            this.tBBfindCategory = new System.Windows.Forms.ToolBarButton();
            this.tBBfindNext = new System.Windows.Forms.ToolBarButton();
            this.tBBfindItem = new System.Windows.Forms.ToolBarButton();
            this.blank2 = new System.Windows.Forms.ToolBarButton();
            this.tBBexpandNode = new System.Windows.Forms.ToolBarButton();
            this.tBBExpandOnly = new System.Windows.Forms.ToolBarButton();
            this.tBBcollapseNode = new System.Windows.Forms.ToolBarButton();
            this.iListTbarBtns = new System.Windows.Forms.ImageList(this.components);
            this.lblDebugging = new System.Windows.Forms.Label();
            this.ExportItemsDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem10,
            this.menuItem19,
            this.menuItem28,
            this.menuItem37});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuOpenKB,
            this.menuCloseKB,
            this.menuItem41,
            this.menuItemPrint,
            this.menuAdvPrint,
            this.menuPrinterSetup,
            this.menuItem6,
            this.menuTrayed,
            this.menuItem8,
            this.menuExit});
            this.menuItem1.Text = "File";
            // 
            // menuOpenKB
            // 
            this.menuOpenKB.Index = 0;
            this.menuOpenKB.Text = "Open Knowledge DB";
            this.menuOpenKB.Click += new System.EventHandler(this.menuOpenKB_Click);
            // 
            // menuCloseKB
            // 
            this.menuCloseKB.Enabled = false;
            this.menuCloseKB.Index = 1;
            this.menuCloseKB.Text = "Close....";
            this.menuCloseKB.Click += new System.EventHandler(this.menuCloseKB_Click);
            // 
            // menuItem41
            // 
            this.menuItem41.Index = 2;
            this.menuItem41.Text = "-";
            // 
            // menuItemPrint
            // 
            this.menuItemPrint.Enabled = false;
            this.menuItemPrint.Index = 3;
            this.menuItemPrint.Text = "Print Items";
            this.menuItemPrint.Click += new System.EventHandler(this.menuItemPrint_Click);
            // 
            // menuAdvPrint
            // 
            this.menuAdvPrint.Enabled = false;
            this.menuAdvPrint.Index = 4;
            this.menuAdvPrint.Text = "Print Preview...";
            this.menuAdvPrint.Click += new System.EventHandler(this.menuAdvPrint_Click);
            // 
            // menuPrinterSetup
            // 
            this.menuPrinterSetup.Index = 5;
            this.menuPrinterSetup.Text = "Printer Setup...";
            this.menuPrinterSetup.Click += new System.EventHandler(this.menuPrinterSetup_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 6;
            this.menuItem6.Text = "-";
            // 
            // menuTrayed
            // 
            this.menuTrayed.Index = 7;
            this.menuTrayed.Shortcut = System.Windows.Forms.Shortcut.CtrlK;
            this.menuTrayed.Text = "Trayed Mode";
            this.menuTrayed.Click += new System.EventHandler(this.menuTrayed_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 8;
            this.menuItem8.Text = "-";
            // 
            // menuExit
            // 
            this.menuExit.Index = 9;
            this.menuExit.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 1;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuFindCategory,
            this.menuFindItem,
            this.menuFindNext,
            this.menuItem17,
            this.menuAssignTo});
            this.menuItem10.Text = "Edit";
            // 
            // menuFindCategory
            // 
            this.menuFindCategory.Index = 0;
            this.menuFindCategory.Text = "Find Category";
            this.menuFindCategory.Click += new System.EventHandler(this.menuFindCategory_Click);
            // 
            // menuFindItem
            // 
            this.menuFindItem.Index = 1;
            this.menuFindItem.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.menuFindItem.Text = "Find Item/Note";
            this.menuFindItem.Click += new System.EventHandler(this.menuFindItem_Click);
            // 
            // menuFindNext
            // 
            this.menuFindNext.Index = 2;
            this.menuFindNext.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.menuFindNext.Text = "Find Next";
            this.menuFindNext.Click += new System.EventHandler(this.menuFindNext_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 3;
            this.menuItem17.Text = "-";
            // 
            // menuAssignTo
            // 
            this.menuAssignTo.Enabled = false;
            this.menuAssignTo.Index = 4;
            this.menuAssignTo.Text = "Assign To...";
            this.menuAssignTo.Click += new System.EventHandler(this.menuAssignTo_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 2;
            this.menuItem19.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuZoomItem,
            this.menuItem21,
            this.menuCloseItems,
            this.menuItem25,
            this.menuExpandNode,
            this.menuCollapseNode,
            this.menuExpandThis,
            this.menuItem3,
            this.menuExpandAll,
            this.menuCollapseAll});
            this.menuItem19.Text = "View";
            // 
            // menuZoomItem
            // 
            this.menuZoomItem.Enabled = false;
            this.menuZoomItem.Index = 0;
            this.menuZoomItem.Shortcut = System.Windows.Forms.Shortcut.F8;
            this.menuZoomItem.Text = "Zoom Item";
            this.menuZoomItem.Click += new System.EventHandler(this.menuZoomItem_Click);
            // 
            // menuItem21
            // 
            this.menuItem21.Index = 1;
            this.menuItem21.Text = "-";
            // 
            // menuCloseItems
            // 
            this.menuCloseItems.Index = 2;
            this.menuCloseItems.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.menuCloseItems.Text = "Close Items";
            this.menuCloseItems.Click += new System.EventHandler(this.menuCloseItems_Click);
            // 
            // menuItem25
            // 
            this.menuItem25.Index = 3;
            this.menuItem25.Text = "-";
            // 
            // menuExpandNode
            // 
            this.menuExpandNode.Index = 4;
            this.menuExpandNode.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
            this.menuExpandNode.Text = "Expand Category";
            this.menuExpandNode.Click += new System.EventHandler(this.menuExpandNode_Click);
            // 
            // menuCollapseNode
            // 
            this.menuCollapseNode.Index = 5;
            this.menuCollapseNode.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
            this.menuCollapseNode.Text = "Collapse Category";
            this.menuCollapseNode.Click += new System.EventHandler(this.menuCollapseNode_Click);
            // 
            // menuExpandThis
            // 
            this.menuExpandThis.Index = 6;
            this.menuExpandThis.Text = "Expand Only This";
            this.menuExpandThis.Click += new System.EventHandler(this.menuExpandThis_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 7;
            this.menuItem3.Text = "-";
            // 
            // menuExpandAll
            // 
            this.menuExpandAll.Index = 8;
            this.menuExpandAll.Text = "Expand All";
            this.menuExpandAll.Click += new System.EventHandler(this.menuExpandAll_Click);
            // 
            // menuCollapseAll
            // 
            this.menuCollapseAll.Index = 9;
            this.menuCollapseAll.Text = "Collapse All";
            this.menuCollapseAll.Click += new System.EventHandler(this.menuCollapseAll_Click);
            // 
            // menuItem28
            // 
            this.menuItem28.Index = 3;
            this.menuItem28.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuImportItems,
            this.menuExportCSV,
            this.menuItems2email,
            this.menuItem34,
            this.menuNewKDB,
            this.menuItem2,
            this.menuAutoAssign,
            this.menuItem4,
            this.menuSettings});
            this.menuItem28.Text = "Tools";
            // 
            // menuImportItems
            // 
            this.menuImportItems.Enabled = false;
            this.menuImportItems.Index = 0;
            this.menuImportItems.Text = "Import Items...";
            this.menuImportItems.Click += new System.EventHandler(this.menuImportItems_Click);
            // 
            // menuExportCSV
            // 
            this.menuExportCSV.Enabled = false;
            this.menuExportCSV.Index = 1;
            this.menuExportCSV.Text = "Export CSV";
            this.menuExportCSV.Click += new System.EventHandler(this.menuExportCSV_Click);
            // 
            // menuItems2email
            // 
            this.menuItems2email.Enabled = false;
            this.menuItems2email.Index = 2;
            this.menuItems2email.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
            this.menuItems2email.Text = "Email Items";
            this.menuItems2email.Click += new System.EventHandler(this.menuItems2email_Click);
            // 
            // menuItem34
            // 
            this.menuItem34.Index = 3;
            this.menuItem34.Text = "-";
            // 
            // menuNewKDB
            // 
            this.menuNewKDB.Index = 4;
            this.menuNewKDB.Text = "New Knowledge DB";
            this.menuNewKDB.Click += new System.EventHandler(this.menuNewKDB_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 5;
            this.menuItem2.Text = "-";
            // 
            // menuAutoAssign
            // 
            this.menuAutoAssign.Enabled = false;
            this.menuAutoAssign.Index = 6;
            this.menuAutoAssign.Text = "Auto-Assign Items";
            this.menuAutoAssign.Click += new System.EventHandler(this.menuAutoAssign_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 7;
            this.menuItem4.Text = "-";
            // 
            // menuSettings
            // 
            this.menuSettings.Index = 8;
            this.menuSettings.Text = "Settings...";
            this.menuSettings.Click += new System.EventHandler(this.menuSettings_Click);
            // 
            // menuItem37
            // 
            this.menuItem37.Index = 4;
            this.menuItem37.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuOnlineHelp,
            this.menuAbout,
            this.menuShare,
            this.menuFeedback});
            this.menuItem37.Text = "Help";
            // 
            // menuOnlineHelp
            // 
            this.menuOnlineHelp.Index = 0;
            this.menuOnlineHelp.Text = "User Guide";
            this.menuOnlineHelp.Click += new System.EventHandler(this.menuOnlineHelp_Click);
            // 
            // menuAbout
            // 
            this.menuAbout.Index = 1;
            this.menuAbout.Text = "About";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // menuShare
            // 
            this.menuShare.Index = 2;
            this.menuShare.Text = "Share...";
            this.menuShare.Click += new System.EventHandler(this.menuShare_Click);
            // 
            // menuFeedback
            // 
            this.menuFeedback.Index = 3;
            this.menuFeedback.Text = "Feedback...";
            this.menuFeedback.Click += new System.EventHandler(this.menuFeedback_Click);
            // 
            // cbTesting
            // 
            this.cbTesting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTesting.AutoSize = true;
            this.cbTesting.Checked = true;
            this.cbTesting.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTesting.Location = new System.Drawing.Point(1144, 12);
            this.cbTesting.Name = "cbTesting";
            this.cbTesting.Size = new System.Drawing.Size(22, 21);
            this.cbTesting.TabIndex = 1;
            this.cbTesting.UseVisualStyleBackColor = true;
            this.cbTesting.Visible = false;
            this.cbTesting.Click += new System.EventHandler(this.cbTesting_Click);
            // 
            // trayIconTrayed
            // 
            this.trayIconTrayed.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIconTrayed.Icon")));
            this.trayIconTrayed.Click += new System.EventHandler(this.trayIconTrayed_Click);
            this.trayIconTrayed.DoubleClick += new System.EventHandler(this.trayIconTrayed_DoubleClick);
            // 
            // tBarQuickBtns
            // 
            this.tBarQuickBtns.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tBBtrayed,
            this.blank1,
            this.tBBfindCategory,
            this.tBBfindNext,
            this.tBBfindItem,
            this.blank2,
            this.tBBexpandNode,
            this.tBBExpandOnly,
            this.tBBcollapseNode});
            this.tBarQuickBtns.DropDownArrows = true;
            this.tBarQuickBtns.ImageList = this.iListTbarBtns;
            this.tBarQuickBtns.Location = new System.Drawing.Point(0, 0);
            this.tBarQuickBtns.Name = "tBarQuickBtns";
            this.tBarQuickBtns.ShowToolTips = true;
            this.tBarQuickBtns.Size = new System.Drawing.Size(1178, 28);
            this.tBarQuickBtns.TabIndex = 7;
            this.tBarQuickBtns.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tBarQuickBtns_ButtonClick);
            // 
            // tBBtrayed
            // 
            this.tBBtrayed.ImageIndex = 0;
            this.tBBtrayed.Name = "tBBtrayed";
            this.tBBtrayed.ToolTipText = "Tray Mode";
            // 
            // blank1
            // 
            this.blank1.Name = "blank1";
            this.blank1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tBBfindCategory
            // 
            this.tBBfindCategory.ImageIndex = 1;
            this.tBBfindCategory.Name = "tBBfindCategory";
            this.tBBfindCategory.ToolTipText = "Find Category";
            // 
            // tBBfindNext
            // 
            this.tBBfindNext.ImageIndex = 2;
            this.tBBfindNext.Name = "tBBfindNext";
            this.tBBfindNext.ToolTipText = "Find Next";
            // 
            // tBBfindItem
            // 
            this.tBBfindItem.ImageIndex = 3;
            this.tBBfindItem.Name = "tBBfindItem";
            this.tBBfindItem.ToolTipText = "Find Item";
            // 
            // blank2
            // 
            this.blank2.Name = "blank2";
            this.blank2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tBBexpandNode
            // 
            this.tBBexpandNode.ImageIndex = 4;
            this.tBBexpandNode.Name = "tBBexpandNode";
            this.tBBexpandNode.ToolTipText = "Expand";
            // 
            // tBBExpandOnly
            // 
            this.tBBExpandOnly.ImageIndex = 5;
            this.tBBExpandOnly.Name = "tBBExpandOnly";
            this.tBBExpandOnly.ToolTipText = "Expand Only This";
            // 
            // tBBcollapseNode
            // 
            this.tBBcollapseNode.ImageIndex = 6;
            this.tBBcollapseNode.Name = "tBBcollapseNode";
            this.tBBcollapseNode.ToolTipText = "Collapse Node";
            // 
            // iListTbarBtns
            // 
            this.iListTbarBtns.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iListTbarBtns.ImageStream")));
            this.iListTbarBtns.TransparentColor = System.Drawing.Color.Transparent;
            this.iListTbarBtns.Images.SetKeyName(0, "16_tray.ico");
            this.iListTbarBtns.Images.SetKeyName(1, "16_find_category.ico");
            this.iListTbarBtns.Images.SetKeyName(2, "16_find_next.bmp");
            this.iListTbarBtns.Images.SetKeyName(3, "16_find_item.ico");
            this.iListTbarBtns.Images.SetKeyName(4, "16_expand_nodes.ico");
            this.iListTbarBtns.Images.SetKeyName(5, "16_expand_only_this.ico");
            this.iListTbarBtns.Images.SetKeyName(6, "16_collapse_node.ico");
            // 
            // lblDebugging
            // 
            this.lblDebugging.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDebugging.AutoSize = true;
            this.lblDebugging.Location = new System.Drawing.Point(1047, 12);
            this.lblDebugging.Name = "lblDebugging";
            this.lblDebugging.Size = new System.Drawing.Size(91, 20);
            this.lblDebugging.TabIndex = 9;
            this.lblDebugging.Text = "Debugging:";
            this.lblDebugging.Visible = false;
            // 
            // ExportItemsDialog
            // 
            this.ExportItemsDialog.FileName = "exportedItems.csv";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 594);
            this.Controls.Add(this.lblDebugging);
            this.Controls.Add(this.cbTesting);
            this.Controls.Add(this.tBarQuickBtns);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Menu = this.mainMenu1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "orGenta -- (not connected)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.LocationChanged += new System.EventHandler(this.frmMain_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMain_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemPrint;
        private System.Windows.Forms.MenuItem menuAdvPrint;
        private System.Windows.Forms.MenuItem menuPrinterSetup;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuTrayed;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuExit;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuFindCategory;
        private System.Windows.Forms.MenuItem menuFindItem;
        private System.Windows.Forms.MenuItem menuItem17;
        private System.Windows.Forms.MenuItem menuItem19;
        private System.Windows.Forms.MenuItem menuItem21;
        private System.Windows.Forms.MenuItem menuItem25;
        private System.Windows.Forms.MenuItem menuExpandAll;
        private System.Windows.Forms.MenuItem menuCollapseAll;
        private System.Windows.Forms.MenuItem menuItem28;
        private System.Windows.Forms.MenuItem menuItem34;
        private System.Windows.Forms.MenuItem menuItem37;
        private System.Windows.Forms.MenuItem menuAbout;
        private System.Windows.Forms.MenuItem menuOpenKB;
        private System.Windows.Forms.MenuItem menuCloseKB;
        private System.Windows.Forms.MenuItem menuItem41;
        private System.Windows.Forms.MenuItem menuSettings;
        private System.Windows.Forms.CheckBox cbTesting;
        private System.Windows.Forms.NotifyIcon trayIconTrayed;
        private System.Windows.Forms.ToolBar tBarQuickBtns;
        private System.Windows.Forms.ImageList iListTbarBtns;
        private System.Windows.Forms.ToolBarButton tBBtrayed;
        private System.Windows.Forms.ToolBarButton blank1;
        private System.Windows.Forms.ToolBarButton tBBfindCategory;
        private System.Windows.Forms.ToolBarButton tBBfindItem;
        private System.Windows.Forms.ToolBarButton tBBfindNext;
        private System.Windows.Forms.ToolBarButton blank2;
        private System.Windows.Forms.ToolBarButton tBBexpandNode;
        private System.Windows.Forms.ToolBarButton tBBExpandOnly;
        private System.Windows.Forms.ToolBarButton tBBcollapseNode;
        public System.Windows.Forms.MenuItem menuAssignTo;
        public System.Windows.Forms.MenuItem menuZoomItem;
        private System.Windows.Forms.MenuItem menuOnlineHelp;
        private System.Windows.Forms.Label lblDebugging;
        public System.Windows.Forms.MenuItem menuExportCSV;
        private System.Windows.Forms.SaveFileDialog ExportItemsDialog;
        private System.Windows.Forms.MenuItem menuShare;
        private System.Windows.Forms.MenuItem menuFeedback;
        private System.Windows.Forms.MenuItem menuCloseItems;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        public System.Windows.Forms.MenuItem menuFindNext;
        public System.Windows.Forms.MenuItem menuAutoAssign;
        public System.Windows.Forms.MenuItem menuImportItems;
        public System.Windows.Forms.MenuItem menuExpandNode;
        public System.Windows.Forms.MenuItem menuCollapseNode;
        public System.Windows.Forms.MenuItem menuExpandThis;
        public System.Windows.Forms.MainMenu mainMenu1;
        public System.Windows.Forms.MenuItem menuItems2email;
        private System.Windows.Forms.MenuItem menuNewKDB;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}

