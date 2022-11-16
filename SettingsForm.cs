using System;
using System.Windows.Forms;

namespace orGenta_NNv
{
    public partial class SettingsForm : Form
    {
        private frmMain myParent;
        private bool LongErrMessages;
        private int TVupdateInterval;
        private int TVupdateInterval2nd;
        private bool WrapMode;
        private bool AdjustItemsToP;
        private bool CreateCategories;
        private bool HighlightCats;

        public SettingsForm(frmMain parent)
        {
            InitializeComponent();
            myParent = parent;
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            LongErrMessages = myParent.optLongErrMessages;
            cbLongErrs.Checked = false;
            if (LongErrMessages)
                { cbLongErrs.Checked = true; }

            WrapMode = myParent.optWrapMode;
            cbWrapText.Checked = false;
            if (WrapMode)
                { cbWrapText.Checked = true; }

            AdjustItemsToP = myParent.optAdjustItemsToParent;
            cbAdjustItemsToP.Checked = false;
            if (AdjustItemsToP)
                { cbAdjustItemsToP.Checked = true; }

            TVupdateInterval = myParent.optTVupdateInterval / 1000;
            tbTVinterval1.Text = TVupdateInterval.ToString();
            TVupdateInterval2nd = myParent.optTVupdateInterval2nd / 1000;
            tbTVinterval2.Text = TVupdateInterval2nd.ToString();

            CreateCategories = myParent.optCreateCategories;
            cbTrayCreateCats.Checked = false;
            if (CreateCategories)
                { cbTrayCreateCats.Checked = true; }

            HighlightCats = myParent.optHighlightCats;
            cbHighlightCats.Checked = false;
            if (HighlightCats)
                { cbHighlightCats.Checked = true; }
        }

        private void cbLongErrs_Click(object sender, EventArgs e)
        {
            LongErrMessages = false;
            if (cbLongErrs.Checked)
                { LongErrMessages = true; }
        }

        private void cbWrapText_Click(object sender, EventArgs e)
        {
            WrapMode = false;
            if (cbWrapText.Checked)
                { WrapMode = true; }
        }

        private void cbAdjustItemsToP_Click(object sender, EventArgs e)
        {
            AdjustItemsToP = false;
            if (cbAdjustItemsToP.Checked)
                { AdjustItemsToP = true; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            myParent.optLongErrMessages = LongErrMessages;
            myParent.optTVupdateInterval = TVupdateInterval * 1000;
            myParent.optTVupdateInterval2nd = TVupdateInterval2nd * 1000;
            myParent.optWrapMode = WrapMode;
            myParent.optAdjustItemsToParent = AdjustItemsToP;
            myParent.optCreateCategories = CreateCategories;
            myParent.optHighlightCats = HighlightCats;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbTVinterval1_TextChanged(object sender, EventArgs e)
        {
            TVupdateInterval = Convert.ToInt16(tbTVinterval1.Text);
        }

        private void tbTVinterval2_TextChanged(object sender, EventArgs e)
        {
            TVupdateInterval2nd = Convert.ToInt16(tbTVinterval2.Text);
        }

        private void cbTrayCreateCats_Click(object sender, EventArgs e)
        {
            CreateCategories = false;
            if (cbTrayCreateCats.Checked)
                { CreateCategories = true; }
        }

        private void cbHighlightCats_Click(object sender, EventArgs e)
        {
            HighlightCats = false;
            if (cbHighlightCats.Checked)
                { HighlightCats = true; }
        }
    }
}
