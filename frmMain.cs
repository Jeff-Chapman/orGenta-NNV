using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Microsoft.Win32;

#pragma warning disable IDE1006 // Naming Styles

namespace orGenta_NNv
{
    public partial class frmMain : Form
    {
        #region Private Variables
        private TreeViewForm tvfMyTreeForm;
        private string LogfileName = "ErrorLog.txt";
        private SharedRoutines myErrHandler = new SharedRoutines();
        private SharedRoutines myDBupdater = new SharedRoutines();
        private bool restoredDBinfo;
        private MinimalIntface GetTextLineForm;    
        private bool FoundMatchNode;
        private bool MatchedOnPrevNode;
        private string DataProvider;
        private string RLockOption;
        private bool weHaveAprinterObject = false;
        private AdvancedPrint myPrinter;
        private System.Collections.ArrayList myCatXrefArray = new System.Collections.ArrayList();
        private System.Collections.ArrayList itemIDstoPrint = new System.Collections.ArrayList();
        private string FirstKBdate = "";
        private bool isItOldMSaccess;
        private bool isItSQLite;
        private GlobalKeyboardHook _globalKeyboardHook;
        private bool hasAlt = false;
        #endregion
        
        #region userOptions
        public bool optLongErrMessages = true;
        public int optTVupdateInterval = 10000;
        public int optTVupdateInterval2nd = 30000;
        public bool optWrapMode = true;
        public bool optAdjustItemsToParent = true;
        public bool optCreateCategories = false;
        public bool optHighlightCats = false;
        #endregion

        public SideUtilBox mySideUtils;        
        public bool findReplaying;
        public string lastExecutedSearch;
        public string orgStopWords;
        public bool RunningMinimal;
        public static bool Running64bit;
        public TreeNode copyingNode;
        public string copyingSourceDB;
        public List<string> KBsOpen = new List<string>();
        public List<bool> KBalwaysOpen = new List<bool>();
        public TreeViewForm ActiveTopForm;
        public ItemsForm ActiveTopItems;
        public List<List<string>> TempCatSuppress = new List<List<string>> { };
        public List<string> AutoCreateCats = new List<string> { };
        public IDbConnection localCacheDBconx;
        public bool endOfUserSearch = false;
        public int HighestMRUitem = 0;
        public string[] OpenItemsWindows = new string[] {"","","","","","",""};
        public int[,] ItemWindowLocUsed = new int[7,2]; // element [,0] is used flag [,1] is MRU#

        // NOTICE: This software is Copyright (c) 2006, 2021 by Jeff D. Chapman
        // Non-networked version licensed as Open Source under GNU Lesser General Public License v3.0

        public frmMain()
        {
            InitializeComponent();
            this.Height = 650;
            this.Top = 0;
            this.Width = 1000;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Running64bit = Environment.Is64BitProcess;
            RestoreCoordinates();
            RestoreUserOptions();

            if (Control.ModifierKeys == Keys.Shift)
            { 
                Program.testing = true;
                cbtesting.Visible = true;
                lblDebugging.Visible = true;
            }

            if (Program.testing)
            {
                using (StreamWriter sw = File.AppendText(LogfileName))
                {
                    sw.WriteLine("");
                    string dtStamper = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                    sw.WriteLine("Starting orGenta... " + dtStamper);
                    sw.WriteLine("dotNet Runtime Version: {0}",
                        Environment.Version.ToString());
                    sw.WriteLine(new String('-', 60));
                }
            }

            // Check if user is trying to start a second instance of the software

            bool ok;
            Mutex ownerMutex;
            CheckMutex(out ok, out ownerMutex);

            if (!ok)
            {
                MessageBox.Show("orGenta is already running");
                this.Close();
                Application.Exit();
                return;
            }
            GC.KeepAlive(ownerMutex);

            mySideUtils = new SideUtilBox(this);
            mySideUtils.Left = this.Right - 6;
            mySideUtils.Top = this.Top + 60;

            GetTextLineForm = new MinimalIntface(this);

            connectTolocalCache();

            StreamReader readStops = new StreamReader("orgStopWords.txt");
            orgStopWords = readStops.ReadToEnd();
            readStops.Dispose();

            // first attempt to ever connect to KB?
            bool firstTimeKB = false;
            RegistryKey ThisUser = Registry.CurrentUser;
            try
            {
                RegistryKey DBsettings = ThisUser.OpenSubKey("Software\\orGenta\\1stLogin", true);
                FirstKBdate = DBsettings.GetValue("ConxDate").ToString();
            }
            catch { firstTimeKB = true; }

            if (firstTimeKB) { setupBuiltinDBsettings(); }
            else 
            { 
                openTheAOkbs();
                mySideUtils.Show();
                SetupKeyboardHooks();
                this.Cursor = Cursors.Arrow;
                if (KBsOpen.Count > 0) { return; }
            }

            // Try to autoconnect first before popping up the user KB conx box
            if (Program.testing) { getDBconnxInfo(); }
            if (!BuildAndValidateDBconx(true))
                { getDBconnxInfo(); }
            else
            {
                if (firstTimeKB) 
                {
                    SaveAlwaysOpenKBtoRegistry(ThisUser);
                    alwaysOpenFlag = true;
                    RegistryKey DBsettings = ThisUser.CreateSubKey("Software\\orGenta\\1stLogin");
                    DBsettings.SetValue("ConxDate", DateTime.Now.ToShortDateString());
                    FirstKBdate = DateTime.Now.ToShortDateString();
                }
            }

            if (!dbIsConnected)
            {
                if (!BuildAndValidateDBconx(false))
                {
                    this.Close();
                    Application.Exit();
                    return;
                }
            }

            this.Text = "Orgenta :: " + activeDBname;
            CreateNewTree(myDBconx);
            KBsOpen.Add(activeDBname);
            KBalwaysOpen.Add(alwaysOpenFlag);
            dbCleanupRoutines();

            mySideUtils.Show();
            SetupKeyboardHooks();
            this.Cursor = Cursors.Arrow;
        }

