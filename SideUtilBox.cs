using System;
using System.Windows.Forms;

namespace orGenta_NNv
{
    // Note: all routines herein must set UtilCanBeSeen = true before
    //      returning focus to the main form

    public partial class SideUtilBox : Form
    {
        private frmMain myParent;
        public bool UtilCanBeSeen = false;
        private Form[] formsList;
        private string tgtType;

        public SideUtilBox(frmMain parent)
        {
            InitializeComponent();
            myParent = parent;
        }

        public void ShowMe()
        {
            this.Focus();
            UtilCanBeSeen = true;
            myParent.Focus();
        }

        private void lbOpenWindows_Click(object sender, EventArgs e)
        {
            myParent.BringToFront();
            string winForFocus;
            try
                { winForFocus = lbOpenWindows.SelectedItem.ToString(); }
            catch { return; }
            formsList = myParent.MdiChildren;
            int chkForKB = winForFocus.IndexOf("(KB)");
            if (chkForKB > 0)
            { 
                tgtType = "orGenta.TreeViewForm";
                winForFocus = winForFocus.Substring(0, chkForKB - 1);
            }
            else
                { tgtType = "orGenta.ItemsForm"; }
            foreach (Form chkForm in formsList)
            {
                string thisFormType = chkForm.GetType().ToString();
                if (thisFormType != tgtType) { continue; }
                if (chkForm.Text.IndexOf(winForFocus) > -1)
                    { chkForm.Activate(); }
            }
            if (chkForKB > 0) 
            {
                myParent.Focus();
                myParent.ActiveTopForm.Focus();
            }
        }

        private void lbCatList_Click(object sender, EventArgs e)
        {
            string catForFocus;
            try
                { catForFocus = lbCatList.SelectedItem.ToString(); }
            catch { return; }
            //myParent.txtCatSearch.Text = catForFocus;
            //myParent.txtCatSearch_Validating(this, null);
            myParent.findReplaying = false;
            myParent.ActiveTopForm.CatSearch(catForFocus);
            TreeNode catNode = myParent.ActiveTopForm.tvCategories.SelectedNode;
            myParent.findReplaying = true;
            while (catNode.Text != catForFocus)
            {
                myParent.ActiveTopForm.CatSearch(catForFocus);
                catNode = myParent.ActiveTopForm.tvCategories.SelectedNode;
            }
            int nodeTop = catNode.Bounds.Top;
            TreeNodeMouseClickEventArgs myArgs =
                new TreeNodeMouseClickEventArgs(myParent.ActiveTopForm.tvCategories.SelectedNode,
                    System.Windows.Forms.MouseButtons.Left, 1, 0, nodeTop);
            myParent.ActiveTopForm.tvCategories_NodeMouseClick(this, myArgs);
        }

        private void SideUtilBox_Load(object sender, EventArgs e)
        {
            int padWidth = 25;      // Note: debug is 25, release is 16 (!)
            lbCatList.Width = this.Width - padWidth;
            lbCatList.Height = this.Height - padWidth - 20;
            lbOpenWindows.Width = this.Width - padWidth;
            lbOpenWindows.Height = this.Height - padWidth - 20;
        }
    }
}
