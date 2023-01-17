using System;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace orGenta_NNv
{
    public partial class DBconxGet : Form
    {
        public bool restoredDBinfo;
        public bool testing = false;
        public string DataProvider = "System.Data.OleDb";

        public DBconxGet()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string svLoc = tbServer.Text;
            string localName = Environment.MachineName.ToString();
            if ((svLoc.IndexOf(localName) < 0) && (svLoc != ".") && (svLoc.Substring(0,2) != "C:"))
            {
                MessageBox.Show("Free version requires a Local DB", "Connect Error");
                return;
            }
      
            int dbnLen = tbDatabase.Text.Length;

            if (dbnLen < 4) { tbDatabase.Text += ".mdb"; }
            bool hasGoodExt = false;
            int dotLoc = tbDatabase.Text.IndexOf(".");
            string testExt = tbDatabase.Text.Substring(dotLoc);
            if ((testExt == ".mdb") || (testExt == ".accdb")) { hasGoodExt = true; }
            if (!hasGoodExt) { tbDatabase.Text += ".mdb"; }

            this.Close();
        }


        private void DBconxGet_Activated(object sender, EventArgs e)
        {
            if (restoredDBinfo)
                { cbUseAsDefault.Visible = false; }
        }

        private void tbServer_Click(object sender, EventArgs e)
        {
            cbUseAsDefault.Visible = true;
        }

        private void tbDatabase_Click(object sender, EventArgs e)
        {
            cbUseAsDefault.Visible = true;
        }

        private void btnSelectDB_Click(object sender, EventArgs e)
        {
            openDBdialog.Filter = "MS Access (*.mdb,*.accdb)|*.mdb;*.accdb";
            openDBdialog.DefaultExt = "mdb";
            if (openDBdialog.ShowDialog(this) == DialogResult.OK)
            {
                string dbFullName = openDBdialog.FileName;
                int lastSlash = 0;
                for (int i = dbFullName.Length - 1; i > 0;  i--)
                {
                    if (dbFullName.Substring(i,1) == "\\")
                    {
                        lastSlash = i;
                        break;
                    }
                }
                tbServer.Text = dbFullName.Substring(0,lastSlash);
                tbDatabase.Text = dbFullName.Substring(lastSlash + 1);
            }
        }

    }
}
