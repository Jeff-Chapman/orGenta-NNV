using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace orGenta_NNv
{
    public partial class frmMain
    {
        private void menuTrayed_Click(object sender, EventArgs e)
        {
            string TitleHolder = "orGentax";
            this.trayIconTrayed.Text = TitleHolder.Substring(0, 7);
            this.trayIconTrayed.Visible = true;
            mySideUtils.Visible = false;
            this.Visible = false;
            RunningMinimal = true;
            GetTextLineForm.AllCatList = "";
            string buildCatList = "";
            foreach(DataRow oneCat in ActiveTopForm.myCategoryTable.Rows)
                { buildCatList += oneCat.ItemArray[1].ToString() + " "; }
            GetTextLineForm.AllCatList = buildCatList.ToLower();
        }

        private void trayIconTrayed_Click(object sender, EventArgs e)
        {
            // Ignore single click if form is already shown
            if (GetTextLineForm.Visible == true)
            {
                GetTextLineForm.txtDataEntered.SelectAll();
                GetTextLineForm.Activate();
                GetTextLineForm.txtDataEntered.Focus();
                return;
            }
            Screen[] thisPCscreens = Screen.AllScreens;
            Rectangle ScreenSize = new Rectangle();
            ScreenSize = thisPCscreens[0].WorkingArea;

            // Locate the form at the bottom right of the screen
            GetTextLineForm.Left = ScreenSize.Width - GetTextLineForm.Width;
            GetTextLineForm.Top = ScreenSize.Height - 26;
            GetTextLineForm.lblKBname.Text = activeDBname;
            GetTextLineForm.txtDataEntered.SelectionStart = 0;
            GetTextLineForm.ResetEntryColor();
            string startingPrompt = "--> Enter a new Item, or a single word to Search";
            GetTextLineForm.txtDataEntered.Text = startingPrompt;
            GetTextLineForm.txtDataEntered.SelectAll();
            GetTextLineForm.userClickedControl = false;
            GetTextLineForm.wordToCheck = "";
            GetTextLineForm.newPotCats.Clear();
            GetTextLineForm.existingCats.Clear();
            GetTextLineForm.txtDataEntered.Focus();

            if (GetTextLineForm.ShowDialog(this) == DialogResult.OK)
            {
                ProcessMIentry(startingPrompt);
                return;
            }
            if (GetTextLineForm.txtDataEntered.Text == "")
            {
                //	user pressed the restore button
                trayIconTrayed_DoubleClick(GetTextLineForm.txtDataEntered, null);
                Application.DoEvents();
                return;
            }
        }

        private void ProcessMIentry(string startingPrompt)
        {
            //	If user entered only 1 word, then it's a search
            if (GetTextLineForm.txtDataEntered.Text.IndexOf(" ", 1) == -1)
            {
                SearchForMatchMI();
                return;
            }

            if (GetTextLineForm.txtDataEntered.Text == startingPrompt)
            { return; }

            // Copy potential new categories if that option is true
            if (optCreateCategories)
            {
                foreach (string oneNewCat in GetTextLineForm.newPotCats)
                    { AutoCreateCats.Add(oneNewCat); }
            }

            // New item entered: add to Unassigned
            TreeNode UnassignedParent = FindNodeInTV("Unassigned", "", true, "");
            TreeViewForm.TagStruct UAcatTag = (TreeViewForm.TagStruct)UnassignedParent.Tag;

            ItemsForm ShadowItemForm = new ItemsForm(ActiveTopForm);
            ShadowItemForm.categoryID = UAcatTag.CatID;
            ShadowItemForm.tbNewItem.Text = GetTextLineForm.txtDataEntered.Text;
            ShadowItemForm.NewNoteText = GetTextLineForm.NewNoteText;
            ShadowItemForm.FormIsShadow = true;
            ShadowItemForm.myDBconx = ActiveTopForm.myDBconx;
            ShadowItemForm.DataProvider = ActiveTopForm.DataProvider;
            ShadowItemForm.RLockOption = RLockOption;
            ShadowItemForm.myItemCleaner = new SharedRoutines();

            ShadowItemForm.btnOK_Click(this, null);
            GetTextLineForm.NewNoteText = "";

            // Note: there might be an issue here if the Shadow form deallocates
            //      before it finishes doing soft assigns...

            return;
        }

        private void SearchForMatchMI()
        {
            // Search categories first
            string MIsearch = GetTextLineForm.txtDataEntered.Text;
            findReplaying = false;
            ActiveTopForm.CatSearch(MIsearch);
            if (ActiveTopForm.tvCategories.SelectedNode.Text != "Main")
            {
                trayIconTrayed_DoubleClick(GetTextLineForm.txtDataEntered, null);
                Application.DoEvents();
                ActiveTopForm.tvCategories.Focus();
                return;
            }

            // Next search items and notes
            
            bool foundIt = ActiveTopForm.ItemSearch(MIsearch);
            if (foundIt)
            {
                trayIconTrayed_DoubleClick(GetTextLineForm.txtDataEntered, null);
                Application.DoEvents();
                return;
            }

            MessageBox.Show("No Match Found");
            return;
        }

        private void trayIconTrayed_DoubleClick(object sender, EventArgs e)
        {
            GetTextLineForm.Visible = false;
            if (this.WindowState == FormWindowState.Minimized)
            { this.WindowState = FormWindowState.Normal; }

            // Activate the main form.
            this.trayIconTrayed.Visible = false;
            this.Visible = true;
            mySideUtils.Visible = true;
            this.Activate();
            RunningMinimal = false;
            if (sender.ToString().IndexOf("NotifyIcon") >= 0)
            { Cursor.Position = new Point(this.Left + 600, this.Top + 600); }
            else
            { Cursor.Position = new Point(this.Left + 300, this.Top + 300); }
        }

    }
}