        private void openTheAOkbs()
        {
            alwaysOpenFlag = true;
            RegistryKey ThisUser = Registry.CurrentUser;
            RegistryKey DBsettings = ThisUser.CreateSubKey("Software\\orGenta\\DBsettings");
            int AOcount = Convert.ToInt32(DBsettings.GetValue("AOcount", 0));
            for (int i = 1; i < AOcount + 1; i++)
            {
                string locBack = i.ToString();
                DBsettings = ThisUser.CreateSubKey("Software\\orGenta\\DBsettings\\AO" + locBack.ToString());
                myServerType = DBsettings.GetValue("ServerType").ToString();
                myServerName = DBsettings.GetValue("ServerName").ToString();
                myKnowledgeDBname = DBsettings.GetValue("DBname").ToString();
                if (myKnowledgeDBname == "") { continue; }

                myUserID = DBsettings.GetValue("dbLoginID").ToString();
                DataProvider = DBsettings.GetValue("dataProv").ToString();
                RemoteConx = false;
                if (!BuildAndValidateDBconx(true)) 
                {
                    string cantOpen = myServerName + "\\" + myKnowledgeDBname;
                    MessageBox.Show("Unable to open KB " + cantOpen, "Error!");
                    DBsettings.SetValue("DBname", "");
                }
                if (!dbIsConnected) { continue; }
                CreateNewTree(myDBconx);
                KBsOpen.Add(activeDBname);
                KBalwaysOpen.Add(alwaysOpenFlag);
                dbCleanupRoutines();
            }
            this.Text = "Orgenta :: " + activeDBname;
            alwaysOpenFlag = false;
        }

        public void SetupKeyboardHooks()
        {
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
        }

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardState != GlobalKeyboardHook.KeyboardState.KeyDown) { return; }

            //Console.WriteLine(e.KeyboardData.VirtualCode);
            //Console.WriteLine(e.KeyboardData.Flags);
            //Console.WriteLine("-----");

            if (e.KeyboardData.VirtualCode == GlobalKeyboardHook.VkControl)
            // 162 is the code for the Ctrl key
            {
                hasAlt = true;
                return;
            }

            if (e.KeyboardData.VirtualCode != GlobalKeyboardHook.VkSnapshot)
            // 75 is the code for k
            {
                hasAlt = false;
                return;
            }

