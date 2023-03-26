using System;
using System.Windows.Forms;

namespace orGenta_NNv
{
    static class Program
    {
        public static bool testing = false;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
