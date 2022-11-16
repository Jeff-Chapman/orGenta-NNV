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
        #endregion
        private bool testing = false;

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
        public TreeNode copyingNode;
        public string copyingSourceDB;
        public List<string> KBsOpen = new List<string>();
        public TreeViewForm ActiveTopForm;
        public ItemsForm ActiveTopItems;
        public List<List<string>> TempCatSuppress = new List<List<string>> { };
        public List<string> AutoCreateCats = new List<string> { };
        public IDbConnection localCacheDBconx;
        public bool endOfUserSearch = false;

        // NOTICE: This software is Copyright (c) 2006, 2021 by Jeff D. Chapman
        // Non-networked version licensed as Open Source under [TODO: fill this in]

        public frmMain()
        {
            InitializeComponent();
            this.Height = 650;
            this.Top = 0;
            this.Width = 1000;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            RestoreCoordinates();
            // TODO: place user options in the registry or DB, get at startup

            if (Control.ModifierKeys == Keys.Shift)
            { 
                testing = true;
                cbTesting.Visible = true;
                lblDebugging.Visible = true;
            }

            if (testing)
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
            mySideUtils.Left = this.Right + 10;
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

            if (!firstTimeKB)
                { restoreDefaultDBsettings();}
            else
                { setupBuiltinDBsettings(); }

            // Try to autoconnect first before popping up the user KB conx box
            if (testing) { getDBconnxInfo(); }
            if (!BuildAndValidateDBconx(true))
                { getDBconnxInfo(); }
            else
            {
                if (firstTimeKB) 
                { 
                    SaveDefaultDBtoRegistry(ThisUser);
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
            dbCleanupRoutines();
            mySideUtils.Show();       
            this.Cursor = Cursors.Arrow;
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
                this.Top = Convert.ToInt32(ScreenLoc.GetValue("Top", 1));
                this.Left = Convert.ToInt32(ScreenLoc.GetValue("Left", 1));
                RegistryKey ScreenSize = ThisUser.OpenSubKey("Software\\orGenta\\ScreenSize", true);
                this.Height = Convert.ToInt32(ScreenSize.GetValue("Height", 532));
                if (this.Height < 100)
                    { this.Height = 100; }
                this.Width = Convert.ToInt32(ScreenSize.GetValue("Width", 728));
                if (this.Width < 100)
                    { this.Width = 100; }
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
            tvfMyTreeForm = new TreeViewForm(this);
            tvfMyTreeForm.myDBconx = myDBconx;
            tvfMyTreeForm.DataProvider = DataProvider;
            tvfMyTreeForm.dbVersion = dBversion;
            tvfMyTreeForm.RLockOption = RLockOption;
            tvfMyTreeForm.activeDBname = activeDBname;
            tvfMyTreeForm.RemoteConx = RemoteConx;
            tvfMyTreeForm.BuildCatTree();
  
            tvfMyTreeForm.tvCategories.Nodes[0].Expand();
            tvfMyTreeForm.MdiParent = this;

            tvfMyTreeForm.Text = activeDBname;
            string[] dbParts = activeDBname.Split(new char[] { '\\', '/' });
            int pathLen = dbParts.Length;
            if (pathLen > 1)
                { tvfMyTreeForm.Text = dbParts[pathLen - 2] + "\\" + dbParts[pathLen - 1];}

            tvfMyTreeForm.testing = testing;
            // if (testing) { tvfMyTreeForm.tmrTVdirty.Interval = 50000; }
            tvfMyTreeForm.Show();
        }
 
        private void restoreDefaultDBsettings()
        {
            restoredDBinfo = false;
            RegistryKey ThisUser = Registry.CurrentUser;
            try
            {
                RegistryKey DBsettings = ThisUser.OpenSubKey("Software\\orGenta\\DBsettings", true);
                myServerType = DBsettings.GetValue("ServerType").ToString();
                myServerName = DBsettings.GetValue("ServerName").ToString();
                myKnowledgeDBname = DBsettings.GetValue("DBname").ToString();
                myUserID = DBsettings.GetValue("dbLoginID").ToString();
                DataProvider = DBsettings.GetValue("dataProv").ToString();
                RemoteConx = false; 
                restoredDBinfo = true;
            }
            catch
            {
                // Defaults to the oem MS Access DB
                setupBuiltinDBsettings();
            }
            myPW = "";
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
            }
            // TODO: when doing a findNext, need to return "Main" Node if node is earlier than prior
            return NodeThatMatches;
        }

        private TreeNode Match1Node(TreeNode MatchNode, string PathToFind, string TagToMatch, bool PartialMatch, string PreviousFoundNode)
        {
            if ((MatchNode.FullPath == PathToFind) || ((PartialMatch) &&
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
                if (FoundMatchNode)
                { return NodeThatMatches; }
            }
            return NodeThatMatches;
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                mySideUtils.Left = this.Right + 10;
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
            IDbCommand cmd = ActiveTopForm.myDBconx.CreateCommand();

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
                cmd = ActiveTopForm.myDBconx.CreateCommand();
                cmd.CommandText = chkText;
                int rowsMatch = (int)cmd.ExecuteScalar();
                if (rowsMatch > 0) { continue; }

                //  Add the Rels record for this item and category
                string insRelCmd = "INSERT INTO [Rels] ([CategoryID],[ItemID],[isDeleted]) VALUES (";
                insRelCmd += AssigningCatID + "," + ItemNumber + ",0)";
                cmd.CommandText = insRelCmd;
                int rowsIns = cmd.ExecuteNonQuery();
            }

            // Remove item from trash if it's there
            string TrashRem = "UPDATE Rels SET isDeleted = 1 WHERE (Rels.ItemID = "; 
            TrashRem += ItemNumber + ") AND (Rels.CategoryID = 3)";
            cmd.CommandText = TrashRem;
            int Trashed = cmd.ExecuteNonQuery();
        
        }

        private void AddToAssignCats(TreeNode scanNode, CheckedListBox boxTarget)
        {
            boxTarget.Items.Add(scanNode.FullPath);
  
            // call myself recursively for my children
            foreach (TreeNode childNode in scanNode.Nodes)
                { AddToAssignCats(childNode, boxTarget); }
        }

        private void cbTesting_Click(object sender, EventArgs e)
        {
            if (testing)
                { testing = false; }
            else
                { testing = true; }
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
            RegistryKey ThisUser = Registry.CurrentUser;
            RegistryKey ScreenLoc = ThisUser.CreateSubKey("Software\\orGenta\\ScreenLocation");
            ScreenLoc.SetValue("Top", this.Top);
            ScreenLoc.SetValue("Left", this.Left);
            RegistryKey ScreenSize = ThisUser.CreateSubKey("Software\\orGenta\\ScreenSize");
            ScreenSize.SetValue("Height", this.Height);
            ScreenSize.SetValue("Width", this.Width);
        }

    }
}
