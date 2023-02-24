using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace orGenta_NNv
{
    public partial class TreeViewForm : Form
    {
        private int oldWidth;
        private bool dirtyTree = false;
        private int tempCatID = 1;
        private bool deleteAfterPasteFlag;
        private Color FrozenColor = Color.BlueViolet;
        private ArrayList origItemArray = new ArrayList();
        private ArrayList newAddedItems = new ArrayList();

        private void AddNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oldWidth = this.Width;
            tbNewNodeName.Text = "";
            btnNewSister.Checked = true;
            btnNewChild.Checked = false;
            this.Width = 520;
            pnlAddNode.Visible = true;
            this.BringToFront();
            tbNewNodeName.Focus();
        }

        private void btnNewOK_Click(object sender, EventArgs e)
        {
            if (tbNewNodeName.Text  == "")
                { pnlAddNode.Visible = false;
                this.Width = oldWidth;
                tvCategories.Focus();
                return;}

            TreeNode newNparent = tvCategories.SelectedNode;
            if (newNparent == null)
            { 
                newNparent = tvCategories.Nodes[0];
                tvCategories.SelectedNode = newNparent;
            }
            if ((btnNewSister.Checked) && (newNparent.Text != "Main"))
                { newNparent = newNparent.Parent; }

            SetupNewNode(newNparent, tbNewNodeName.Text, true);

            tmrTVdirty.Interval = 1000;
 
            pnlAddNode.Visible = false;
            this.Width = oldWidth;
            tvCategories.Focus();
        }

        public TreeNode SetupNewNode(TreeNode newNparent, string nodeText, bool showNewNode)
        {
            TreeNode LoadTreeNode = new TreeNode(nodeText);

            TagStruct thisTag = new TagStruct();
            thisTag.CatID = "t" + tempCatID.ToString();
            tempCatID++;
            thisTag.isFrozen = "0";
            thisTag.ManualAssign = "0";
            thisTag.isDeleted = "0";

            LoadTreeNode.Tag = thisTag;
            LoadTreeNode.ForeColor = SystemColors.ControlText;

            if (tvCategories.SelectedNode.Text == "TrashCan")
                { tvCategories.SelectedNode = tvCategories.Nodes[0].Nodes[0]; }
            int myNodeLoc = newNparent.Nodes.IndexOf(tvCategories.SelectedNode);
            newNparent.Nodes.Insert(myNodeLoc + 1, LoadTreeNode);
            if (showNewNode)
            {
                tvCategories.SelectedNode = LoadTreeNode;
                tvCategories.SelectedNode.EnsureVisible();
                int tvStop = LoadTreeNode.Bounds.Top;
                int tvSleft = LoadTreeNode.Bounds.Left;
                int clLeft = myParentForm.Left + this.Left + tvSleft + 80;
                int clTop = myParentForm.Top + this.Top + tvStop + 140;
                Cursor.Position = new Point(clLeft, clTop);
            }

            FullPathList.Add(LoadTreeNode.FullPath);
            TreeIsDirty(LoadTreeNode, true);

            return LoadTreeNode;
        }

        private void TreeIsDirty(TreeNode thisNode, bool AutoAssignFlag)
        {
            TagStruct myTag = (TagStruct)thisNode.Tag;
            string thisCatID = myTag.CatID;
            myTag.isDirty = "1";
            thisNode.Tag = myTag;

            tmrTVdirty.Enabled = true;
            dirtyTree = true;

            if (AutoAssignFlag)
            {
                // Force any new categories to persist...
                this.Cursor = Cursors.WaitCursor;
                while (catUpdatesActive)
                { System.Threading.Thread.Sleep(500); }
                tmrTVdirty_Tick(this, null);
                this.Cursor = Cursors.Arrow;

                this.Cursor = Cursors.WaitCursor;
                myTag = (TagStruct)thisNode.Tag;
                thisCatID = myTag.CatID;
                ScanForItemsToAssign(thisNode, thisCatID);
                this.Cursor = Cursors.Arrow;
            }
        }

        private void ScanForItemsToAssign(TreeNode thisNode, string thisCatID)
        {
            string myLoadSQL = "SELECT ItemDesc, ItemID ";
            myLoadSQL += "FROM vw_Get_Items_Distinct " + RLockOption;
            myLoadSQL += " WHERE ItemDesc LIKE '%" + thisNode.Text + "%'";
            SharedRoutines DataGrabber = new SharedRoutines();
            DataSet myDS = new DataSet();
            try { myDS = DataGrabber.GetDataFor(DataProvider, myDBconx, myLoadSQL); }
            catch (Exception ex)
            {
                if (testing) { myErrHandler.LogRTerror("ScanForItemsToAssign", ex); }
                MessageBox.Show("Unable to read matching Items from DB", "DB Read Error");
                if (myParentForm.optLongErrMessages)
                { myErrHandler.ShowErrDetails("ScanForItemsToAssign", ex, "DB Read Error"); }
            }

            IDbCommand cmd = myDBconx.CreateCommand();
            DataRowCollection MightMatchCat = myDS.Tables[0].Rows;
            foreach (DataRow chkItemMatch in MightMatchCat)
            {
                string thisItemID = chkItemMatch.ItemArray[1].ToString();
                AddTheRel(thisCatID, cmd, thisItemID, RLockOption);
            }
        }

        private void AddTheRel(string thisCatID, IDbCommand cmd, string thisItemID, string RLockOption)
        {
            // Don't assign if it's already there
            string chkRelCmd = "SELECT COUNT(*) FROM Rels " + RLockOption + "WHERE CategoryID = ";
            chkRelCmd += thisCatID + " AND ItemID = " + thisItemID;
            cmd.CommandText = chkRelCmd;
            int countBack = (int)cmd.ExecuteScalar();
            if (countBack > 0) { return; }
            // Okay to assign Item to Category
            string insRelCmd = "INSERT INTO [Rels] ([CategoryID],[ItemID],[isDeleted]) VALUES (";
            insRelCmd += thisCatID + "," + thisItemID + ",0)";
            cmd.CommandText = insRelCmd;
            int rowsIns = cmd.ExecuteNonQuery();
        }

        private void demoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode thisNode = tvCategories.SelectedNode;
            TreeNode myParent = thisNode.Parent;
            int myOldSis = myParent.Nodes.IndexOf(thisNode) - 1;
            TreeNode mySisNode = myParent.Nodes[myOldSis];
            myParent.Nodes.Remove(thisNode);          
            mySisNode.Nodes.Add(thisNode);
            TreeIsDirty(thisNode, false);
        }

        private void promoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode thisNode = tvCategories.SelectedNode;
            TreeNode myParent = thisNode.Parent;
            myParent.Nodes.Remove(thisNode);
            TreeNode myGrandpa = myParent.Parent;
            int parLoc = myGrandpa.Nodes.IndexOf(myParent);
            myGrandpa.Nodes.Insert(parLoc + 1, thisNode);
            tvCategories.SelectedNode = thisNode;
            TreeIsDirty(thisNode, false);
        }

        private void editNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode editingNode = tvCategories.SelectedNode;
            tbEditingCat.Text = editingNode.Text;
            TagStruct myTag = (TagStruct)editingNode.Tag;
            string ManlAs = myTag.ManualAssign;
            cbManlOnly.Checked = false;
            if (ManlAs == "1") { cbManlOnly.Checked = true; }
            cbReassign.Checked = false;

            oldWidth = this.Width;
            this.Width = 520;
            pnlEditCat.Visible = true;
            tbEditingCat.Focus();
        }

        private void btnOKedit_Click(object sender, EventArgs e)
        {
            TreeNode editingNode = tvCategories.SelectedNode;
            editingNode.Text = tbEditingCat.Text;
            string ManlAs = "0";
            if (cbManlOnly.Checked) { ManlAs = "1"; }
            TagStruct myTag = (TagStruct)editingNode.Tag;
            myTag.ManualAssign = ManlAs;
            editingNode.Tag = myTag;
            
            TreeIsDirty(editingNode, cbReassign.Checked);
            pnlEditCat.Visible = false;
            this.Width = oldWidth;
            tvCategories.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlEditCat.Visible = false;
            this.Width = oldWidth;
            tvCategories.Focus();
        }

        private void deleteNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode deletedNode = tvCategories.SelectedNode;
            string delCat = deletedNode.Text;
            string parentCat = deletedNode.Parent.Text;
            string childAction = "";

            // What should we do if the node to delete has child nodes?

            if (deletedNode.Nodes.Count > 0)
            {
                childAction = getChildAction(delCat, parentCat);
                if (childAction == "cancel") { return; }
            }
            else
            {
                string cMsg = "Sure you want to Delete " + delCat + "?";
                DialogResult confirmDel = MessageBox.Show(cMsg, "Confirm...", MessageBoxButtons.OKCancel);
                if (confirmDel == DialogResult.Cancel) { return; }
            }

            switch (childAction)
            {
                case "":        // singe node delete
                    DeleteOneNode(deletedNode, false);
                    break;
                case "leave":   // delete items but leave children
                    DeleteOneNode(deletedNode, true);
                    break;
                case "delete":  // delete node and all its children
                    foreach (TreeNode childNode in deletedNode.Nodes)
                        { DeleteChildNode(childNode); }
                    DeleteOneNode(deletedNode, false);
                    break;
                case "move":    // promote children, then delete node
                    CategoriesToDelete.Clear();
                    foreach (TreeNode childNode in deletedNode.Nodes)
                        { MoveChildToGparent(childNode); }
                    foreach (object oneNode in CategoriesToDelete)
                    {
                        TreeNode delThis = (TreeNode)oneNode;
                        delThis.Remove();
                    }
                    DeleteOneNode(deletedNode, false);
                    break;
            }

            // Anything else? Check back to orignial vsn to see what i did there

            // Offline and online modes may need to work differently...
            // (Perhaps disallow if both offline and category already persisted)
            // Note: a person may delete a category that hasn't yet been persisted to the DB
 
        }

        private void MoveChildToGparent(TreeNode childNode)
        {
            TreeNode gParentNode = childNode.Parent.Parent;
            CategoriesToDelete.Add(childNode);
            TreeNode cloneChild = (TreeNode)childNode.Clone();
            gParentNode.Nodes.Add(cloneChild);
            TreeIsDirty(cloneChild, false);
        }

        private void DeleteChildNode(TreeNode childNode)
        {
            foreach (TreeNode grandChildNode in childNode.Nodes)
                { DeleteChildNode(grandChildNode); }
            DeleteOneNode(childNode, false);
        }

        private string getChildAction(string delCat, string parentCat)
        {
            DelChoice myDelChoice = new DelChoice();
            string qLab = myDelChoice.tbQuestion.Text;
            myDelChoice.tbQuestion.Text = qLab.Replace("[category]", delCat);
            myDelChoice.tbQuestion.SelectionLength = 0;
            string leaveText = myDelChoice.rbLeave.Text;
            myDelChoice.rbLeave.Text = leaveText.Replace("[category]", delCat);
            string parentText = myDelChoice.rbMove.Text;
            myDelChoice.rbMove.Text = parentText.Replace("[parent]", parentCat);
            SendKeys.Send("{TAB}");
            myDelChoice.btnCancel.Focus();
            myDelChoice.ShowDialog();
            if (myDelChoice.choiceCanceled) { return "cancel"; }
            if (myDelChoice.rbDelete.Checked) { return "delete"; }
            if (myDelChoice.rbLeave.Checked) { return "leave"; }
            if (myDelChoice.rbMove.Checked) { return "move"; }
            return "none";
        }

        private void DeleteOneNode(TreeNode deletedNode, bool ItemsOnly)
        {
            TagStruct dNtg = (TagStruct)deletedNode.Tag;
            string delCatID = dNtg.CatID;

            if (!ItemsOnly)
            {
                dNtg.isDeleted = "1";
                deletedNode.Tag = dNtg;
                TreeIsDirty(deletedNode, false);
                deletedNode.ForeColor = Color.Gray;
                // the node itself gets deleted from the treeview in a timer event...
            }

            // add rels for all its solo-attached items to traschan 

            string TrashInCmd = "INSERT INTO Rels (CategoryID, ItemID, isDeleted) ";
            TrashInCmd += "SELECT 3, II.ItemID, 0 FROM (Rels II INNER JOIN ";
            TrashInCmd += "vw_AssignCount AA ON II.ItemID = AA.ItemID) WHERE (II.CategoryID = ";
            TrashInCmd += delCatID + ") AND (AA.AssignCount = 1)";

            IDbCommand cmd = myDBconx.CreateCommand();
            cmd.CommandText = TrashInCmd;
            int rowsIns = cmd.ExecuteNonQuery();

            // now set delete on all item rels to this category

            string setRelsCmd = "UPDATE Rels SET isDeleted = 1 WHERE (Rels.CategoryID = ";
            setRelsCmd += delCatID + ")";

            cmd.CommandText = setRelsCmd;
            int rowsSet = cmd.ExecuteNonQuery();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This is for copying items and one Category across DBs

            myParentForm.copyingNode = tvCategories.SelectedNode;
            myParentForm.copyingSourceDB = this.Text;
            deleteAfterPasteFlag = false;
        }

        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // This is for moving a category within one DB

            myParentForm.copyingNode = tvCategories.SelectedNode;
            myParentForm.copyingSourceDB = this.Text;
            deleteAfterPasteFlag = true;
        }

        private void pasteBelowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pasteAsChildOrBelow(false);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pasteAsChildOrBelow(true);
        }

        private void pasteAsChildOrBelow(bool AsChild)
        {
            TreeNode TargetForPaste = tvCategories.SelectedNode;
            if (TargetForPaste == myParentForm.copyingNode) { return; }

            if (deleteAfterPasteFlag)
            {
                TreeNode myParent = myParentForm.copyingNode.Parent;
                myParent.Nodes.Remove(myParentForm.copyingNode);
                if (AsChild)
                    { TargetForPaste.Nodes.Add(myParentForm.copyingNode); }
                else
                {
                    TreeNode TargParent = TargetForPaste.Parent;
                    int targLoc = TargParent.Nodes.IndexOf(TargetForPaste);
                    TargParent.Nodes.Insert(targLoc + 1, myParentForm.copyingNode);
                }

                tvCategories.SelectedNode = myParentForm.copyingNode;
                tvCategories.SelectedNode.EnsureVisible();
                TreeIsDirty(myParentForm.copyingNode, false);
                myParentForm.copyingNode = null;
            }
            else
            {
                // This is the paste after a cross-DB copy
                tvCategories.SuspendLayout();
                origItemArray.Clear();
                newAddedItems.Clear();
                string origCatText = myParentForm.copyingNode.Text;
                TreeNode TargetPasted = PasteCatAndItems(myParentForm.copyingNode, TargetForPaste);
                if (TargetPasted != null)
                    { PasteChildDrill(myParentForm.copyingNode, TargetPasted); }
                tvCategories.Nodes.Clear();
                BuildCatTree();
                tvCategories.Nodes[0].Expand();
                tvCategories.ResumeLayout();
                myParentForm.findReplaying = false;
                SearchForCat(origCatText);
                myParentForm.copyingNode = null;
            }
        }

        private void PasteChildDrill(TreeNode CopyingNode, TreeNode newTarget)
        {
            // On a cross DB copy all of the child nodes of this node also get copied 
            if (testing) { Console.WriteLine("PCD: Looping over " + CopyingNode.Text); }
            foreach (TreeNode childNode in CopyingNode.Nodes)
            {
                if (testing) { Console.WriteLine("PCD: Calling PCaI to insert " + childNode.Text + " targeting " + newTarget.Text); }
                TreeNode TargetPasted = PasteCatAndItems(childNode, newTarget);
                if (testing) { Console.WriteLine("PCD: Returned from PCaI with TargetPasted = " + TargetPasted.Text); }
                if (TargetPasted != null)
                    { PasteChildDrill(childNode, TargetPasted); }
            }
        }

        private TreeNode PasteCatAndItems(TreeNode copyingNode, TreeNode TargetForPaste)
        {
            TreeNode catForIncomingItems = TargetForPaste;
            // Create new category node if incoming category differs from existing target
            if (TargetForPaste.Text != copyingNode.Text)
            {
                TreeNode newNode = (TreeNode)copyingNode.Clone();
                TagStruct thisTag = (TagStruct)newNode.Tag;
                thisTag.CatID = "t" + tempCatID.ToString();
                tempCatID++;
                newNode.Tag = thisTag;
                TargetForPaste.Nodes.Add(newNode);
                if (testing) { Console.WriteLine("PCaI: Created " + newNode.Text + " under " + TargetForPaste.Text ); }
                catForIncomingItems = newNode;
                TreeIsDirty(newNode, true);
            }

            // Force any new categories to persist...
            this.Cursor = Cursors.WaitCursor;
            while (catUpdatesActive)
                { System.Threading.Thread.Sleep(500); }
            tmrTVdirty_Tick(this, null);
            this.Cursor = Cursors.Arrow;

            // copy all the items that are coming along too...
            TagStruct newCatTag = (TagStruct)catForIncomingItems.Tag;
            string parentCatForItems = newCatTag.CatID;
            TagStruct oldCatTag = (TagStruct)copyingNode.Tag;
            string oldCatID = oldCatTag.CatID;

            TreeViewForm sourceTV = (TreeViewForm)copyingNode.TreeView.Parent;
            string sourceRlock = sourceTV.RLockOption;
            string myLoadSQL = "SELECT hasNote, ItemDesc, DateCreated, ItemID, CategoryID ";
            myLoadSQL += "FROM vw_Get_Items " + sourceRlock;
            myLoadSQL += " WHERE CategoryID = " + oldCatID + " ORDER BY DateCreated DESC";
            SharedRoutines DataGrabber = new SharedRoutines();
            string sourceDataProv = sourceTV.DataProvider;
            IDbConnection sourceDataConx = sourceTV.myDBconx;

            DataSet myDS = new DataSet();
            try { myDS = DataGrabber.GetDataFor(sourceDataProv, sourceDataConx, myLoadSQL); }
            catch (Exception ex)
            {
                if (testing) { myErrHandler.LogRTerror("PasteCatAndItems", ex); }
                MessageBox.Show("Unable to read source Items from DB", "DB Read Error");
                if (myParentForm.optLongErrMessages)
                { myErrHandler.ShowErrDetails("PasteCatAndItems", ex, "DB Read Error"); }
                return null;
            }

            DataRowCollection IncomingItems = myDS.Tables[0].Rows;
            foreach (DataRow newItemRow in IncomingItems)
            {
                if (testing) { Console.WriteLine("PCaI: Calling AddnewI for catID: " + parentCatForItems + " with: " + newItemRow.ItemArray[1].ToString()); }
                AddnewItemFor(newItemRow, parentCatForItems, sourceDataConx, sourceRlock);
            }
            return catForIncomingItems;
        }

        private void AddnewItemFor(DataRow newItemRow, string parentCatForItems, IDbConnection sourceDataConx,
            string sourceRlock)
        {
            bool newItemHasNote = false;
            IDbCommand cmd = myDBconx.CreateCommand();
            int rowsIns = 0;
            string newItemIDback = "";
            string origItemID = newItemRow.ItemArray[3].ToString();

            // If the item was already added skip re-adding and just add the rel
            int foundExistNewItemloc = origItemArray.IndexOf(origItemID);
            if (foundExistNewItemloc < 0) 
            { 
                OkForNewItem(newItemRow, out newItemHasNote, out cmd, out rowsIns, out newItemIDback);
                origItemArray.Add(origItemID);
                newAddedItems.Add(newItemIDback);
            }
            else {  newItemIDback = newAddedItems[foundExistNewItemloc].ToString(); }

            AddTheRel(parentCatForItems, cmd, newItemIDback, RLockOption);

            if (!newItemHasNote) { return; }
            string GetItemCmd = "SELECT NoteValue FROM Notes " + sourceRlock + "WHERE ItemID = " + origItemID;
            IDbCommand noteCmd = sourceDataConx.CreateCommand();
            noteCmd.CommandText = GetItemCmd;
            string NoteTextToAdd;
            try { NoteTextToAdd = noteCmd.ExecuteScalar().ToString(); }
            catch { return; }

            // save the note in target KB
            string holdNote = myItemCleaner.CleanTheItem(NoteTextToAdd);
            string insNoteCmd = "INSERT INTO [Notes] ([ItemID],[NoteValue]) VALUES (";
            insNoteCmd += newItemIDback + ",'" + holdNote + "')";

            cmd.CommandText = insNoteCmd;
            rowsIns = cmd.ExecuteNonQuery();
        }

        private void OkForNewItem(DataRow newItemRow, out bool newItemHasNote, out IDbCommand cmd, out int rowsIns, out string newItemIDback)
        {
            newItemHasNote = false;
            if (newItemRow.ItemArray[0].ToString() == "1") { newItemHasNote = true; }
            string holdItemText = newItemRow.ItemArray[1].ToString();
            string newItemText = myItemCleaner.CleanTheItem(holdItemText);
            string newItemDate = newItemRow.ItemArray[2].ToString();
            string insItemCmd = "";
            insItemCmd = "INSERT INTO [Items] ([ItemDesc],[hasNote],[isDeleted],[DateCreated]) VALUES (";
            insItemCmd += "'" + newItemText + "',";
            if (newItemHasNote)
            { insItemCmd += "1,0"; }
            else
            { insItemCmd += "0,0"; }
            insItemCmd += ",'" + newItemDate + "')";

            cmd = myDBconx.CreateCommand();
            cmd.CommandText = insItemCmd;
            if (testing) { Console.WriteLine("ANIf: " + insItemCmd); }

            rowsIns = cmd.ExecuteNonQuery();
            if (testing) { Console.WriteLine("ANIf: " + rowsIns.ToString() + " rows inserted"); }

            if (myDBconx.Database != "")
            { cmd.CommandText = "SELECT @@IDENTITY AS NEWROWID"; }
            else
            { cmd.CommandText = "SELECT MAX(ItemID) AS NEWROWID FROM Items"; }

            newItemIDback = cmd.ExecuteScalar().ToString();
        }

        public void expandNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tvCategories.BeginUpdate();
            this.tvCategories.SelectedNode.ExpandAll();
            if (this.tvCategories.SelectedNode.ForeColor != FrozenColor)
                { CollapseFrozenNodes(this.tvCategories.SelectedNode); }
            this.tvCategories.EndUpdate();
            Rectangle nodeLoc = tvCategories.SelectedNode.Bounds;
            Cursor.Position = new Point(myParentForm.Left + this.Left + nodeLoc.Left + 40,
                myParentForm.Top + this.Top + nodeLoc.Top + 140);
        }

        private void CollapseFrozenNodes(TreeNode StartingNode)
        {
            foreach (TreeNode CollapseNode in StartingNode.Nodes)
                { FreezeCollapseCascade(CollapseNode); }
        }

        private void FreezeCollapseCascade(TreeNode CollapseNode)
        {
            if (CollapseNode.ForeColor == FrozenColor)
                { CollapseNode.Collapse(); }
            else
            {
                foreach (TreeNode ChildCollapseNode in CollapseNode.Nodes)
                    { FreezeCollapseCascade(ChildCollapseNode); }
            }
        }

        public void collapseNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            CollapseNodeCascade(this.tvCategories.SelectedNode);
            this.Cursor = Cursors.Default;
            Rectangle nodeLoc = tvCategories.SelectedNode.Bounds;
            Cursor.Position = new Point(myParentForm.Left + this.Left + nodeLoc.Left + 40,
                myParentForm.Top + this.Top + nodeLoc.Top + 140);
        }

        private void CollapseNodeCascade(TreeNode NodeToCollapse)
        {
            foreach (TreeNode ChildToCollaspe in NodeToCollapse.Nodes)
                { CollapseNodeCascade(ChildToCollaspe); }
            NodeToCollapse.Collapse();
        }

        public void expandOnlyThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode SaveMyNode;
            try
                { SaveMyNode = this.tvCategories.SelectedNode; }
            catch
                { return; }

            this.tvCategories.CollapseAll();
            this.tvCategories.Nodes[0].Expand();

            this.tvCategories.SelectedNode = SaveMyNode;
            SaveMyNode.EnsureVisible();
            SaveMyNode.Expand();
            Rectangle nodeLoc = tvCategories.SelectedNode.Bounds;
            Cursor.Position = new Point(myParentForm.Left + this.Left + nodeLoc.Left + 40,
                myParentForm.Top + this.Top + nodeLoc.Top + 140);
        }

        private void freezeClosedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode freezeNode = tvCategories.SelectedNode;
            TagStruct myTag = (TagStruct)freezeNode.Tag;
            string FreezeFlag = myTag.isFrozen;
            if (FreezeFlag == "0")
                {FreezeFlag = "1";
                freezeNode.ForeColor = FrozenColor;}
            else
                {FreezeFlag = "0";
                freezeNode.ForeColor = SystemColors.ControlText;}
            myTag.isFrozen = FreezeFlag;
            freezeNode.Tag = myTag;
            
            TreeIsDirty(freezeNode, false);
        }

    }
}
