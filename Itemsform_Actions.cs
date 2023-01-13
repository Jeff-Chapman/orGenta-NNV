using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace orGenta_NNv
{
    public partial class ItemsForm : Form
    {
        private bool DeleteWasCanceled = false;
        private bool AlreadyDeleted = false;
        private Point igMouseDownLoc;
        private Point igMouseUpLoc;
  
        private void label1_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            tbNewItem.Focus();
        }

        private void tbNewItem_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            tbNewItem.Focus();
        }

        private void tbNewItem_TextChanged(object sender, EventArgs e)
        {
            this.btnNote.Enabled = true;
            this.btnOK.Enabled = true;
            label1.Visible = false;
        }

        public void btnOK_Click(object sender, EventArgs e)
        {
            // persist to storage
            itemGotSavedToDB = false;
            itemGotSavedToCache = false;
            ItemSaveRoutine();

            if (FormIsShadow) { return; }

            //  refresh the gridview    
            if (itemGotSavedToDB)
            {
                orGentaDBDataSet.Clear();
                ItemsForm_Load(this, null);
            }

            // clear input field and note flag

            tbNewItem.Text = "";
            newItemHasNote = false;
            NewNoteText = "";
            label1.Visible = true;
        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            myNoteForm = new NoteForm(this);
            myNoteForm.MdiParent = myParentForm.myParentForm;
            myNoteForm.Top = this.Top + 60;
            myNoteForm.Left = this.Left - 30;
            myNoteForm.NoteIsOnNewItem = true;
            string itemText = tbNewItem.Text;
            string itemSamp = itemText + "...";
            if (itemText.Length > 20) { itemSamp = itemText.Substring(0, 20) + "..."; }
            myNoteForm.Text = "Note For: \"" + itemSamp + "\"";
            myNoteForm.Show();
        }

        private void ItemGrid_MouseDown(object sender, MouseEventArgs e)
        {
            igMouseDownLoc = e.Location;
        }

        private void ItemGrid_MouseUp(object sender, MouseEventArgs e)
        {
            // prevent erroneous click operations on a select-drag
            igMouseUpLoc = e.Location;
            int distClickX = (igMouseDownLoc.X - igMouseUpLoc.X) ^ 2;
            int distClickY = (igMouseDownLoc.Y - igMouseUpLoc.Y) ^ 2;
            if (distClickX + distClickY > 50) { return; }

            DeleteWasCanceled = false;
            AlreadyDeleted = false;
            clickedColumn = this.ItemGrid.HitTest(e.X, e.Y).ColumnIndex;
            clickedRow = this.ItemGrid.HitTest(e.X, e.Y).RowIndex;
            if (clickedRow == -1) { return; }
            if (clickedColumn == -1)
                { ItemGrid.Rows[clickedRow].Selected = true; }

            string ActiveItem = ItemGrid.Rows[clickedRow].Cells[3].Value.ToString();

            // They clicked the Note checkbox
            if ((clickedColumn == 0) && (clickedRow != -1))
            {
                BuildAndShowNote(ActiveItem);
                // if box was previously checked then keep it so
                ItemGrid.Rows[clickedRow].Cells[0].Value = 1;
            }

            sUtil = myParentForm.myParentForm.mySideUtils.lbCatList;
            sUtil.Visible = true;
            myParentForm.myParentForm.mySideUtils.Text = "Item Categories";

            // build list of Categories assigned to this item for sidebar

            sUtil.Items.Clear();
            string GetItemCmd = "SELECT Category FROM vw_GetCatsForItem " + RLockOption + "WHERE ItemID = " + ActiveItem;
            IDbCommand cmd = myDBconx.CreateCommand();
            cmd.CommandText = GetItemCmd;
            IDataReader ItemCats = cmd.ExecuteReader();
            while (ItemCats.Read())
            {
                String ThisCat = ItemCats.GetValue(0).ToString();
                sUtil.Items.Add(ThisCat);
            }

        }

        private void ItemGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) { return; }

            DataGridView myDG = (DataGridView)sender;
            if (myDG.CurrentCell == null) { return; }

            string newItemData = myDG.CurrentCell.FormattedValue.ToString();
            newItemData = myItemCleaner.CleanTheItem(newItemData);

            clickedRow = e.RowIndex;
            if (clickedRow == -1) { return; }

            ActiveItem = ItemGrid.Rows[clickedRow].Cells[3].Value.ToString();

            string updSQLcmd = "UPDATE Items SET ItemDesc = '" + newItemData;
            updSQLcmd += "' WHERE (ItemID = " + ActiveItem + ")";

            IDbCommand cmd = myDBconx.CreateCommand();
            cmd.CommandText = updSQLcmd;
            int rowsUpd = cmd.ExecuteNonQuery();

            itemIsBeingEdited = true;
            SoftAssign(newItemData);
            itemIsBeingEdited = false;
        }

        private void splGridSplitter_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            pb2arrow.Visible = false;
        }

        private void splGridSplitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            pb2arrow.Top = splGridSplitter.SplitterDistance - 16;
            pb2arrow.Visible = true;
        }

        private void ItemsForm_ResizeEnd(object sender, EventArgs e)
        {
            int formWid = this.Width;
            pb2arrow.Left = formWid - 66;
            if (localCacheToDisplay) { pb2arrow.Visible = true; }
            this.itemDescDataGridViewTextBoxColumn.Width = this.Width - 200;
        }

        private void ItemsForm_ResizeBegin(object sender, EventArgs e)
        {
            pb2arrow.Visible = false;
        }

        private void ItemGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            // the language auto executes this module for each row selected, but I only need to run it once
            if (DeleteWasCanceled)
            {
                e.Cancel = true;
                return;
            }
            if (AlreadyDeleted) { return; }

            bool cancelDelete = GetDeleteOptions();
            if (cancelDelete)
            {
                e.Cancel = true;
                return;
            }

            DataGridView myDG = (DataGridView)sender;
            RowsDeleting = myDG.SelectedRows;
            try
            { foreach (DataGridViewRow DeleteRow in RowsDeleting) { DeleteOneRow(DeleteRow); } }
            catch { }
            AlreadyDeleted = true;
        }

        private void ItemGrid_SelectionChanged(object sender, EventArgs e)
        {
            myParentForm.myParentForm.menuAssignTo.Enabled = true;
            myParentForm.myParentForm.menuZoomItem.Enabled = true;
            myParentForm.myParentForm.menuExportCSV.Enabled = true;
            myParentForm.myParentForm.menuItems2email.Enabled = true;
        }

        private void ItemGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DataGridView myDG = (DataGridView)sender;
                DataGridViewCell myCell = myDG.CurrentCell;
                if (myCell == null) { return; }
                if (myCell.Value != PriorCellValue)
                {
                    PriorCellValue = myCell.Value;
                    SendKeys.Send("{RIGHT}");
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{DELETE}");
        }

        private void assignToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myParentForm.myParentForm.menuAssignTo_Click(this, null);
        }

        private void btnChildItems_Click(object sender, EventArgs e)
        {
            bool ShowChildItems = false;
            switch (btnChildItems.Text)
            {
                case "o o":     // parent Category has no children
                    return;
                    break;
                case "+ +":     // user wants to see child items too
                    btnChildItems.Text = "- -";
                    ttChildItems.SetToolTip(btnChildItems, "Hide Child Items");
                    ShowChildItems = true;
                    break;
                case "- -":     // user wants to hide child items
                    btnChildItems.Text = "+ +";
                    ttChildItems.SetToolTip(btnChildItems, "Include Child Items");
                    break;
            }

            if (ShowChildItems)
            {
                TreeNode hotNode;
                try { hotNode = myParentForm.tvCategories.SelectedNode; }
                catch { hotNode = myParentForm.tvCategories.Nodes[0]; }
                foreach (TreeNode childNode in hotNode.Nodes)
                {
                    TreeViewForm.TagStruct pTag = new TreeViewForm.TagStruct();
                    pTag = (TreeViewForm.TagStruct)childNode.Tag;
                    string childCategoryID = pTag.CatID;
                    string myLoadSQL = "SELECT hasNote, ItemDesc, DateCreated, ItemID, CategoryID FROM vw_Get_Items " + RLockOption;
                    myLoadSQL += " WHERE CategoryID = " + childCategoryID;
                    LoadUptheGrids(myLoadSQL, "");
                }
            }
            else
            {
                orGentaDBDataSet.Clear();
                ItemsForm_Load(this, null);
            }

            myParentForm.BuildItemCatXref(this);
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myParentForm.myParentForm.menuZoomItem_Click(this, null);
        }

    }
}
