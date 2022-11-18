using System;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace orGenta_NNv
{
    public partial class ImportStuff : Form
    {
        #region Private Variables
        private bool MidStreamCancel = false;
        private int ItemCount = 0;
        private string ImportedItem;
        private string importMode;
        private frmMain myparent;
        private string categoryPath;
        private string hasNoteFlag;
        private string priorItemID;
        private System.Collections.ArrayList catFullPathList;
        private System.Collections.ArrayList oldItemIDs = new System.Collections.ArrayList();
        private System.Collections.ArrayList newItemIDs = new System.Collections.ArrayList();
        private bool importingNotes;
        private string FullNote = "";
        private string HalnaBuildItem;
        private string HalnaBuildNote;
        private int HalnaIndent;
        private int HalnaBlankline;
        private string RootParentPath;
        private string IncomingCategory;
        private string CurrentWorkingPath;
        private string newDateIn;
        private int previousCatIndent;
        private string newDBitemID;
        private string FileToImport;
        private int XMLImportReadCount;
        private int XMLlevel;

        #endregion

        public bool prevNoteLineBlank { get; private set; }

        public ImportStuff(frmMain ParentForm)
        {
            InitializeComponent();
            myparent = ParentForm;
            rbEverything.Enabled = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (pnlEveryOptions.Visible) 
            { 
                OKcontinueRoutine();
                return;
            }
            if (rbOPML.Checked) { this.ImportItemsDialog.FilterIndex = 2; }
            DialogResult openFresult = this.ImportItemsDialog.ShowDialog(this);
            if (openFresult == DialogResult.Cancel)
            { 
                this.Close();
                return;
            }

            importMode = "Everything";
            if (rbItems.Checked) { importMode = "Items"; }
            if (rbItemsDates.Checked) { importMode = "ItemsDates"; }
            if (rbHalna.Checked) { importMode = "Halna"; }
            if (rbOPML.Checked) { importMode = "Opml"; }

            if (importMode == "Everything" || importMode == "Halna" || importMode == "Opml")
            {
                string lblOpt = lblImportUnassigned.Text;
                lblImportUnassigned.Text = lblOpt.Replace("xxx", myparent.ActiveTopItems.Text);
                pnlEveryOptions.Visible = true;
                return;
            }
            else
                { this.btnOK.Visible = false; }

            OKcontinueRoutine();
        }

        private void OKcontinueRoutine()
        {
            FileToImport = this.ImportItemsDialog.FileName;
            this.ImportItemsDialog.Dispose();
            if (FileToImport == "") { this.Close(); }
            else { ImportItemsFromFile(FileToImport); }
        }

        private void ImportItemsFromFile(string FileToImport)
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            // Read the file first to see how big it is
            StreamReader ImportReader = new StreamReader(FileToImport);
            String ImportedItem;
            int SizeOfImportFile = 0;
            while ((ImportedItem = ImportReader.ReadLine()) != null)
                { SizeOfImportFile++; }
            ImportReader.Close();

            pbImportProgress.Visible = true;
            pbImportProgress.Maximum = SizeOfImportFile;
            this.btnCancel.Visible = true;
            int ImportReadCount;

            if (importMode == "Opml")
            {
                XmlDocument importedXML = new XmlDocument();
                importedXML.Load(FileToImport);
                ImportReadCount = LoadXMLImportToTable(importedXML);
            }
            else
            {
                ImportReader = new StreamReader(FileToImport);
                ImportReadCount = LoadImportToTable(ImportReader);
                ImportReader.Close();
                ImportReader.Dispose();
            }

            this.btnCancel.Visible = false;
            string dispCat = myparent.ActiveTopItems.Text + " category";
            if (rbImportCats.Checked) { dispCat = myparent.ActiveTopForm.Text + " database"; }
            string importedMessage = ImportReadCount.ToString() + " Items loaded to the " + dispCat;
            myparent.ActiveTopItems.tbNewItem.Text = "";
            MessageBox.Show(importedMessage);
            myparent.ActiveTopItems.orGentaDBDataSet.Clear();
            myparent.ActiveTopItems.ItemsForm_Load(this, null);
            this.Close();
        }

        private int LoadXMLImportToTable(XmlDocument importedXML)
        {
            XMLImportReadCount = 0;
            XMLlevel = 0;
            catFullPathList = myparent.ActiveTopForm.FullPathList;
            RootParentPath = "Main\\";

            foreach (XmlNode node in importedXML.DocumentElement.ChildNodes)
            {
                foreach (XmlNode childNode in node.ChildNodes) { XMLnodeDrill(childNode); }
            }
            return XMLImportReadCount;
        }

        private void XMLnodeDrill(XmlNode node)
        {
            string ItemText = ""; string NoteText = "";
            try { ItemText = node.Attributes["text"].InnerText; }
            catch { }
            try { NoteText = node.Attributes["_note"].InnerText; }
            catch { }
            if (ItemText != "") { processOneXMLentry(ItemText, NoteText, XMLlevel); }
            XMLlevel++;
            foreach (XmlNode childNode in node.ChildNodes) { XMLnodeDrill(childNode); }
            XMLlevel--;
        }

        private void processOneXMLentry(string itemText, string noteText, int XMLlevel)
        {
            Console.WriteLine(XMLlevel.ToString());
            Console.WriteLine(itemText); Console.WriteLine(noteText);
            bool hasTwoSpaces = CheckForTwoSpaces(itemText);
            if (hasTwoSpaces)
            { 
                saveItemToDB(itemText, noteText);
                XMLImportReadCount++;
            }
            else
            {
                IncomingCategory = itemText.Trim();
                CurrentWorkingPath = GetCategoryParent(XMLlevel) + IncomingCategory;
                categoryPath = CurrentWorkingPath;
                Console.WriteLine("Found Category " + IncomingCategory);
                Console.WriteLine("With Path of " + CurrentWorkingPath);
                if (noteText != "")
                {
                    string BuildItem = "Some additional notes for " + IncomingCategory;
                    saveItemToDB(BuildItem, noteText);
                    XMLImportReadCount++;
                }
            }
        }

        private int LoadImportToTable(StreamReader ImportReader)
        {
            importingNotes = false;
            catFullPathList = myparent.ActiveTopForm.FullPathList;
            int ImportReadCount = 0;
            
            oldItemIDs.Clear();
            newItemIDs.Clear();

            HalnaBuildItem = "";
            HalnaBuildNote = "";
            HalnaIndent = -1;
            HalnaBlankline = 1;
            RootParentPath = "Main\\";

            if (importMode == "Everything")     // bypass header
                { ImportedItem = ImportReader.ReadLine(); }

            while ((ImportedItem = ImportReader.ReadLine()) != null)
            {
                pbImportProgress.Value = ImportReadCount;
                //	check if we got an interrupt to cancel
                Application.DoEvents();
                if (MidStreamCancel) { break; }

                if (ImportedItem == "")
                {
                    HalnaBlankline++;
                    continue;
                }
                ImportReadCount++;
                if (importingNotes) { ProcessImportedNote(ImportedItem); }
                else
                {
                    ProcessImportedItem(ImportedItem);
                    HalnaBlankline = 0;

                    if ((!importingNotes) && (importMode != "Halna")) { ItemCount++; }
                }
            }

            if (importMode == "Halna")
            { 
                saveItemToDB(HalnaBuildItem, HalnaBuildNote);
                HalnaBuildItem = ""; HalnaBuildNote = "";
            }

            return ItemCount;
        }

        private void ProcessImportedNote(string ImportedItem)
        {
            if (ImportedItem == "") { return; }
            string lastChar = ImportedItem.Substring(ImportedItem.Length - 1);
            if (lastChar != "\"")    // This is a multiline note that continues
            {
                if (FullNote == "")
                    { FullNote = ImportedItem; }
                else
                    {FullNote += "\r\n" + ImportedItem;}
                return;
            }
            if (FullNote != "") 
            { 
                FullNote += "\r\n" + ImportedItem;
                ImportedItem = FullNote;
            }
            int firstComma = ImportedItem.IndexOf(",");
            string oldItemID = ImportedItem.Substring(0, firstComma);
            string noteValue = QuoteCleanup(ImportedItem.Substring(firstComma + 1));
            int ixIDloc = oldItemIDs.IndexOf(oldItemID);
            string newNoteID = newItemIDs[ixIDloc].ToString();
            myparent.ActiveTopItems.saveNewNoteInDBWrapper(newNoteID, noteValue);
            FullNote = "";
        }

        private void ProcessImportedItem(string ImportedItem)
        {
            if (ImportedItem == "") { return; }
            string parseMe = ImportedItem.Replace("\",\"", "|");
            string[] colsIn = parseMe.Split(new char[] { '|' });
            if ((importMode == "ItemsDates") && (colsIn.GetUpperBound(0) == 0))
            {
                parseMe = ImportedItem.Replace("\",", "|");
                colsIn = parseMe.Split(new char[] { '|' });
            }
            string newItemIn = "";
            string newDateIn;
            if ((importMode == "Items") || (importMode == "ItemsDates"))
            {
                newItemIn = QuoteCleanup(colsIn[0]);
            }

            if (newItemIn.Length > 250)
            {
                string shortItemIn = "This is a note for how " + newItemIn.Substring(0, 50) + "...";
                saveItemToDB(shortItemIn, newItemIn);
                return;
            }

            if (importMode == "Items")
            {
                myparent.ActiveTopItems.saveNewItemWrapper(newItemIn);
                return;
            }
            if (importMode == "ItemsDates")
            {
                newDateIn = QuoteCleanup(colsIn[1]);
                myparent.ActiveTopItems.saveNewItemWrapper(newItemIn, newDateIn);
                return;
            }
            if (importMode == "Halna")
            {
                DoHalnaImport(ref ImportedItem);
                return;
            }

            // welp it must be Everything mode...

            DoEverything(colsIn, ref newItemIn);
        }

        private void DoEverything(string[] colsIn, ref string newItemIn)
        {
            string AddedItemID;
            categoryPath = QuoteCleanup(colsIn[0]);
            hasNoteFlag = QuoteCleanup(colsIn[1]);
            try
            {
                newItemIn = QuoteCleanup(colsIn[2]);
                newDateIn = QuoteCleanup(colsIn[3]);
                priorItemID = colsIn[4].Substring(0, colsIn[4].Length - 2);
            }
            catch
            {
                importingNotes = true;
                return;
            }

            CreateNewDBitem(newItemIn, out AddedItemID);
            oldItemIDs.Add(priorItemID);
            newItemIDs.Add(AddedItemID);
        }

        private void CreateNewDBitem(string newItemIn, out string AddedItemID)
        {
            if (!rbImportCats.Checked)
            {
                AddedItemID = myparent.ActiveTopItems.saveNewItemWrapper(newItemIn, newDateIn);
                return;
            }

            // use existing path if exists, else create it
            string incomingCatID;
            if (catFullPathList.IndexOf(categoryPath) >= 0)     // ERR: this is a bug, should be case insensitive
            {
                TreeNode foundCat = myparent.FindNodeInTV(categoryPath, null, false, "");
                TreeViewForm.TagStruct thisTag = (TreeViewForm.TagStruct)foundCat.Tag;
                incomingCatID = thisTag.CatID;
            }
            else { incomingCatID = createNodesForPath(categoryPath); }

            AddedItemID = myparent.ActiveTopItems.saveNewItemWrapper(newItemIn, newDateIn, incomingCatID);
        }

        private void DoHalnaImport(ref string ImportedItem)
        {
            int thisIndent = CountTabs(ImportedItem);
            ImportedItem = ImportedItem.Replace("\t", "").Trim();
            if ((HalnaIndent != thisIndent) || (HalnaBlankline > 1))
            {
                saveItemToDB(HalnaBuildItem, HalnaBuildNote);
                HalnaBuildItem = ""; HalnaBuildNote = "";
                HalnaIndent = thisIndent;
            }

            bool hasTwoSpaces = CheckForTwoSpaces(ImportedItem);

            if ((hasTwoSpaces) || (HalnaBlankline == 0))
            {
                if ((ImportedItem.Length > 50) && (HalnaBuildItem == ""))
                {
                    // This is comments on a category level, needs ghost Item
                    HalnaBuildItem = "Some additional notes for " + IncomingCategory;
                    AddToHalnaNote(ImportedItem, thisIndent);
                    return;
                }

                if (HalnaBuildItem == "")
                { HalnaBuildItem = ImportedItem.Trim(); }
                else
                {
                    AddToHalnaNote(ImportedItem, thisIndent);
                }

            }
            else
            {
                if (ImportedItem != "")
                {
                    // Note: category persists when there is a following item
                    IncomingCategory = ImportedItem.Trim();
                    CurrentWorkingPath = GetCategoryParent(thisIndent) + IncomingCategory;
                    categoryPath = CurrentWorkingPath;

                    Console.WriteLine("Found Category " + IncomingCategory);
                    Console.WriteLine("With Path of " + CurrentWorkingPath);
                }

            }

            return;
        }

        private static bool CheckForTwoSpaces(string ImportedItem)
        {
            if(ImportedItem == null) { return false; }
            bool hasTwoSpaces = false;
            int spaceChk = ImportedItem.IndexOf(" ");
            if (spaceChk > -1)
            {
                int secSpace = ImportedItem.IndexOf(" ", spaceChk + 1);
                if (secSpace > -1) { hasTwoSpaces = true; }
            }

            return hasTwoSpaces;
        }

        private string GetCategoryParent(int thisIndent)
        {
            //if (importMode == "Opml") { thisIndent++; }
            string workingCat = IncomingCategory;
            string newParentPath = "";
            if (thisIndent == 0) 
            {
                previousCatIndent = thisIndent;
                return RootParentPath; 
            }
            if (thisIndent > previousCatIndent)
            {
                newParentPath = CurrentWorkingPath + "\\";
                previousCatIndent = thisIndent;
            }
            else
                { newParentPath = WalkBackPath(CurrentWorkingPath, thisIndent); }

            return newParentPath;
        }

        private string WalkBackPath(string CurrentWorkingPath, int thisIndent)
        {
            int slashesToKeep = thisIndent + 1;
            int startPoint = 0;
            int slashCount = 0;
            while (slashCount < slashesToKeep)
            {
                int getSlash = CurrentWorkingPath.IndexOf("\\", startPoint + 1);
                slashCount++;
                startPoint = getSlash;
            }

            string pathToReturn = CurrentWorkingPath.Substring(0, startPoint + 1);
            return pathToReturn;
        }

        private void AddToHalnaNote(string ImportedItem, int thisIndent)
        {
            if ((ImportedItem == "") && (thisIndent > 0))
             { 
                HalnaBuildNote += "\r\n\r\n";
                prevNoteLineBlank = true;
                return;
            }
            if ((HalnaBuildNote != "") && (!prevNoteLineBlank))
                { HalnaBuildNote += " "; } 
            HalnaBuildNote += ImportedItem.Trim();
            prevNoteLineBlank = false;
        }

        private void saveItemToDB(string importedItem, string importedNote)
        {
            if (importedItem == "") { return; }

            // Save imported item and any notes to the DB
            Console.WriteLine("Writing Item " + importedItem);
            newDateIn = "";
            CreateNewDBitem(importedItem, out newDBitemID);
            ItemCount++;

            if (importedNote == "") { return; }
            
            Console.WriteLine("Writing Note " + importedNote);
            myparent.ActiveTopItems.saveNewNoteInDBWrapper(newDBitemID, importedNote);
       }

        private int CountTabs(string importedItem)
        {
            int TabCount = 0;
            int chkFrom = 0;
            bool chkMore = true;
            while (chkMore)
            {
                if (importedItem.IndexOf("\t", chkFrom) > -1)
                {
                    TabCount++;
                    chkFrom = importedItem.IndexOf("\t", chkFrom) + 1;
                }
                else { chkMore = false; } 
            }

            return TabCount;
        }

        private string createNodesForPath(string categoryPath)
        {
            // See what the longest existing matching nodePath is
            string ExistPath = "";
            foreach (string thisPath in catFullPathList)
            {
                if ((categoryPath.IndexOf(thisPath) > -1) && (thisPath.Length > ExistPath.Length))
                    { ExistPath = thisPath; }
            }

            // Build necessary child nodes all the way down
            string newPathToBuild = categoryPath.Substring(ExistPath.Length + 1);
            string[] newCatsIn = newPathToBuild.Split(new char[] { '\\' });
            string startingPath = ExistPath;
            TreeNode nextPath = new TreeNode();

            foreach (string newCatDesc in newCatsIn)
            {
                nextPath = addNewCatNode(startingPath, newCatDesc);
                startingPath = nextPath.FullPath;
            }

            // Force category persistence            
            myparent.ActiveTopForm.tmrTVdirty_Tick(this, null);

            TreeViewForm.TagStruct thisTag = (TreeViewForm.TagStruct)nextPath.Tag;
            return thisTag.CatID;
        }

        private TreeNode addNewCatNode(string startingPath, string newCatDesc)
        {
            TreeNode newAddedNode;
            TreeNode myParentNode = myparent.FindNodeInTV(startingPath, null, false, "");

            newAddedNode = myparent.ActiveTopForm.SetupNewNode(myParentNode, newCatDesc);
            return newAddedNode;
        }

        private string QuoteCleanup(string MessyString)
        {
            string CleanString = MessyString;
            if (CleanString.Substring(0,1) == "\"")
                { CleanString = CleanString.Substring(1); }
            int csLen = CleanString.Length;
            if (CleanString.Substring(csLen-1,1) == "\"")
                { CleanString = CleanString.Substring(0, csLen - 1); }
            return CleanString;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MidStreamCancel = true;
        }

        private void lblImportUnassigned_Click(object sender, EventArgs e)
        {
            rbImportUnassigned.PerformClick();
        }

        private void lblImportCats_Click(object sender, EventArgs e)
        {
            rbImportCats.PerformClick();
        }

  
    }
}
