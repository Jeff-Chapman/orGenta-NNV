using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace orGenta_NNv
{
    public partial class ItemsForm : Form
    {
        #region Private Variables
        private bool newItemHasNote = false;
        private string newItemIDback;
        private NoteForm myNoteForm;
        private string EmptyNoteText = "Enter your note info here...";
        private int clickedColumn;
        private int clickedRow;
        private string ActiveItem;
        private bool itemGotSavedToDB;
        private bool itemGotSavedToCache;
        private char[] PunctuationArray = new char[] { ' ', ',', '.', ':', '?', '!', '\'', '(', ')' };
        private SharedRoutines myErrHandler = new SharedRoutines();
        private ArrayList PossibleAssigns = new ArrayList();
        private struct SAitems
        {
            public string matchCat;
            public string matchItem;
        }
        private bool SoftAssignPending;
        private bool itemBeingAssigned;       
        private bool itemIsBeingEdited = false;        
        private string DeleteOptionMode;
        private bool itemGotAssigned;
        private string ItemToFind;
        private DataGridViewSelectedRowCollection RowsDeleting;
        private object PriorCellValue;
        private bool searchMayBeEmpty;
        private bool localCacheToDisplay = false;
        private ListBox sUtil;
        private bool calledFromWrapper = false;
        private string newItemDate = "";
        private bool searchFirstLoad = false;
        private bool formJustLoaded;
        private List<List<string>> inspectSuppr;
        private string WorkingText;
        private TreeView catNodes;
        private string[] AssignWords;
        private struct CacheRecord
        {
            public string crCategory;
            public string crParentID;
            public string crItemDesc;
            public string crDateCreated;
            public string crNoteValue;
            public string crKBname;
        }
        CacheRecord myCacheRecord = new CacheRecord();
        #endregion

        public ArrayList ItemsCatXrefArray = new ArrayList();
        public ArrayList SearchCats = new ArrayList();
        public TreeViewForm myParentForm;
        public string NewNoteText = "";
        public IDbConnection myDBconx;
        public string categoryID;
        public TreeNode myNodeForTheseItems;
        public bool FormIsShadow = false;
        public string DataProvider;
        public string RLockOption;
        public SharedRoutines myItemCleaner;

        public ItemsForm(TreeViewForm parent)
        {
            InitializeComponent();
            myParentForm = parent;
            ItemToFind = "";
            TreeNode hotNode;
            try {hotNode = myParentForm.tvCategories.SelectedNode;}
            catch {hotNode = myParentForm.tvCategories.Nodes[0];}
            btnChildItems.BackColor = SystemColors.ControlLightLight;
            btnChildItems.ForeColor = Color.Black;
            if (hotNode.Nodes.Count == 0)
            {
                btnChildItems.BackColor = SystemColors.Control;
                btnChildItems.ForeColor = SystemColors.ControlText;
                btnChildItems.Text = "o o";
                ttChildItems.SetToolTip(btnChildItems, "(No child items)");
            }
        }

        public ItemsForm(TreeViewForm parent, string SearchItem)
        {
            InitializeComponent();
            myParentForm = parent;
            ItemToFind = SearchItem;
            btnChildItems.Visible = false;
            searchFirstLoad = true;
        }

        public void ItemsForm_Load(object sender, EventArgs e)
        {
            formJustLoaded = true;
            dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.NotSet;
            if (myParentForm.myParentForm.optWrapMode)
            {
                dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            }
            itemDescDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;

            localCacheTableBindingSource.Filter = "KBname = '" + myParentForm.Text + "'";
            myCacheRecord.crKBname = myParentForm.Text;

            string myLoadSQL;
            string myNoteLoadSQL = "";

            if (ItemToFind == "")
            {
                myLoadSQL = GetRegularItemsSQL();
            }
            else
            {
                myLoadSQL = "SELECT hasNote, ItemDesc, DateCreated, ItemID FROM vw_Get_Items_Distinct " + RLockOption;
                myLoadSQL += " WHERE [ItemDesc] LIKE '%" + ItemToFind + "%' ORDER BY DateCreated DESC";
                myCacheRecord.crCategory = "Unassigned";
                myCacheRecord.crParentID = "1";
                localCacheTableBindingSource.Filter += " AND ItemDesc LIKE '%" + ItemToFind + "%'";

                myNoteLoadSQL = "SELECT hasNote, ItemDesc, DateCreated, Vid.ItemID FROM (vw_Get_Items_Distinct Vid " + RLockOption;
                myNoteLoadSQL += " INNER JOIN Notes " + RLockOption + " ON Vid.ItemID = Notes.ItemID) WHERE";
                myNoteLoadSQL += " [NoteValue] LIKE '%" + ItemToFind + "%' ORDER BY DateCreated DESC";
            }

            myItemCleaner = new SharedRoutines();

            LoadUptheGrids(myLoadSQL, myNoteLoadSQL);
            if (ItemToFind != "") { FillInFirstCats(); }

            if (searchMayBeEmpty && (ItemToFind != ""))
            {
                MessageBox.Show("Search term not found");
                this.Visible = false;
                this.BeginInvoke(new MethodInvoker(this.Close));
                return;
            }

            if (searchFirstLoad)
            {
                String thisItem = Text.Substring(Text.IndexOf(":") + 3);
                sUtil = myParentForm.myParentForm.mySideUtils.lbOpenWindows;
                sUtil.Items.Add(thisItem);
                searchFirstLoad = false;
            }

         }

        private string GetRegularItemsSQL()
        {
            string myLoadSQL = "SELECT hasNote, ItemDesc, DateCreated, ItemID, CategoryID FROM vw_Get_Items " + RLockOption;
            myLoadSQL += " WHERE CategoryID = " + categoryID + " ORDER BY DateCreated DESC";
            TreeNode myCatNode = myParentForm.tvCategories.SelectedNode;
            myCacheRecord.crCategory = myCatNode.Text;
            localCacheTableBindingSource.Filter += " AND Category = '" + myCatNode.Text + "'";
            try
            {
                TreeViewForm.TagStruct pTag;
                pTag = (TreeViewForm.TagStruct)myCatNode.Parent.Tag;
                myCacheRecord.crParentID = pTag.CatID;
            }
            catch { myCacheRecord.crParentID = "1"; }
            myParentForm.myParentForm.menuAutoAssign.Enabled = true;
            myParentForm.myParentForm.menuImportItems.Enabled = true;
            return myLoadSQL;
        }

        private void FillInFirstCats()
        {
            foreach (DataGridViewRow myDGrow in ItemGrid.Rows)
            {
                string thisItem = myDGrow.Cells[3].Value.ToString();
                IDbCommand cmd = myDBconx.CreateCommand();
                string getCatcmd = "SELECT TOP 1 CategoryID FROM Rels WHERE isDeleted = 0 AND ItemID = " + thisItem;
                cmd.CommandText = getCatcmd;
                string SearchCatFound = cmd.ExecuteScalar().ToString();
                SearchCats.Add(SearchCatFound);
            }
        }

        private void LoadUptheGrids(string myLoadSQL, string myNoteLoadSQL)
        {
            SharedRoutines DataGrabber = new SharedRoutines();

            if (myParentForm.isOldMSaccess)
            { orGentaDBDataSet.vw_Get_Items.Columns["hasNote"].DataType = typeof(Byte); }
            else if (!myParentForm.isSQLlite)
            { orGentaDBDataSet.vw_Get_Items.Columns["CategoryID"].DataType = typeof(Int16); }
            if (myParentForm.isSQLlite)
            { orGentaDBDataSet.vw_Get_Items.Columns["CategoryID"].DataType = typeof(String); }

            try
            {
                DataSet myDS = new DataSet();
                myDS = DataGrabber.GetDataFor(DataProvider, myDBconx, myLoadSQL);
                myDS.Tables[0].PrimaryKey = new DataColumn[] { myDS.Tables[0].Columns[3] };

                DataSet myNotesDS = new DataSet();
                if (ItemToFind != "")
                {
                    myNotesDS = DataGrabber.GetDataFor(DataProvider, myDBconx, myNoteLoadSQL);
                    myNotesDS.Tables[0].PrimaryKey = new DataColumn[] { myNotesDS.Tables[0].Columns[3] };
                    myDS.Merge(myNotesDS);
                }
                searchMayBeEmpty = false;
                if (myDS.Tables[0].Rows.Count == 0)
                    { searchMayBeEmpty = true; }
                orGentaDBDataSet.Tables[0].Merge(myDS.Tables[0]);
            }
            catch
            {
                myParentForm.pbDBisDown.Visible = true;
            }

            localCacheTableTableAdapter.Fill(this.localCacheDataSet.localCacheTable);            
            localCacheToDisplay = false;
            if (CachedGrid.RowCount > 0) 
            { 
                localCacheToDisplay = true;
                searchMayBeEmpty = false;
            }

            if (!localCacheToDisplay)
            {
                splGridSplitter.Panel2Collapsed = true;
                pb2arrow.Visible = false;
            }
            else 
                { pb2arrow.Visible = true;}

        }

        private void ItemSaveRoutine()
        {
            if (NewNoteText != "") { newItemHasNote = true; }

            if ((myParentForm.myParentForm.optCreateCategories) && (myParentForm.myParentForm.AutoCreateCats.Count > 0))
            {
                TreeNode newAddedNode;
                TreeNode myParentNode = myParentForm.myParentForm.FindNodeInTV("Main\\Untitled", null, false, "");
                List<string> newCatsToMake = myParentForm.myParentForm.AutoCreateCats;
                TreeViewForm myTVform = myParentForm.myParentForm.ActiveTopForm;
                foreach (string oneNewCat in newCatsToMake)
                    { newAddedNode = myTVform.SetupNewNode(myParentNode, oneNewCat, false); }

                newCatsToMake.Clear();
                // Force category persistence            
                myParentForm.myParentForm.ActiveTopForm.tmrTVdirty_Tick(this, null);
            }

            try { saveNewItemInDB(categoryID); }
            catch (Exception ex)
            {
                if (myParentForm.testing) { myErrHandler.LogRTerror("ItemSaveRoutine:saveNewItemInDB", ex); }
                persistItemandNotesToCache();
                return;
            }
            itemGotSavedToDB = true;

            if (newItemHasNote)
            {
                try { saveNewNoteInDB(); }
                catch (Exception ex)
                {
                    if (myParentForm.testing) { myErrHandler.LogRTerror("ItemSaveRoutine:saveNewNoteInDB", ex); }
                    persistNoteToCache(); }
            }
        }

        internal string saveNewItemWrapper(string newItemIn, object newDateIn)
        {
            throw new NotImplementedException();
        }

        private void persistNoteToCache()
        {
            if (myParentForm.testing) 
            { 
                string msgAboutNoteCache = "Unable to save Note to DB, cached note text follows\n";
                msgAboutNoteCache += NewNoteText;
                LogRTnotes("persistNoteToCache", msgAboutNoteCache); 
            }

            string noteCacheFileN = myParentForm.activeDBname + "_Notes_Cache.txt";
           
            using (StreamWriter sw = File.AppendText(noteCacheFileN))
            {
                string td = DateTime.Now.ToShortDateString();
                sw.WriteLine(td + " " + DateTime.Now.ToLongTimeString());

                string holdNote = myItemCleaner.CleanTheItem(NewNoteText);
                string insNoteCmd = "INSERT INTO [Notes] ([ItemID],[NoteValue]) VALUES (";
                insNoteCmd += newItemIDback + ",'" + holdNote + "')";

                sw.WriteLine(insNoteCmd);
                sw.WriteLine(new String('-', 60));
            }
        }

        public void saveNewNoteInDBWrapper(string itemForNote, string NoteText)
        {
            NewNoteText = NoteText;
            newItemIDback = itemForNote;
            saveNewNoteInDB();
            string updSQLcmd = "UPDATE Items SET hasNote = 1";
            updSQLcmd += " WHERE (ItemID = " + itemForNote + ")";
            IDbCommand cmd = myDBconx.CreateCommand();
            cmd.CommandText = updSQLcmd;
            int rowsIns = cmd.ExecuteNonQuery();
        }

        private void saveNewNoteInDB()
        {
            string holdNote = myItemCleaner.CleanTheItem(NewNoteText);
            string insNoteCmd = "INSERT INTO [Notes] ([ItemID],[NoteValue]) VALUES (";
            insNoteCmd += newItemIDback + ",'" + holdNote + "')";

            IDbCommand cmd = myDBconx.CreateCommand();
            cmd.CommandText = insNoteCmd;
            int rowsIns = cmd.ExecuteNonQuery();
        }

        private void persistItemandNotesToCache()
        {
            if (myParentForm.testing)
            {
                string msgAboutNoteCache = "Unable to save Item to DB, cached item follows\n";
                msgAboutNoteCache += tbNewItem.Text;
                LogRTnotes("persistItemandNotesToCache", msgAboutNoteCache);
            }

            string itemCacheFileN = myParentForm.activeDBname + "_Notes_Cache.txt";
            string td = DateTime.Now.ToShortDateString();
            string holdItem = myItemCleaner.CleanTheItem(tbNewItem.Text);

            using (StreamWriter sw = File.AppendText(itemCacheFileN))
            {
                sw.WriteLine(td + " " + DateTime.Now.ToLongTimeString());
                string insItemCmd = "INSERT INTO [Items] ([ItemDesc],[hasNote],[isDeleted]) VALUES (";
                insItemCmd += "'" + holdItem + "',";
                if (newItemHasNote)
                    { insItemCmd += "1,0)"; }
                else
                    { insItemCmd += "0,0)"; }

                sw.WriteLine(insItemCmd);
                sw.WriteLine(new String('-', 60));
                itemGotSavedToCache = true;
            }
            
            myCacheRecord.crDateCreated = td;
            myCacheRecord.crItemDesc = holdItem;
            myCacheRecord.crNoteValue = "";
            if (newItemHasNote) { myCacheRecord.crNoteValue = NewNoteText; }
            
            // save record to local cache DB

            this.Cursor = Cursors.WaitCursor;
            this.Refresh();
            string insCacheCmd = "INSERT INTO localCacheTable (Category, ParentID, ItemDesc, DateCreated, NoteValue, KBname) ";
            insCacheCmd += "VALUES ('" + myCacheRecord.crCategory + "', " + myCacheRecord.crParentID + ", '";
            insCacheCmd += myCacheRecord.crItemDesc + "', '" + myCacheRecord.crDateCreated + "', '" + myCacheRecord.crNoteValue;
            insCacheCmd += "', '" + myCacheRecord.crKBname + "')";
            IDbCommand cmd = myParentForm.myParentForm.localCacheDBconx.CreateCommand();
            cmd.CommandText = insCacheCmd;
            int rowsIns = cmd.ExecuteNonQuery();
            myParentForm.pbDBisDown.Visible = true;
            if (splGridSplitter.Panel2Collapsed)
            {
                splGridSplitter.Panel2Collapsed = false;
                pb2arrow.Visible = true;
                splGridSplitter.SplitterDistance -= 160;
            }

            localCacheDataSet.Clear();
            ItemsForm_Load(this, null);
            this.Cursor = Cursors.Arrow;

            if (newItemHasNote) { persistNoteToCache();}
        }

        public string saveNewItemWrapper(string newItemIn)
        {
            saveNewItemWrapper(newItemIn, "");
            return newItemIDback;
        }

        public string saveNewItemWrapper(string newItemIn, string newDateIn)
        {
            saveNewItemWrapper(newItemIn, newDateIn, categoryID);
            newItemDate = "";
            return newItemIDback;
        }

        public string saveNewItemWrapper(string newItemIn, string newDateIn, string newCategoryIn)
        {
            tbNewItem.Text = newItemIn;
            newItemHasNote = false;
            newItemDate = newDateIn;
            calledFromWrapper = true;
            saveNewItemInDB(newCategoryIn);
            calledFromWrapper = false;
            return newItemIDback;
        }

        private void saveNewItemInDB(string newCategoryID)
        {
            if (tbNewItem.Text == "") { return; }
            IDbCommand cmd;
            int rowsIns;
            ItemAloneSave(out cmd, out rowsIns);
            if (newCategoryID == null) { newCategoryID = "2"; }       // use Unassigned if blank

            string insRelCmd = "INSERT INTO [Rels] ([CategoryID],[ItemID],[isDeleted]) VALUES (";
            insRelCmd += newCategoryID + "," + newItemIDback + ",0)";
            cmd.CommandText = insRelCmd;
            rowsIns = cmd.ExecuteNonQuery();

            if (!calledFromWrapper) { SoftAssign(tbNewItem.Text); }
        }

        private void ItemAloneSave(out IDbCommand cmd, out int rowsIns)
        {
            string holdItem = myItemCleaner.CleanTheItem(tbNewItem.Text);

            string insItemCmd = "";
            if (newItemDate == "")
                { insItemCmd = "INSERT INTO [Items] ([ItemDesc],[hasNote],[isDeleted]) VALUES ("; }
            else
                { insItemCmd = "INSERT INTO [Items] ([ItemDesc],[hasNote],[isDeleted],[DateCreated]) VALUES ("; }
            insItemCmd += "'" + holdItem + "',";
            if (newItemHasNote)
                { insItemCmd += "1,0"; }
            else
                { insItemCmd += "0,0"; }
            if (newItemDate == "")
                { insItemCmd += ")"; }
            else
                { insItemCmd += ",'" + newItemDate + "')"; }

            cmd = myDBconx.CreateCommand();
            cmd.CommandText = insItemCmd;
            rowsIns = cmd.ExecuteNonQuery();

            if (myDBconx.Database != "")
                { cmd.CommandText = "SELECT @@IDENTITY AS NEWROWID"; }
            else
                { cmd.CommandText = "SELECT MAX(ItemID) AS NEWROWID FROM Items"; }

            newItemIDback = cmd.ExecuteScalar().ToString();
        }

        private void LogRTnotes(string RoutineName, string NotesToSave)
        {
            using (StreamWriter sw = File.AppendText("ErrorLog.txt"))
            {
                string td = DateTime.Now.ToShortDateString();
                sw.WriteLine(td + " " + DateTime.Now.ToLongTimeString());
                sw.WriteLine(RoutineName);
                sw.WriteLine(NotesToSave);
                sw.WriteLine(new String('-', 60));
            }
        }

        private void BuildAndShowNote(string ActiveItem)
        {
            string NoteTextToShow = "";
            
            string GetItemCmd = "SELECT NoteValue FROM Notes " + RLockOption + "WHERE ItemID = " + ActiveItem;

            IDbCommand cmd = myDBconx.CreateCommand();
            cmd.CommandText = GetItemCmd;
       
            try
            {
                NoteTextToShow = cmd.ExecuteScalar().ToString();
                if (NoteTextToShow == null)
                { NoteTextToShow = EmptyNoteText; }
            }
            catch
                { NoteTextToShow = EmptyNoteText; }

            myNoteForm = new NoteForm(this);
            myNoteForm.MdiParent = myParentForm.myParentForm;
            myNoteForm.Top = this.Top + 60;
            myNoteForm.Left = this.Left - 30;
            // if OK button is hidden we need to shift form upwards
            if (myNoteForm.Bottom > myParentForm.myParentForm.Height - 90)
                { myNoteForm.Height = myParentForm.myParentForm.Height - myNoteForm.Top - 100; }
            string itemText = ItemGrid.Rows[clickedRow].Cells[1].Value.ToString();
            string itemSamp = itemText + "...";
            if (itemText.Length > 20) { itemSamp = itemText.Substring(0, 20) + "..."; }
            myNoteForm.Text = "Note For: \"" + itemSamp + "\"";
            myNoteForm.NoteIsOnNewItem = false;
            myNoteForm.parentItemID = ActiveItem;
            myNoteForm.tbNoteText.Text = NoteTextToShow;
            myNoteForm.originalNoteText = NoteTextToShow;
            myNoteForm.parentClickedRow = clickedRow;

            if (NoteTextToShow != EmptyNoteText)
            { 
                myNoteForm.tbNoteText.SelectionStart = NoteTextToShow.Length;
                myNoteForm.noteWasBlank = false;
            }
            else
            {
                myNoteForm.tbNoteText.SelectionStart = 0;
                myNoteForm.tbNoteText.SelectionLength = NoteTextToShow.Length;
            }            
            
            myNoteForm.Show();

        }

        private void tmrSoftAssign_Tick(object sender, EventArgs e)
        {
            if (itemBeingAssigned) { return; }
            itemBeingAssigned = true;
            // WARNING: any exceptions or early exits from this routine need to run SoftAssignCleanup

            SAitems chkAssign = (SAitems)PossibleAssigns[0];
            string matchCategoryID = chkAssign.matchCat;
            string matchItemID = chkAssign.matchItem;
            IDbCommand cmd = myDBconx.CreateCommand();

            // check for already assigned
            string chkRelCmd = "SELECT COUNT(*) FROM Rels " + RLockOption + "WHERE CategoryID = ";
            chkRelCmd += matchCategoryID + " AND ItemID = " + matchItemID + " AND IsDeleted = 0";
            cmd.CommandText = chkRelCmd;
            int countBack = (int)cmd.ExecuteScalar();
            if (countBack > 0)
            {
                SoftAssignCleanup(matchItemID);
                return;
            }

            string insRelCmd = "INSERT INTO [Rels] ([CategoryID],[ItemID],[isDeleted]) VALUES (";
            insRelCmd += matchCategoryID + "," + matchItemID + ",0)";
            cmd.CommandText = insRelCmd;
            int rowsIns = cmd.ExecuteNonQuery();
            itemGotAssigned = true;

            SoftAssignCleanup(matchItemID);
        }

        private void SoftAssignCleanup(string matchItemID)
        {
            PossibleAssigns.RemoveAt(0);
            if (PossibleAssigns.Count == 0)
            {
                tmrSoftAssign.Enabled = false;
                SoftAssignPending = false;
                if (itemGotAssigned)
                {
                    // Remove item from "Unassigned" category if it's there. 
                    string matchCategoryID = "2";
                    IDbCommand cmd = myDBconx.CreateCommand();
                    string delRelCmd = "UPDATE [Rels] SET isDeleted = 1 WHERE [CategoryID] = ";
                    delRelCmd += matchCategoryID + " AND [ItemID] = " + matchItemID;
                    cmd.CommandText = delRelCmd;
                    int countBack = (int)cmd.ExecuteNonQuery();
                }
            }
            itemBeingAssigned = false;
        }

        private void SoftAssign(string TextToSoftAssign)
        {
            while (SoftAssignPending)
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                System.Threading.Thread.Sleep(250);
            }

            this.Cursor = Cursors.Arrow;
            string lowerWorkingText = TextToSoftAssign.ToLower();
            AssignWords = lowerWorkingText.Split(PunctuationArray);            
            string MatchingItemID = newItemIDback;

            if (itemIsBeingEdited) { MatchingItemID = ActiveItem; }

            // Sort the text string to avoid matching dupes

            ArrayList SortedWords = new ArrayList();
            SortedWords.AddRange(AssignWords);
            SortedWords.Sort();
            int numAssignWords = AssignWords.GetUpperBound(0);
            WorkingText = "";

            for (int i = 0; i <= numAssignWords; i++)
            {
                if ((i > 0) && ((SortedWords[i]) == SortedWords[i - 1])) { continue; }
                if (SortedWords[i].ToString() != "") { WorkingText += SortedWords[i].ToString() + " "; }
            }
            catNodes = myParentForm.tvCategories;
            SoftAssignDrill(catNodes.Nodes, MatchingItemID, TextToSoftAssign);

            if (PossibleAssigns.Count > 0)
            {
                SoftAssignPending = true;
                tmrSoftAssign.Enabled = true;
                itemGotAssigned = false;
            }
        }

        private void SoftAssignDrill(TreeNodeCollection NodesToSearch, string MatchingItemID, string TextToSoftAssign)
        {
            int numAssignWords = AssignWords.GetUpperBound(0);
            foreach (TreeNode CategoryNode in NodesToSearch)
                { MatchWordToTreenode(MatchingItemID, numAssignWords, CategoryNode, TextToSoftAssign); }
        }

        private void MatchWordToTreenode(string MatchingItemID, int numAssignWords, TreeNode CategoryNode, string TextToSoftAssign)
        {
            for (int i = 0; i <= numAssignWords; i++)
            {
                if (CategoryNode.Text.ToLower() == (AssignWords[i]))
                {
                    TreeViewForm.TagStruct CatTag = (TreeViewForm.TagStruct)CategoryNode.Tag;
                    if (CatTag.ManualAssign == "1") { continue; }
                    if (CatTag.CatID == categoryID) { continue; }
                    bool matchedSupress = false;
                    if (myParentForm.myParentForm.TempCatSuppress.Count > 0) 
                        { matchedSupress = checkForTempSuppress(CategoryNode.Text.ToLower(), TextToSoftAssign); }
                    if (matchedSupress) { continue; }

                    SAitems thisMatch = new SAitems();
                    thisMatch.matchCat = CatTag.CatID;
                    thisMatch.matchItem = MatchingItemID;
                    PossibleAssigns.Add(thisMatch);
                }
            }
            SoftAssignDrill(CategoryNode.Nodes, MatchingItemID, TextToSoftAssign);
        }

        private bool checkForTempSuppress(string catText, string passedItemText)
        {
            int elementNum = 0;
            inspectSuppr = myParentForm.myParentForm.TempCatSuppress;
            foreach( List<string> oneSuppr in inspectSuppr )
            {
                if ((oneSuppr[0].ToLower() == catText.ToLower()) && (oneSuppr[1] == passedItemText))
                {
                    inspectSuppr.RemoveAt(elementNum);
                    return true; 
                }
                elementNum++;
            }
            return false;
        }

        private bool GetDeleteOptions()
        {
            ItemDelete GetDeleteInfo = new ItemDelete();
            if (categoryID == "3")
                { GetDeleteInfo.lblTrashWarning.Visible = true; }

            if (GetDeleteInfo.ShowDialog(this) == DialogResult.OK)
            {
                if (GetDeleteInfo.btnDeleteFromCat.Checked)
                {
                    Console.WriteLine("Delete from Category selected");
                    DeleteOptionMode = "Delete";
                }
                else
                {
                    Console.WriteLine("Discard Completely selected");
                    DeleteOptionMode = "Discard";
                }
                if (categoryID == "3")
                    { DeleteOptionMode = "Trash"; }
                return false;
            }
            else
            {
                DeleteWasCanceled = true;
                return true;
            }
        }

        private void DeleteOneRow(DataGridViewRow DeleteRow)
        {
            DataRowView myDR = (DataRowView)DeleteRow.DataBoundItem;
            object[] DelColVals = myDR.Row.ItemArray;

            // get both item ID and category ID from backing dataset (search grid may be mixed) 
            string DelItemID = DelColVals[3].ToString();
            string DelCatID = DelColVals[4].ToString();

            string updSQLcmd;
            if (DeleteOptionMode == "Delete")
            {
                updSQLcmd = "UPDATE Rels SET isDeleted = 1";
                updSQLcmd += " WHERE (ItemID = " + DelItemID + " AND CategoryID = " + DelCatID + ")";
            }
            else if (DeleteOptionMode == "Discard")
            {
                updSQLcmd = "UPDATE Rels SET isDeleted = 1";
                updSQLcmd += " WHERE (ItemID = " + DelItemID + ")";
            }
            else
            {
                // Removing from Trash
                updSQLcmd = "UPDATE Items SET isDeleted = 1";
                updSQLcmd += " WHERE (ItemID = " + DelItemID + ")";
            }

            IDbCommand cmd = myDBconx.CreateCommand();
            cmd.CommandText = updSQLcmd;
            int rowsUpd = cmd.ExecuteNonQuery();

            // In Delete mode, if an item is no longer attached to any Category
            //      then it equivalents to a Discard

            if (DeleteOptionMode == "Delete")
            {
                string chkDelete = "SELECT COUNT(*) FROM Rels WHERE (ItemID = " + DelItemID + " AND isDeleted = 0)";
                cmd.CommandText = chkDelete;
                int attachCount = (int)cmd.ExecuteScalar();
                if (attachCount == 0) { DeleteOptionMode = "Discard"; }
            }

            if (DeleteOptionMode == "Discard")
            {
                // Insert rel for this item to the TrashCan category
                string insRelCmd = "INSERT INTO [Rels] ([CategoryID],[ItemID],[isDeleted]) VALUES (";
                insRelCmd += "3," + DelItemID + ",0)";
                cmd.CommandText = insRelCmd;
                int rowsIns = cmd.ExecuteNonQuery();
            }
        }

        private void ItemsForm_Activated(object sender, EventArgs e)
        {
            myParentForm.myParentForm.ActiveTopItems = this;
            myParentForm.myParentForm.ActiveTopForm = myParentForm;
            myParentForm.myParentForm.menuExportCSV.Enabled = true;
            myParentForm.myParentForm.menuItems2email.Enabled = true;
            myParentForm.BringToFront();
            if ((formJustLoaded) && (myParentForm.myParentForm.optAdjustItemsToParent == true))
            {
                int MDIright = myParentForm.myParentForm.Right;
                this.Width = MDIright - this.Left - myParentForm.myParentForm.Left - 20;
                if (this.Bottom > myParentForm.myParentForm.Bottom - 150)
                    { this.Width = this.Width - 20; }
                ItemGrid.Columns[1].Width = this.Width - 200;
                formJustLoaded = false;
            }

            pb2arrow.Top = splGridSplitter.SplitterDistance - 13;
            pb2arrow.Left = this.Width - 66;
            this.tbNewItem.Focus();
        }

        public void AutoAssign_Items()
        {
            foreach (DataGridViewRow oneRow in ItemGrid.Rows)
            {
                newItemIDback = oneRow.Cells[3].Value.ToString();
                SoftAssign(oneRow.Cells[1].Value.ToString());
            }

        }

        private void ItemsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Alert user if they closed with unsaved new item typed in
            if (tbNewItem.Text != "")
            {
                DialogResult btnPressed = MessageBox.Show("Unsaved Item! Do you want to save it?",
                    "Unsaved Item", MessageBoxButtons.YesNoCancel);
                if (btnPressed.ToString() == "Cancel")
                {
                    e.Cancel = true;
                    return;
                }
                if (btnPressed.ToString() == "Yes")
                    { btnOK_Click(this, null); }
            }

            myParentForm.myParentForm.ActiveTopItems = null;
            sUtil = myParentForm.myParentForm.mySideUtils.lbOpenWindows;
            String sUWindowName = Text.Substring(Text.IndexOf(":") + 3);
            sUtil.Items.Remove(sUWindowName);
            this.orGentaDBDataSet.Clear();
            this.orGentaDBDataSet.Dispose();
            this.localCacheDataSet.Clear();
            this.localCacheDataSet.Dispose();
            myParentForm.myParentForm.menuAssignTo.Enabled = false;
            myParentForm.myParentForm.menuZoomItem.Enabled = false;
            myParentForm.myParentForm.menuExportCSV.Enabled = false;
            myParentForm.myParentForm.menuAutoAssign.Enabled = false;
            myParentForm.myParentForm.menuImportItems.Enabled = false;
            myParentForm.myParentForm.menuItems2email.Enabled = false;

        }

        private void ItemGrid_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            myParentForm.myParentForm.ActiveTopItems = this;
        }
    }
}
