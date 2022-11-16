using System;
using System.Windows.Forms;

namespace orGenta_NNv
{
    public partial class DelChoice : Form
    {
        public bool choiceCanceled = false;

        public DelChoice()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            choiceCanceled = true;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void delOption_Click(object sender, EventArgs e)
        {
            btnOK.Enabled = true;
        }
    }
}
