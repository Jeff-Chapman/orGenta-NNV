using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace orGenta_NNv
{
    public partial class frmMain
    {
        private Color FrozenColor = Color.BlueViolet;
        private Form[] formsList;
        
        private void menuOpenKB_Click(object sender, EventArgs e)
        {
            // opens an existing knowledge base

            getDBconnxInfo();
            if (KBsOpen.IndexOf(myKnowledgeDBname) > -1)
            {
                MessageBox.Show(myKnowledgeDBname + " Database is Already Open", "Connect Error");
                this.Cursor = Cursors.Arrow;
                return;
            }

            RLockOption = "";

            if (!BuildAndValidateDBconx(false)) { return; }

            DBshowTree();
        }

        private void DBshowTree()
        {
            this.Text = "Orgenta :: " + activeDBname;
            dbCleanupRoutines();
            CreateNewTree(myDBconx);
            KBsOpen.Add(activeDBname);
            menuCloseKB.Enabled = true;

            this.Cursor = Cursors.Arrow;
            this.Activate();
        }

        private void menuCloseKB_Click(object sender, EventArgs e)
        {
            string DBtoClose = ActiveTopForm.activeDBname;
            string confirmMsg = "Are you sure you want to close " + DBtoClose + "?";
            DialogResult CloseResp = MessageBox.Show(confirmMsg, "Confirm Close", MessageBoxButtons.OKCancel);
            if (CloseResp == DialogResult.Cancel) { return; }
            ActiveTopForm.myDBconx.Close();
            ActiveTopForm.Close();
            KBsOpen.Remove(DBtoClose);
            if (KBsOpen.Count == 1) { menuCloseKB.Enabled = false; }
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            using (About AboutBox = new About())
            {
                AboutBox.txtVersionNumber.Text = "Version " +
                    softwareVersion.Major.ToString() + "." + softwareVersion.Minor.ToString()
                    + "." + softwareVersion.Build.ToString();
                AboutBox.txtBuildNumber.Text = "Build " + softwareVersion.Revision.ToString();
                AboutBox.txtDotNetVsn.Text = "Runtime " + Environment.Version.ToString();
                AboutBox.tbIntialized.Text = "Initialized " + FirstKBdate;
                AboutBox.ShowDialog(this);
            }
        }

        private void menuFindCategory_Click(object sender, EventArgs e)
        {
            findReplaying = false;
            ActiveTopForm.CatSearch();
        }

        private void menuFindItem_Click(object sender, EventArgs e)
        {
            ActiveTopForm.ItemSearch();
        }

        private void menuAdvPrint_Click(object sender, EventArgs e)
        {
            if (!InitializePrinterSettings()) { return; }
            myPrinter.ShowDialog(this);
            myPrinter.prtPreviewWindow.InvalidatePreview();
        }

        private void menuPrinterSetup_Click(object sender, EventArgs e)
        {
            if (!weHaveAprinterObject)
            {
                myPrinter = new AdvancedPrint();
                weHaveAprinterObject = true;
            }

            ActiveTopForm.Activate();
            try
            {
                myPrinter.btnPrintSetup_Click(this, null);
                menuItemPrint.Enabled = true;
                menuAdvPrint.Enabled = true;
            }
            catch
                { MessageBox.Show("No Printer Found", "Print Error"); }
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
            return;
        }

        private void menuItemPrint_Click(object sender, EventArgs e)
        {
            if (!InitializePrinterSettings()) { return; }
            myPrinter.btnPrint_Click(this, null);
            myPrinter.prtPreviewWindow.InvalidatePreview();
        }

        private void menuFindNext_Click(object sender, EventArgs e)
        {
            findReplaying = true;
            try { ActiveTopForm.CatSearch(lastExecutedSearch); }
            catch { MessageBox.Show("Please find a Category (or part of one) first..."); }
        }

        public void menuAssignTo_Click(object sender, EventArgs e)
        {
            ArrayList AssignedItems = new ArrayList();
            bool gridHasItemsSelected = false;
            for (int i = 0; i < ActiveTopItems.ItemGrid.RowCount; i++)
            {
                if (ActiveTopItems.ItemGrid.Rows[i].Selected)
                {
                    gridHasItemsSelected = true;
                    AssignedItems.Add(ActiveTopItems.ItemGrid.Rows[i]);
                }
            }
            if (!gridHasItemsSelected)
            {
                MessageBox.Show(this, "Select Item Rows First");
                return;
            }

            CatAssignForm GetCatsForm = new CatAssignForm();
            foreach (TreeNode scanNode in ActiveTopForm.tvCategories.Nodes[0].Nodes)
            { AddToAssignCats(scanNode, GetCatsForm.chkListAssignedCats); }

            // Show the assignment form
            IDbCommand cmd = myDBconx.CreateCommand();
            if (GetCatsForm.ShowDialog(this) == DialogResult.OK)
            {
                for (int i = 0; i < AssignedItems.Count; i++)
                {
                    DataGridViewRow thisRow = (DataGridViewRow)AssignedItems[i];
                    string thisItemID = thisRow.Cells[3].Value.ToString();
                    AddCatsForItem(thisItemID, GetCatsForm);
                    // remove item from Unassigned category (flag its rel)
                    string updCmdSQL = "Update Rels SET isDeleted = 1 WHERE (Rels.ItemID = ";
                    updCmdSQL += thisItemID + " AND CategoryID = 2)";
                    cmd.CommandText = updCmdSQL;
                    int rowsUpd = cmd.ExecuteNonQuery();
                }
                // if Unassigned Items are ActiveTopItems then refresh its grid
                if (ActiveTopItems.categoryID == "2")
                {
                    ActiveTopItems.orGentaDBDataSet.Clear();
                    ActiveTopItems.ItemsForm_Load(this, null);
                }
            }
            GetCatsForm.Dispose();
        }

        private void menuExportCSV_Click(object sender, EventArgs e)
        {
            this.ExportItemsDialog.Filter = "Comma Seperated (*.csv)|*.csv";
            this.ExportItemsDialog.DefaultExt = "csv";
            if (this.ExportItemsDialog.ShowDialog(this) != DialogResult.OK) { return; }

            bool okayToExport = true;
            CheckIfExportOkay(ref okayToExport);

            if (!okayToExport)
            {
                MessageBox.Show("Unauthorized Access");
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            ActiveTopForm.BuildItemCatXref(ActiveTopItems);
            string ExportFileName = this.ExportItemsDialog.FileName;
            StreamWriter ExportFile = new StreamWriter(ExportFileName);
            ExportFile.Write("\"" + "Category Path" + "\",");

            // Headers
            for (int j = 0; j < ActiveTopItems.ItemGrid.ColumnCount; j++)
            {
                DataGridViewColumn ThisCol = ActiveTopItems.ItemGrid.Columns[j];
                ExportFile.Write("\"" + ThisCol.HeaderText + "\","); 
            }
            ExportFile.WriteLine("");

            // And now the grid data
            string CategoryPath = "";
            for (int i = 0; i < ActiveTopItems.ItemGrid.RowCount; i++)
            {
                CategoryPath = ExportOneRow(ExportFile, CategoryPath, i);
            }

            // Export Notes with their ItemID
            ExportFile.WriteLine("");
            ExportFile.WriteLine("\"ItemID\",\"Note\"");
            for (int i = 0; i < ActiveTopItems.ItemGrid.RowCount; i++)
            {
                int hasNote = Convert.ToInt32(ActiveTopItems.ItemGrid[0, i].Value);
                if (hasNote == 1)
                {
                    string ActiveItem = ActiveTopItems.ItemGrid[3, i].Value.ToString();
                    string getNote = "SELECT NoteValue FROM Notes " + ActiveTopForm.RLockOption;
                    getNote += "WHERE ItemID = " + ActiveItem;
                    IDbCommand cmd = ActiveTopForm.myDBconx.CreateCommand();
                    cmd.CommandText = getNote;
                    try
                    {
                        string noteTextBack = cmd.ExecuteScalar().ToString();
                        ExportFile.Write(ActiveItem + ",\"");
                        ExportFile.WriteLine(noteTextBack + "\"");
                    }
                    catch { }
                }
            }

            ExportFile.Flush();
            ExportFile.Close();
            ExportFile.Dispose();
            Cursor = Cursors.Default;
        }

        private string ExportOneRow(StreamWriter ExportFile, string CategoryPath, int rowOut)
        {
            for (int colNum = 0; colNum < ActiveTopItems.ItemGrid.ColumnCount; colNum++)
            {
                if (colNum == 0)
                {
                    CategoryPath = ActiveTopItems.ItemsCatXrefArray[rowOut].ToString();
                    ExportFile.Write("\"" + CategoryPath + "\",");
                }

                DataGridViewCell DataOut = ActiveTopItems.ItemGrid[colNum, rowOut];

                //	Special formatting for date fields

                string ColType = DataOut.ValueType.Name;
                if (ColType == "DateTime")
                {
                    try
                        { ExportFile.Write("\"" + Convert.ToDateTime(DataOut.Value) + "\","); }
                    catch
                        { ExportFile.Write("\"" + "\","); }
                }
                else
                    { ExportFile.Write("\"" + DataOut.Value.ToString() + "\","); }

                if (colNum == ActiveTopItems.ItemGrid.ColumnCount - 1)
                    { ExportFile.WriteLine(""); }
            }
            return CategoryPath;
        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            SettingsForm mySettings = new SettingsForm(this);
            mySettings.ShowDialog();
        }

        private void menuOnlineHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("orGenta User Guide.pdf");
        }

        public void menuZoomItem_Click(object sender, EventArgs e)
        {
            using (ZoomedItem ThisItemZoom = new ZoomedItem())
            {
                int ActiveRow = ActiveTopItems.ItemGrid.CurrentCell.RowIndex;
                ThisItemZoom.txtZoomBox.Text = ActiveTopItems.ItemGrid[1, ActiveRow].Value.ToString();
                ThisItemZoom.txtZoomBox.SelectionStart = ThisItemZoom.txtZoomBox.Text.Length;
                ThisItemZoom.txtZoomBox.ReadOnly = true;
                ThisItemZoom.txtZoomBox.BackColor = Color.White;
                ThisItemZoom.txtZoomBox.ForeColor = Color.Black;
                ThisItemZoom.ShowDialog();
            }
        }

        private void menuImportItems_Click(object sender, EventArgs e)
        {
            ImportStuff myImportForm = new ImportStuff(this);
            myImportForm.ShowDialog(this);
        }

        private void menuItems2email_Click(object sender, EventArgs e)
        {

            bool okayToExport = true;
            CheckIfExportOkay(ref okayToExport);

            if (!okayToExport)
            {
                MessageBox.Show("Unauthorized Access");
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            string eMailBody = "";

            for (int i = 0; i < ActiveTopItems.ItemGrid.RowCount; i++)
            {
                DataGridViewCell DataOut = ActiveTopItems.ItemGrid[1, i];
                string thisItem = DataOut.Value.ToString();
                eMailBody += thisItem + "%0D%0A%0D%0A";

                int hasNote = Convert.ToInt32(ActiveTopItems.ItemGrid[0, i].Value);
                if (hasNote == 1)
                {
                    string ActiveItem = ActiveTopItems.ItemGrid[3, i].Value.ToString();
                    string getNote = "SELECT NoteValue FROM Notes " + ActiveTopForm.RLockOption;
                    getNote += "WHERE ItemID = " + ActiveItem;
                    IDbCommand cmd = ActiveTopForm.myDBconx.CreateCommand();
                    cmd.CommandText = getNote;
                    try
                    {
                        string noteTextBack = cmd.ExecuteScalar().ToString();
                        eMailBody += noteTextBack + "%0D%0A%0D%0A";
                    }
                    catch { }
                }
                
            }

            string ShareMailMessage = "mailto:[fillinWho@fillinWhere.com]?Subject=Some Notes About " + ActiveTopItems.Text;
            ShareMailMessage += "&Body=" + eMailBody;
            System.Diagnostics.Process.Start(ShareMailMessage);

            Cursor = Cursors.Default;
        }

        private void CheckIfExportOkay(ref bool okayToExport)
        {
            okayToExport = true;
        }

        private void menuExpandAll_Click(object sender, EventArgs e)
        {
            ActiveTopForm.tvCategories.BeginUpdate();
            ActiveTopForm.tvCategories.ExpandAll();
            CollapseFrozenNodes(ActiveTopForm.tvCategories.Nodes[0]);
            try
                { ActiveTopForm.tvCategories.SelectedNode.EnsureVisible(); }
            catch { }
            ActiveTopForm.tvCategories.EndUpdate();
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

        private void menuCollapseAll_Click(object sender, EventArgs e)
        {
            ActiveTopForm.tvCategories.CollapseAll();
            ActiveTopForm.tvCategories.Nodes[0].Expand();
            ActiveTopForm.tvCategories.SelectedNode = ActiveTopForm.tvCategories.GetNodeAt(1, 1);
        }

        private void menuExpandNode_Click(object sender, EventArgs e)
        {
            ActiveTopForm.expandNodeToolStripMenuItem_Click(this, null);
        }

        private void menuCollapseNode_Click(object sender, EventArgs e)
        {
            ActiveTopForm.collapseNodeToolStripMenuItem_Click(this, null);
        }

        private void menuExpandThis_Click(object sender, EventArgs e)
        {
            ActiveTopForm.expandOnlyThisToolStripMenuItem_Click(this, null);
        }

        private void menuShare_Click(object sender, EventArgs e)
        {
            // Create an eMail message the user can send to friends
            string shareMessageBody = "Hi [friend name(s)],%0D%0A%0D%0A";
            shareMessageBody += "I've been using some new knowledge management software I found called orGenta ";
            shareMessageBody += "for a few months now, and I really like it. It's especially useful for rapidly ";
            shareMessageBody += "capturing critical info that I need to refer to in my job, and it's also ";
            shareMessageBody += "especially great for short notes to keep track of the social things I'd ";
            shareMessageBody += "like to quickly recall for my friends and coworkers.%0D%0A%0D%0A";
            shareMessageBody += "Thought I'd pass this along anyhow, and if you'd like to give it a try you can ";
            shareMessageBody += "download it from the company that makes it, called Izard Exploratory LLC. ";
            shareMessageBody  += "(The single-user version is free.)%0D%0A%0D%0A";
            shareMessageBody += "Sincerely, Your Friend,";
            string ShareMailMessage = "mailto:[fillinAfriend@fillinAserver.com]?Subject=orGenta Software";
            ShareMailMessage += "&Body=" + shareMessageBody;
            System.Diagnostics.Process.Start(ShareMailMessage);
        }

        private void menuFeedback_Click(object sender, EventArgs e)
        {
            // Send us feedback
            string shareMessageBody = "Hello Izard,%0D%0A%0D%0A";
            shareMessageBody += "I've been using orGenta since ";
            RegistryKey ThisUser = Registry.CurrentUser;
            string FirstKBdate = "[when]";
            try
            {
                RegistryKey DBsettings = ThisUser.OpenSubKey("Software\\orGenta\\1stLogin", true);
                FirstKBdate = DBsettings.GetValue("ConxDate").ToString();
            }
            catch {  }
            shareMessageBody += FirstKBdate + ", and I really like it. What I like most about it is ";
            shareMessageBody += "[please tell us!]. It would be great however if [your suggestions].%0D%0A%0D%0A";
            shareMessageBody += "Please let me know your thoughts on this and when it might become available.%0D%0A%0D%0A";
            shareMessageBody += "Sincerely, Your Friend,";
            string ShareMailMessage = "mailto:ideas@izard-llc.com?Subject=orGenta Suggestion";
            ShareMailMessage += "&Body=" + shareMessageBody;
            System.Diagnostics.Process.Start(ShareMailMessage);
        }

        private void menuCloseItems_Click(object sender, EventArgs e)
        {
            formsList = this.MdiChildren;
            foreach (Form chkForm in formsList)
            {
                string thisFormType = chkForm.GetType().ToString();
                if (thisFormType != "orGenta_NNv.ItemsForm") { continue; }
                chkForm.Close();
            }
        }

        private void menuAutoAssign_Click(object sender, EventArgs e)
        {
            try { ActiveTopItems.AutoAssign_Items(); }
            catch
            {
                string aaErrMsg = "Click on a Category to open its Items first, before starting Auto-Assign";
                MessageBox.Show(aaErrMsg);
            }
        }

        private void menuNewKDB_Click(object sender, EventArgs e)
        {
            WarnNewKDB myWarning = new WarnNewKDB();
            myWarning.ShowDialog();
            if (!myWarning.newKBokay) { return; }

            getDBconnxInfo(true);
            if (myServerType != "MS Access") { return; }    

            if ((RemoteConx) && (myServerName == ".")) { return; }

            string FullKBpathName = myServerName + "\\" + myKnowledgeDBname;

            if (File.Exists("orgSeed.mdb"))
            {
                int lenFKB = FullKBpathName.Length;
                if (FullKBpathName.Substring(lenFKB - 4, 4) != ".mdb")
                    { FullKBpathName += ".mdb"; }
                try
                {
                    File.Copy("orgSeed.mdb", FullKBpathName, false);
                    File.SetAttributes(FullKBpathName, FileAttributes.Normal);
                }
                catch (Exception ex)
                {
                    string copyErr = ex.Message;
                    MessageBox.Show("Failed to create " + myKnowledgeDBname + ". " + copyErr);
                    return;
                }
            }
            else
            {
                int lenFKB = FullKBpathName.Length;
                if (FullKBpathName.Substring(lenFKB - 4, 4) == ".mdb")
                    { FullKBpathName = FullKBpathName.Replace(".mdb", ".accdb"); }
                else
                {
                    if (FullKBpathName.Substring(lenFKB - 6, 6) != ".accdb")
                        { FullKBpathName += ".accdb"; }
                }

                try
                {
                    File.Copy("orgSeed.accdb", FullKBpathName, false);
                    File.SetAttributes(FullKBpathName, FileAttributes.Normal);
                }
                catch (Exception ex)
                {
                    string copyErr = ex.Message;
                    MessageBox.Show("Failed to create " + myKnowledgeDBname + ". " + copyErr);
                    return;
                }
            }

            RLockOption = "";
            int hasDot = myKnowledgeDBname.IndexOf(".");
            int KBnameStarts = FullKBpathName.IndexOf(myKnowledgeDBname.Substring(0,hasDot));
            myKnowledgeDBname = FullKBpathName.Substring(KBnameStarts);

            if (!BuildAndValidateDBconx(false)) { return; }

            DBshowTree();
        }

    }
}
