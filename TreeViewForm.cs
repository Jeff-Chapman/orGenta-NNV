using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace orGenta_NNv
{
    public partial class TreeViewForm : Form
    {
        #region Private Variables
        private string getCatsSQL;
        private DataTable matchingItems = new DataTable();
        private string formerParentID;
        private TreeNode formerParentNode;
        private bool expandingNodeFlag;
        private bool collapsingNodeFlag;
        private SharedRoutines myErrHandler = new SharedRoutines();
        private bool catUpdatesActive;
        private SharedRoutines myItemCleaner;
        private ListBox sUtil;
        private ListBox sUtilCat;
        private bool DBdownFlag = false;
        private List<TreeNode> UnloadedNodes = new List<TreeNode>();
        private List<string> UnloadedParentIDs = new List<string>();
        #endregion          
        
        public struct TagStruct
        {
            public string CatID;
            public string isFrozen;
            public string ManualAssign;
            public string isDirty;
            public string isDeleted;
        }

        public bool testing;
        public IDbConnection myDBconx;
        public bool RemoteConx;
        public frmMain myParentForm;
        public string DataProvider;
        public Version dbVersion;
        public string RLockOption;
        public string activeDBname;
        public System.Collections.ArrayList FullPathList = new System.Collections.ArrayList();
        public DataTable myCategoryTable = new DataTable();
        public ArrayList CategoriesToDelete = new ArrayList();

        public TreeViewForm(frmMain parent)
        {
            InitializeComponent();
            myParentForm = parent;
            myItemCleaner = new SharedRoutines();
        }

        private void TreeViewForm_Activated(object sender, EventArgs e)
        {
            myParentForm.Text = "Orgenta :: " + activeDBname;
            myParentForm.ActiveTopForm = this;
            myParentForm.Update();
            sUtilCat = myParentForm.mySideUtils.lbCatList;
            sUtilCat.Visible = false;
            myParentForm.mySideUtils.Text = "Open Windows";
            tmrTVdirty.Enabled = true;
        }

        public void BuildCatTree()
        {
            //activeDBname = myDBconx.Database;  <-- useful when debugging

            getCatsSQL = "SELECT CategoryID, Category, SortSeq, ParentID, isFrozen, isDeleted, ManualAssignOnly, ";
            getCatsSQL += "pPid FROM [vw_Get_Categories_v2] " + RLockOption + "ORDER BY pPid, SortSeq";

            myCategoryTable.Clear();
            UnloadedNodes.Clear();
            UnloadedParentIDs.Clear();

            SharedRoutines DataGrabber = new SharedRoutines();
            try
            {
                DataSet myDS = DataGrabber.GetDataFor(DataProvider, myDBconx, getCatsSQL);
                myCategoryTable = myDS.Tables[0];
            }
            catch (Exception ex)
            {
                if (testing) { myErrHandler.LogRTerror("BuildCatTree", ex); }
                MessageBox.Show("Unable to read Categories from DB", "DB Read Error");
                if (myParentForm.optLongErrMessages)
                { myErrHandler.ShowErrDetails("BuildCatTree", ex, "DB Read Error"); }
            }

            foreach (DataRow catRow in myCategoryTable.Rows)
                { AddNodeToTV(catRow); }

            if (UnloadedNodes.Count > 0)
            {
                
                int nc = 0;
                foreach (TreeNode ThisNode in UnloadedNodes)
                { 
                    LoadSavedNode(ThisNode, UnloadedParentIDs[nc]);
                    nc++;
                }
            }
        }

        private void LoadSavedNode(TreeNode ThisNode, string parentID)
        {
            TreeNode parentNode = findNodeParent(parentID);
            if (parentNode == null)
            {
                // This happens if grandparent isn't loaded yet or if TVdirty failed...
                parentNode = tvCategories.Nodes[0];
            }
            parentNode.Nodes.Add(ThisNode);
        }

        private void AddNodeToTV(DataRow catRow)
        {
            TreeNode LoadTreeNode = new TreeNode(catRow[1].ToString());

            TagStruct thisTag = new TagStruct();
            thisTag.CatID = catRow[0].ToString();
            string frozTF = catRow[4].ToString();
            if (frozTF.ToLower() == "true")
                { thisTag.isFrozen = "1"; }
            else 
                { thisTag.isFrozen = "0"; }
            string manlTF = catRow[6].ToString();
            if (manlTF.ToLower() == "true")
                { thisTag.ManualAssign = "1"; }
            else
                { thisTag.ManualAssign = "0"; }
            thisTag.isDeleted = "0";
            LoadTreeNode.Tag = thisTag;

            if (thisTag.isFrozen == "1")
                { LoadTreeNode.ForeColor = FrozenColor; }
            else
                { LoadTreeNode.ForeColor = SystemColors.ControlText; }

            string parentID = catRow[3].ToString();

            if (parentID == "0")    // The Main Node
            {
                tvCategories.Nodes.Add(LoadTreeNode);
            }
            else
            {
                if (parentID == "")
                    { tvCategories.Nodes.Add(LoadTreeNode); }
                else
                {
                    TreeNode parentNode = findNodeParent(parentID);
                    if (parentNode == null)
                    {
                        // This happens if grandparent isn't loaded yet or if TVdirty failed...
                        UnloadedNodes.Add(LoadTreeNode);
                        UnloadedParentIDs.Add(parentID);
                        return;
                    }
                    parentNode.Nodes.Add(LoadTreeNode);
                }
            }
            FullPathList.Add(LoadTreeNode.FullPath);
        }

        private TreeNode findNodeParent(string parentID)
        {
            if (parentID == formerParentID) { return formerParentNode; }

            TreeNodeCollection EntireTree = tvCategories.Nodes;
            formerParentID = parentID;

            return RecursiveNodeSearch(parentID, EntireTree);
        }

        private TreeNode RecursiveNodeSearch(string parentID, TreeNodeCollection EntireTree)
        {
            foreach (TreeNode ExaminingNode in EntireTree)
            {
                TagStruct checkTag = (TagStruct)ExaminingNode.Tag;
                if (checkTag.CatID == parentID)
                {
                    formerParentNode = ExaminingNode;
                    return ExaminingNode;
                }
                else
                {
                    TreeNode ChildPcheck = RecursiveNodeSearch(parentID, ExaminingNode.Nodes);
                    if (ChildPcheck != null) { return ChildPcheck; }
                }
            }
            return null;
        }

        private void SetContextMenu()
        {
            TreeNode thisNode = tvCategories.SelectedNode;
            if ((thisNode.Text == "TrashCan") || (thisNode.ForeColor == Color.Gray))
            {
                contextMenuStrip1.Enabled = false;
                return;
            }

            contextMenuStrip1.Enabled = true;
            if ((thisNode.Text == "Main") || (thisNode.Text == "Unassigned"))
            {
                editNodeToolStripMenuItem.Enabled = false;
                deleteNodeToolStripMenuItem.Enabled = false;
                demoteToolStripMenuItem.Enabled = false;
                promoteToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                pasteToolStripMenuItem.Enabled = false;
                moveToolStripMenuItem.Enabled = false;
                freezeClosedToolStripMenuItem.Enabled = false;
                return;
            }

            editNodeToolStripMenuItem.Enabled = true;
            deleteNodeToolStripMenuItem.Enabled = true;
            demoteToolStripMenuItem.Enabled = true;
            copyToolStripMenuItem.Enabled = true;      // TODO: crossDB copy: enable in future release
            pasteToolStripMenuItem.Enabled = true;
            moveToolStripMenuItem.Enabled = true;
            freezeClosedToolStripMenuItem.Enabled = true;

            if (myParentForm.KBsOpen.Count == 1) { copyToolStripMenuItem.Enabled = false; }
 
            if (thisNode.Parent.Text == "Main")
                { promoteToolStripMenuItem.Enabled = false; }
            else
                { promoteToolStripMenuItem.Enabled = true; }

            TreeNode myParent = thisNode.Parent;
            if (myParent.Nodes.IndexOf(thisNode) == 0)
                { demoteToolStripMenuItem.Enabled = false; }
            else
                { demoteToolStripMenuItem.Enabled = true; }

            if (myParentForm.copyingNode == null) 
                { pasteToolStripMenuItem.Enabled = false; }
            else 
                { pasteToolStripMenuItem.Enabled = true; }

            if (thisNode.ForeColor == FrozenColor)
                { freezeClosedToolStripMenuItem.Text = "Unfreeze"; }
            else
                { freezeClosedToolStripMenuItem.Text = "Freeze Closed"; }

            expandOnlyThisToolStripMenuItem.Enabled = true;
            myParentForm.menuExpandThis.Enabled = true;
            if (thisNode.Nodes.Count == 0)
            {
                expandOnlyThisToolStripMenuItem.Enabled = false;
                myParentForm.menuExpandThis.Enabled = false;
            }
            
            if ((thisNode.Nodes.Count == 0) || (thisNode.IsExpanded))
            { 
                expandNodeToolStripMenuItem.Enabled = false;
                myParentForm.menuExpandNode.Enabled = false;
            }
            else
            { 
                expandNodeToolStripMenuItem.Enabled = true;
                myParentForm.menuExpandNode.Enabled = true;
            }

            collapseNodeToolStripMenuItem.Enabled = false;
            myParentForm.menuCollapseNode.Enabled = false;
            if (thisNode.IsExpanded) 
            { 
                collapseNodeToolStripMenuItem.Enabled = true;
                myParentForm.menuCollapseNode.Enabled = true;
            }
        }

        private void tvCategories_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            if ((pnlAddNode.Visible) || (pnlEditCat.Visible)) { return; }
            tvCategories.SelectedNode = e.Node;
            SetContextMenu();
        }

        public void tvCategories_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SetContextMenu();
            if (e.Button != MouseButtons.Left) { return; }
            if (collapsingNodeFlag || expandingNodeFlag) { return; }

            int myYloc = e.Y;
            int myRightBorder = this.Left + this.Width;

            // TODO: don't reload form if already shown, just give it focus

            ItemsForm myItemForm = new ItemsForm(this);
            myItemForm.MdiParent = myParentForm;
            myItemForm.Top = myYloc + this.Top;
            myItemForm.Left = myRightBorder + 6;
            string CatToShow = e.Node.Text;
            myItemForm.Text = activeDBname + " :: " + CatToShow;

            TagStruct myTag = (TagStruct)e.Node.Tag;

            string myCatID = myTag.CatID;
            myItemForm.categoryID = myCatID;
            myItemForm.myDBconx = myDBconx;
            myItemForm.DataProvider = DataProvider;
            myItemForm.RLockOption = RLockOption;

            myItemForm.Show();
            String thisItem = CatToShow;
            sUtil.Items.Add(thisItem);

            BuildItemCatXref(myItemForm);

            this.Focus();
            tvCategories.SelectedNode = e.Node;
            Cursor.Position = new Point(myParentForm.Left + myItemForm.Left + 80, myParentForm.Top + myItemForm.Top + 100);
            Application.DoEvents();
            myItemForm.Focus();
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
        }

        public void BuildItemCatXref(ItemsForm myItemForm)
        {
            // build the ItemsCatXrefArray, the cross-ref between data rows and category fullpaths

            myItemForm.ItemsCatXrefArray.Clear();
            string ThisPath = "";

            foreach (DataGridViewRow myDGrow in myItemForm.ItemGrid.Rows)
            {
                DataRowView myDGrowView = (DataRowView)myDGrow.DataBoundItem;
                object[] rowValues = myDGrowView.Row.ItemArray;
                string thisCatID = rowValues[4].ToString();
                if (thisCatID == "") { thisCatID = myItemForm.SearchCats[myDGrow.Index].ToString(); }

                // find the fullPath for thisCatID by matching offset in CatTable
                int indexOfcat;
                for (int rn = 0; rn < myCategoryTable.Rows.Count; rn++)
                {
                    DataRow myRow = myCategoryTable.Rows[rn];
                    string TableCatID = myRow.ItemArray[0].ToString();
                    if (TableCatID == thisCatID)
                    {
                        indexOfcat = rn;
                        ThisPath = FullPathList[indexOfcat].ToString();
                        break;
                    }
                }
                myItemForm.ItemsCatXrefArray.Add(ThisPath);
            }
        }

        public void tmrTVdirty_Tick(object sender, EventArgs e)
        {
            // this is where we persist modified categories to the DB
            if (!dirtyTree)
            {
                tmrTVdirty.Interval = myParentForm.optTVupdateInterval;
                return;
            }
            tmrTVdirty.Enabled = false;
            dirtyTree = false;
            catUpdatesActive = true;
            TreeNodeCollection EntireTree = tvCategories.Nodes;
            CategoriesToDelete.Clear();
            RecursiveNodeUpdate(EntireTree);
            // Finally delete the nodes in the CategoriesToDelete array
            foreach (object oneNode in CategoriesToDelete)
            {
                TreeNode delThis = (TreeNode)oneNode;
                delThis.Remove();
            }
            catUpdatesActive = false;
            tmrTVdirty.Interval = myParentForm.optTVupdateInterval2nd;
            tmrTVdirty.Enabled = true;
        }

        private void RecursiveNodeUpdate(TreeNodeCollection EntireTree)
        {
            foreach (TreeNode ExaminingNode in EntireTree)
            {
                if (ExaminingNode == null) { return; }
                RecursiveNodeUpdate(ExaminingNode.Nodes);                
                TagStruct checkTag = (TagStruct)ExaminingNode.Tag;
                if (checkTag.isDirty == "1")
                {
                    PersistThisNode(ExaminingNode);
                }
            }
        }

        private void PersistThisNode(TreeNode ExaminingNode)
        {
            TagStruct ParentTag = (TagStruct)ExaminingNode.Parent.Tag;
            string pIDnum = ParentTag.CatID;
            if (pIDnum.Substring(0, 1) == "t") { return; }

            TagStruct checkTag = (TagStruct)ExaminingNode.Tag; 
            int CatID;
            try { CatID = Convert.ToInt32(checkTag.CatID); }
            catch { CatID = 0; }
            getCatsSQL = "SELECT [CategoryID],[Category],[SortSeq],[ParentID],[isFrozen],[isDeleted],";
            getCatsSQL += "[ManualAssignOnly] FROM [Categories] WHERE [CategoryID] = ";
            getCatsSQL += CatID.ToString() + ";";

            myCategoryTable.Clear();

            SharedRoutines DataGrabber = new SharedRoutines();
            try
            {
                DataSet myDS = DataGrabber.GetDataFor(DataProvider, myDBconx, getCatsSQL);
                myCategoryTable = myDS.Tables[0];
            }
            catch
            {
                AlertDBisDown();
                PersistNodeToCache(ExaminingNode);
                return;
            }

            try
            {
                if (myCategoryTable.Rows.Count == 0)
                {
                    string newIDback = PersistNewCat(ExaminingNode);
                    checkTag.CatID = newIDback;
                }
                else
                    { UpdateExistingCat(ExaminingNode); }
            }
            catch { return;  }

            checkTag.isDirty = "0";
            ExaminingNode.Tag = checkTag;
        }

        private void PersistNodeToCache(TreeNode ExaminingNode)
        {
            // TODO: cache node info to a file
            return;
        }

        private void AlertDBisDown()
        {
            pbDBisDown.Visible = true;
            DBdownFlag = true;
        }

        private void UpdateExistingCat(TreeNode ExaminingNode)
        {
            string catName = ExaminingNode.Text;
            string updCmdSQL = "UPDATE [Categories] SET [Category] = '" + catName + "'";
            TreeNodeCollection SisterNodes = ExaminingNode.Parent.Nodes;
            int sortOrd = SisterNodes.IndexOf(ExaminingNode);
            updCmdSQL += ",[SortSeq] = " + sortOrd.ToString();
            TagStruct ParentTag = (TagStruct)ExaminingNode.Parent.Tag;
            string pIDnum = ParentTag.CatID;
            updCmdSQL += ",[ParentID] = " + pIDnum;
            TagStruct myTag = (TagStruct)ExaminingNode.Tag;
            string frozBit = myTag.isFrozen;
            updCmdSQL += ",[isFrozen] = " + frozBit;
            string deleteBit = myTag.isDeleted;
            updCmdSQL += ",[isDeleted] = " + deleteBit;
            string manlAssignbit = myTag.ManualAssign;
            updCmdSQL += ",[ManualAssignOnly] = " + manlAssignbit;
            string myCatID = myTag.CatID;
            updCmdSQL += " WHERE [CategoryID] = " + myCatID;

            IDbCommand cmd = myDBconx.CreateCommand();
            cmd.CommandText = updCmdSQL;
            int rowsUpd = cmd.ExecuteNonQuery();

            if (deleteBit == "1") { CategoriesToDelete.Add(ExaminingNode); }
         }

        private string PersistNewCat(TreeNode ExaminingNode)
        {
            string catName = ExaminingNode.Text;
            string insCmdSQL = "INSERT INTO [Categories] ([Category],[SortSeq],[ParentID],";
            insCmdSQL += "[isFrozen],[isDeleted],[ManualAssignOnly]) VALUES (";
            insCmdSQL += "'" + catName + "',";

            TreeNodeCollection SisterNodes = ExaminingNode.Parent.Nodes;
            int sortOrd = SisterNodes.IndexOf(ExaminingNode);
            insCmdSQL += sortOrd.ToString() + ",";

            TagStruct ParentTag = (TagStruct)ExaminingNode.Parent.Tag;
            string pIDnum = ParentTag.CatID;
            insCmdSQL += pIDnum + ",";

            TagStruct myTag = (TagStruct)ExaminingNode.Tag;
            string frozBit = myTag.isFrozen;
            string deleteBit = myTag.isDeleted;            
            string manlAssignbit = myTag.ManualAssign;            
            insCmdSQL += frozBit + "," + deleteBit + "," + manlAssignbit + ")";

            IDbCommand cmd = myDBconx.CreateCommand();
            cmd.CommandText = insCmdSQL;
            int rowsIns = cmd.ExecuteNonQuery();

            if (myDBconx.Database != "")
                { cmd.CommandText = "SELECT @@IDENTITY AS NEWROWID"; }
            else
                { cmd.CommandText = "SELECT MAX(CategoryID) AS NEWROWID FROM Categories"; }
            string newIDback = cmd.ExecuteScalar().ToString();

            if (manlAssignbit == "0")
            {
                try { AssignItemsToCat(catName, newIDback, true); }
                catch (Exception ex)
                {
                    if (testing) { myErrHandler.LogRTerror("PersistNewCat:AssignItemsToCat", ex); }
                }
            }
            this.Cursor = Cursors.Arrow;

            return newIDback;
        }

        private void AssignItemsToCat(string catName, string newIDback, bool foreGround)
        {
            // Note: this is a foreground process after a new category gets added
            // background process though if the manual assign flag gets turned off

            if (foreGround) { this.Cursor = Cursors.WaitCursor; }

            string getItemsSQL = "SELECT ItemID, ItemDesc FROM vw_Get_Solo_Items " + RLockOption;
            getItemsSQL += " WHERE ItemDesc LIKE '%" + catName + "%'";

            matchingItems.Clear();

            SharedRoutines DataGrabber = new SharedRoutines();
            // TODO: this needs a try-catch if DB goes down
            DataSet myDS = DataGrabber.GetDataFor(DataProvider, myDBconx, getItemsSQL);
            matchingItems = myDS.Tables[0];

            foreach (DataRow ItemRow in matchingItems.Rows)
                { BuildRelation(ItemRow, newIDback); }
        }

        private void BuildRelation(DataRow ItemRow, string newIDback)
        {
            string myItemID = ItemRow.ItemArray[0].ToString();

            string insCmdSQL = "INSERT INTO [Rels] ([CategoryID],[ItemID]) VALUES (";
            insCmdSQL += newIDback +  ", " + myItemID + ")";

            IDbCommand cmd = myDBconx.CreateCommand();
            cmd.CommandText = insCmdSQL;
            int rowsIns = cmd.ExecuteNonQuery();

            // Remove items from "Unassigned" or "TrashCan" category if it's there. 
            string delRelCmd = "UPDATE [Rels] SET isDeleted = 1 WHERE [CategoryID] IN (2,3) ";
            delRelCmd += "AND [ItemID] = " + myItemID;
            cmd.CommandText = delRelCmd;
            int countBack = (int)cmd.ExecuteNonQuery();
        }

        private void tvCategories_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            expandingNodeFlag = true;
        }

        private void tvCategories_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            collapsingNodeFlag = true;
        }

        private void tvCategories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            expandingNodeFlag = false;
            collapsingNodeFlag = false;
        }

        private void TreeViewForm_Load(object sender, EventArgs e)
        {
            sUtil = myParentForm.mySideUtils.lbOpenWindows;
            String sUWindowName = Text + " (KB)";
            sUtil.Items.Add(sUWindowName);
        }

        private void aboutThisKBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string KBnameTitle = "About " + activeDBname;
            string KBinfo = DataProvider + "\n\r";
            KBinfo += myDBconx.ConnectionString + "\n\r";
            KBinfo += "Database Version: " + dbVersion.ToString();
            MessageBox.Show(KBinfo, KBnameTitle);
        }

        private void TreeViewForm_MouseEnter(object sender, EventArgs e)
        {
            if (this == myParentForm.ActiveTopForm) { this.Activate(); }
        }

        private void TreeViewForm_Deactivate(object sender, EventArgs e)
        {
            tmrTVdirty.Enabled = false;
        }

        public bool ItemSearch(string SearchItem)
        {
            return SearchForItem(SearchItem);
        }

        public void ItemSearch()
        {
            SearchForm mySearchForm = new SearchForm();
            mySearchForm.Top = myParentForm.Top + 150;
            mySearchForm.Left = myParentForm.Left + 80;
            mySearchForm.lblSearchType.Text = " Searching Items -->";
            if (mySearchForm.ShowDialog(this) == DialogResult.Cancel) { return; }

            string ItemToFind = mySearchForm.tbSearchFor.Text;
            if (ItemToFind == "") { return; }

            bool foundit = SearchForItem(ItemToFind);
        }

        private bool SearchForItem(string ItemToFind)
        {
            myParentForm.menuFindNext.Enabled = false;

            ItemsForm myItemForm = new ItemsForm(this, ItemToFind);
            myItemForm.MdiParent = myParentForm;
            myItemForm.Top = 50;
            myItemForm.Left = 300;
            myItemForm.Text = activeDBname + " :: Search - " + ItemToFind;

            myItemForm.categoryID = null;
            myItemForm.myDBconx = myDBconx;
            myItemForm.DataProvider = DataProvider;
            Application.DoEvents();

            try { 
                myItemForm.Show(); 
                return true;
            }
            catch 
            { 
                tvCategories.Focus(); 
                return false;
            }
        }

        public void CatSearch(string replaySearch)
        {
            SearchForCat(replaySearch);
        }

        public void CatSearch()
        {
            SearchForm mySearchForm = new SearchForm();
            mySearchForm.Top = myParentForm.Top + 150;
            mySearchForm.Left = myParentForm.Left + 80;
            mySearchForm.lblSearchType.Text = "<-- Searching Categories";
            if (mySearchForm.ShowDialog(this) == DialogResult.Cancel) { return; }

            string CatToFind = mySearchForm.tbSearchFor.Text;
            if (CatToFind == "") { return; }

            SearchForCat(CatToFind);
        }

        private void SearchForCat(string CatToFind)
        {
            myParentForm.menuFindNext.Enabled = true;
            TreeNode FoundNode;
            if (myParentForm.findReplaying)
            {
                FoundNode = myParentForm.FindNodeInTV(CatToFind, "", true,
                    tvCategories.SelectedNode.FullPath);
            }
            else
                { FoundNode = myParentForm.FindNodeInTV(CatToFind, "", true, ""); }

            myParentForm.lastExecutedSearch = CatToFind;
            FoundNode.EnsureVisible();
            if ((FoundNode.Text == "Main") && (!myParentForm.RunningMinimal))
            { MessageBox.Show(this, "Category Not Found"); }
            tvCategories.SelectedNode = FoundNode;
            tvCategories.Focus();
            Rectangle nodeLoc = tvCategories.SelectedNode.Bounds;
            Cursor.Position = new Point(myParentForm.Left + this.Left + nodeLoc.Left + 40, 
                myParentForm.Top + this.Top + nodeLoc.Top + 140);
        }

        private void TreeViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            while (catUpdatesActive)
                { System.Threading.Thread.Sleep(500);}

            // TODO: if any dirty nodes manually trigger one more persist call, then 
            // persist any still dirty nodes to local cache

            tmrTVdirty_Tick(this, null);
            this.Cursor = Cursors.Arrow;

            Form[] myItemForms = myParentForm.MdiChildren;
            foreach (Form chkForm in myItemForms)
            {
                string thisFormType = chkForm.GetType().ToString();
                if (thisFormType != "orGenta.ItemsForm") { continue; }
                ItemsForm chkIp = (ItemsForm)chkForm;
                if (chkIp.myParentForm == this)
                    { chkForm.Close(); }
            }

            sUtil = myParentForm.mySideUtils.lbOpenWindows;
            String sUWindowName = Text + " (KB)";
            sUtil.Items.Remove(sUWindowName);
        }


    }
}