            if (hasAlt)
            {
                HandleKhotKey();
                e.Handled = true;
            }
        }

        private void HandleKhotKey()
        {
            this.Opacity = 0;
            mySideUtils.Opacity = 0;
            this.WindowState = FormWindowState.Normal;
            if (!RunningMinimal) { menuTrayed_Click(this, null); }
            this.Opacity = 1;
            mySideUtils.Opacity = 1;
            if (GetTextLineForm.txtDataEntered.Visible) 
            {
                GetTextLineForm.btnRestore_Click(this, null);
                trayIconTrayed_DoubleClick(this, null);
                Application.DoEvents();
                return;
            }
            trayIconTrayed_Click(this, null);
        }

        private void RestoreUserOptions()
        {
            RegistryKey ThisUser = Registry.CurrentUser;
            try
            {
                RegistryKey UserOptions = ThisUser.OpenSubKey("Software\\orGenta\\UserOptions", true);
                optLongErrMessages = Convert.ToBoolean(UserOptions.GetValue("LongErrMessages", 1));
                optTVupdateInterval = Convert.ToInt32(UserOptions.GetValue("TVupdateInterval", 10000)); 
                optTVupdateInterval2nd = Convert.ToInt32(UserOptions.GetValue("TVupdateInterval2nd", 30000));
                optWrapMode = Convert.ToBoolean(UserOptions.GetValue("WrapMode", 1));
                optAdjustItemsToParent = Convert.ToBoolean(UserOptions.GetValue("AdjustItemsToParent", 1));
                optCreateCategories = Convert.ToBoolean(UserOptions.GetValue("CreateCategories", 0));
                optHighlightCats = Convert.ToBoolean(UserOptions.GetValue("HighlightCats", 0));
            }
            catch {}
        }

        [RummageKeepReflectionSafe]
        private static void CheckMutex(out bool ok, out Mutex ownerMutex)
        {
            ownerMutex = new System.Threading.Mutex(true, "orGenta", out ok);
        }

        private void RestoreCoordinates()
        {
            RegistryKey ThisUser = Registry.CurrentUser;
            try
            {
                RegistryKey ScreenLoc = ThisUser.OpenSubKey("Software\\orGenta\\ScreenLocation", true);
                Top = Convert.ToInt32(ScreenLoc.GetValue("Top", 1));
                Left = Convert.ToInt32(ScreenLoc.GetValue("Left", 1));
                RegistryKey ScreenSize = ThisUser.OpenSubKey("Software\\orGenta\\ScreenSize", true);
                Height = Convert.ToInt32(ScreenSize.GetValue("Height", 532));
                if (Height < 300) { Height = 300; }
                Width = Convert.ToInt32(ScreenSize.GetValue("Width", 728));
                if (Width < 300) { Width = 300; }
            }
            catch {}
        }

        private void dbCleanupRoutines()
        {
            RemoveDuplicateRels();
            FixInvalidParentIDs();
        }

        private void FixInvalidParentIDs()
        {
            string catsWithBadParentIDs = "SELECT CategoryID FROM vw_Get_Categories_v2 WHERE (ParentID <> 0) AND (pPid IS NULL)";
            string myRoutineName = "FixInvalidParentIDs";
            string guideErrRecovery = "Unable to read Categories from DB";
            string foundDBfixErrs = "Categories with invalid Parent(s) found...";
            string magicFixitCommand = "UPDATE Categories SET ParentID = 1 WHERE (Categories.CategoryID = ";

            StandardDBfixRoutine(catsWithBadParentIDs, myRoutineName, guideErrRecovery, foundDBfixErrs, magicFixitCommand);

        }

        private void RemoveDuplicateRels()
        {
            string DupeRelsIDs = "SELECT Rels.RelID FROM vw_Highest_Rel_Duplicate HR INNER JOIN ";
            DupeRelsIDs += "Rels ON HR.MaxRel <> Rels.RelID AND HR.CategoryID = Rels.CategoryID ";
            DupeRelsIDs += "AND HR.ItemID = Rels.ItemID";
            string myRoutineName = "RemoveDuplicateRels";
            string guideErrRecovery = "Unable to read Rels from DB";
            string foundDBfixErrs = "Duplicate relationships found...";
            string magicFixitCommand = "Update Rels SET isDeleted = 1 WHERE (Rels.RelID = ";

            StandardDBfixRoutine(DupeRelsIDs, myRoutineName, guideErrRecovery, foundDBfixErrs, magicFixitCommand);
        }

        private void CreateNewTree(IDbConnection myDBconx)
        {
            tvfMyTreeForm = new TreeViewForm(this)
            {
                myDBconx = myDBconx, isOldMSaccess = isItOldMSaccess,
                isSQLlite = isItSQLite, DataProvider = DataProvider,
                dbVersion = dBversion, RLockOption = RLockOption,
                activeDBname = activeDBname, RemoteConx = RemoteConx,
                myServerType = myServerType, myServerName = myServerName,
                myUserID = myUserID
            };

            tvfMyTreeForm.BuildCatTree();
  
            tvfMyTreeForm.tvCategories.Nodes[0].Expand();
            tvfMyTreeForm.MdiParent = this;

            tvfMyTreeForm.Text = activeDBname;
            string[] dbParts = activeDBname.Split(new char[] { '\\', '/' });
            int pathLen = dbParts.Length;
            if (pathLen > 1)
                { tvfMyTreeForm.Text = dbParts[pathLen - 2] + "\\" + dbParts[pathLen - 1];}

            // if (Program.testing) { tvfMyTreeForm.tmrTVdirty.Interval = 50000; }
            tvfMyTreeForm.Show();
        }

        public TreeNode FindNodeInTV(string PathToFind, string TagToMatch, bool PartialMatch, string PreviousFoundNode)
        {
            FoundMatchNode = false;
            MatchedOnPrevNode = false;
            TreeNode NodeThatMatches = ActiveTopForm.tvCategories.Nodes[0];
            foreach (TreeNode MatchNode in ActiveTopForm.tvCategories.Nodes)
            {
                NodeThatMatches = Match1Node(MatchNode, PathToFind, TagToMatch, PartialMatch, PreviousFoundNode);
                if (FoundMatchNode)
                    { return NodeThatMatches; }
                else 
                    { return ActiveTopForm.tvCategories.Nodes[0]; }
            }
            return NodeThatMatches;
        }

        private TreeNode Match1Node(TreeNode MatchNode, string PathToFind, string TagToMatch, bool PartialMatch, string PreviousFoundNode)
        {
            if ((MatchNode.FullPath.ToLower() == PathToFind.ToLower()) || (PartialMatch &&
                (MatchNode.Text.ToLower().IndexOf(PathToFind.ToLower()) >= 0)))
            {
                FoundMatchNode = true; 
 
                //	Sometimes this is a "find next" condition ...
                if (PreviousFoundNode == "")
                    { return MatchNode; }
                else if (PreviousFoundNode == MatchNode.FullPath)
                {
                    MatchedOnPrevNode = true;
                    FoundMatchNode = false;
                }
                else if (MatchedOnPrevNode)
                    { return MatchNode; }
                else
                //	still in process of getting up to the last match
                    { FoundMatchNode = false; }
            }

            // Setting to top node indicates no category match yet
            TreeNode NodeThatMatches = ActiveTopForm.tvCategories.Nodes[0];

            foreach (TreeNode ChildNode in MatchNode.Nodes)
            {
                NodeThatMatches = Match1Node(ChildNode, PathToFind, TagToMatch, PartialMatch, PreviousFoundNode);
                if (FoundMatchNode) { return NodeThatMatches; }
            }
            return NodeThatMatches;
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                mySideUtils.Left = this.Right - 6;
                mySideUtils.Top = this.Top + 60;
            }
            catch { }
        }

        private void frmMain_LocationChanged(object sender, EventArgs e)
        {
            frmMain_SizeChanged(this, null);
        }

        private void tBarQuickBtns_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch (this.tBarQuickBtns.Buttons.IndexOf(e.Button))
            {
                case 0:     // trayed mode
                    menuTrayed_Click(this.tBarQuickBtns, null);
                    break;
                case 2:     // find category
                    menuFindCategory_Click(this.tBarQuickBtns, null);
                    break;
                case 3:     // find next
                    menuFindNext_Click(this.tBarQuickBtns, null);
                    break;
                case 4:     // find item
                    menuFindItem_Click(this.tBarQuickBtns, null);
                    break;
                case 6:     // expand node
                    ActiveTopForm.expandNodeToolStripMenuItem_Click(this.tBarQuickBtns, null);
                    break;
                case 7:     // expand only this
                    ActiveTopForm.expandOnlyThisToolStripMenuItem_Click(this.tBarQuickBtns, null);
                    break;
                case 8:     // collapse node
                    ActiveTopForm.collapseNodeToolStripMenuItem_Click(this.tBarQuickBtns, null);
                    break;
            }

        }

        private bool InitializePrinterSettings()
        {
            if (ActiveTopItems == null)
            {
                string clickMsg = "Please click on a category or an";
                clickMsg += "\nitems window before printing ...";
                MessageBox.Show(clickMsg, "Print Error");
                return false;
            }

            IDbConnection printConx = ActiveTopItems.myDBconx;
            DataGridView myDG = ActiveTopItems.ItemGrid;
            myCatXrefArray.Clear();
            itemIDstoPrint.Clear();
            System.Collections.ArrayList FullPathArray = ActiveTopItems.myParentForm.FullPathList;
            DataTable CatTable = ActiveTopItems.myParentForm.myCategoryTable;
            ActiveTopItems.myParentForm.BuildItemCatXref(ActiveTopItems);
            myCatXrefArray = ActiveTopItems.ItemsCatXrefArray;

            foreach (DataGridViewRow myDGrow in myDG.Rows)
            {
                DataRowView myDGrowView = (DataRowView)myDGrow.DataBoundItem;
                object[] rowValues = myDGrowView.Row.ItemArray;
                string thisItemID = rowValues[3].ToString();
                itemIDstoPrint.Add(thisItemID);
            }

            myPrinter.PrintInitialization(myDG, myCatXrefArray, printConx, itemIDstoPrint);
            return true;
        }

        private void AddCatsForItem(string ItemNumber, CatAssignForm GetCatsForm)
        {
            for (int j = 0; j < GetCatsForm.chkListAssignedCats.CheckedItems.Count; j++)
            {
                // Find matching treenode to retrieve its category ID
                string NodeToFind = GetCatsForm.chkListAssignedCats.CheckedItems[j].ToString();
                TreeNode ParentNode = FindNodeInTV(NodeToFind, "", false, "");
                TreeViewForm.TagStruct UAcatTag = (TreeViewForm.TagStruct)ParentNode.Tag;
                string AssigningCatID = UAcatTag.CatID;

                // Don't assign the item if it's already there though
                string chkText = "SELECT count(*) FROM Rels WHERE ItemID = ";
                chkText += ItemNumber + " AND CategoryID = " + AssigningCatID + " AND isDeleted = 0";
                IDbCommand cmd = ActiveTopForm.myDBconx.CreateCommand();
                cmd.CommandText = chkText;
                int rowsMatch = (int)cmd.ExecuteScalar();
                if (rowsMatch > 0) { continue; }

                //  Add the Rels record for this item and category
                string insRelCmd = "INSERT INTO [Rels] ([CategoryID],[ItemID],[isDeleted]) VALUES (";
                insRelCmd += AssigningCatID + "," + ItemNumber + ",0)";

                int rowsIns = myDBupdater.DBinsert(optLongErrMessages, "frmMain:AddCatsForItem", ActiveTopForm.myDBconx, insRelCmd);
            }

            // Remove item from trash if it's there
            string TrashRem = "UPDATE Rels SET isDeleted = 1 WHERE (Rels.ItemID = "; 
            TrashRem += ItemNumber + ") AND (Rels.CategoryID = 3)";
            int Trashed = myDBupdater.DBupdate(optLongErrMessages, "frmMain:AddCatsForItem", ActiveTopForm.myDBconx, TrashRem);
        }

        private void AddToAssignCats(TreeNode scanNode, CheckedListBox boxTarget)
        {
            boxTarget.Items.Add(scanNode.FullPath);
  
            // call myself recursively for my children
            foreach (TreeNode childNode in scanNode.Nodes)
                { AddToAssignCats(childNode, boxTarget); }
        }

        private void cbtesting_Click(object sender, EventArgs e)
        {
            Program.testing = !Program.testing;
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (mySideUtils.UtilCanBeSeen == true)
                    { mySideUtils.UtilCanBeSeen = false; }
                else 
                { 
                    mySideUtils.ShowMe();
                    mySideUtils.BringToFront();
                }
            }
            catch { }
            this.Activate();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ( this.WindowState == FormWindowState.Minimized )
            {
                this.Opacity = 0;
                mySideUtils.Opacity = 0;
                this.WindowState = FormWindowState.Normal;
                Application.DoEvents();
            }

            RegistryKey ThisUser = Registry.CurrentUser;
            RegistryKey ScreenLoc = ThisUser.CreateSubKey("Software\\orGenta\\ScreenLocation");
            ScreenLoc.SetValue("Top", this.Top);
            ScreenLoc.SetValue("Left", this.Left);
            RegistryKey ScreenSize = ThisUser.CreateSubKey("Software\\orGenta\\ScreenSize");
            ScreenSize.SetValue("Height", this.Height);
            ScreenSize.SetValue("Width", this.Width);

            RegistryKey UserOptions = ThisUser.CreateSubKey("Software\\orGenta\\UserOptions");
            UserOptions.SetValue("LongErrMessages", optLongErrMessages);
            UserOptions.SetValue("TVupdateInterval", optTVupdateInterval);
            UserOptions.SetValue("TVupdateInterval2nd", optTVupdateInterval2nd);
            UserOptions.SetValue("WrapMode", optWrapMode);
            UserOptions.SetValue("AdjustItemsToParent", optAdjustItemsToParent);
            UserOptions.SetValue("CreateCategories", optCreateCategories);
            UserOptions.SetValue("HighlightCats", optHighlightCats);

            Application.Exit();
        }

    }
}
