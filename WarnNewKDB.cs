using System;
using System.Windows.Forms;

namespace orGenta_NNv
{

    public partial class WarnNewKDB : Form
    {
        public bool newKBokay = false;

        public WarnNewKDB()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnCancel.Text == "Next >>")
                { newKBokay = true; }

            this.Close();
        }

        private void cbUnderstand_Click(object sender, EventArgs e)
        {
            btnCancel.Text = "Next >>";
        }
    }
}
