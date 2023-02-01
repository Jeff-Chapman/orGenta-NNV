using System;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Data.SQLite;

namespace orGenta_NNv
{
    public partial class frmMain
    {
        #region Private Variables
        private string myServerType;
        private string myServerName;
        private string myKnowledgeDBname;
        private string myUserID;
        private string myPW;
        private bool RemoteConx = false;
        private string myConxString;
        private IDbConnection myDBconx;
        private bool dbIsConnected;
        private string activeDBname;
        private bool DBversionIsOk;
        private Version softwareVersion;
        private Version dBversion;
        private DBconxGet GetNewDB;
        #endregion

        private void connectTolocalCache()
        {
            string CacheConx = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=localCache.mdb;User Id=Admin;Password=;";
            try
            {
                DbProviderFactory myProviderFactory = DbProviderFactories.GetFactory("System.Data.OleDb");
                localCacheDBconx = myProviderFactory.CreateConnection();
                localCacheDBconx.ConnectionString = CacheConx;
                localCacheDBconx.Open();
            }
            catch (Exception ex)
            {
                if (testing) { myErrHandler.LogRTerror("connectTolocalCache", ex); }
                MessageBox.Show("Unable to connect to cache DB", "Connect Error");
                if (optLongErrMessages)
                { myErrHandler.ShowErrDetails("connectTolocalCache", ex, "DB Connection Error"); }
            }
        }

        private void setupBuiltinDBsettings()
        {
            myServerType = "MS Access";
            myServerName = ".";
            myKnowledgeDBname = "orGenta.mdb";
            myUserID = "Admin";
            myPW = "";
            DataProvider = "System.Data.OleDb";
            RemoteConx = false;
        }

        private void getDBconnxInfo()
        {
            getDBconnxInfo(false);
        }

        private void getDBconnxInfo(bool newFlag)
        {
            RetrievDBsetupInfo();
            GetNewDB.openDBdialog.CheckFileExists = true;
            GetNewDB.Text = "Connect to KB";
            if (newFlag) 
            {
                GetNewDB.openDBdialog.CheckFileExists = false;
                GetNewDB.tbDatabase.Text = "";
                GetNewDB.Text = "Create New KB";
            }
            
            GetNewDB.ShowDialog();

            myServerType = "MS Access";
            myServerName = GetNewDB.tbServer.Text;
            myKnowledgeDBname = GetNewDB.tbDatabase.Text;
            myUserID = "Admin";
            myPW = "";
            DataProvider = GetNewDB.DataProvider;
            RemoteConx = false;

            if (GetNewDB.cbUseAsDefault.Checked)
            {
                RegistryKey ThisUser = Registry.CurrentUser;
                SaveDefaultDBtoRegistry(ThisUser);
            }

        }

        private void RetrievDBsetupInfo()
        {
            GetNewDB = new DBconxGet();
            GetNewDB.tbServer.Text = myServerName;
            GetNewDB.tbDatabase.Text = myKnowledgeDBname;
            GetNewDB.testing = testing;
            GetNewDB.restoredDBinfo = restoredDBinfo;
        }

        private void SaveDefaultDBtoRegistry(RegistryKey ThisUser)
        {
            RegistryKey DBsettings = ThisUser.CreateSubKey("Software\\orGenta\\DBsettings");

            DBsettings.SetValue("RemoteConx", "0");
            DBsettings.SetValue("ServerType", myServerType);
            DBsettings.SetValue("ServerName", myServerName);
            DBsettings.SetValue("DBname", myKnowledgeDBname);
            DBsettings.SetValue("dbLoginID", myUserID);
            DBsettings.SetValue("dataProv", DataProvider);
        }

        private bool BuildAndValidateDBconx(bool isSilent)
        {
            this.Cursor = Cursors.WaitCursor;
            isItSQLite = false;

            myConxString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
            int dotLoc = myKnowledgeDBname.IndexOf(".");
            string dbExt = myKnowledgeDBname.Substring(dotLoc);
            isItOldMSaccess = true;
            if (dbExt == ".accdb") 
                { myConxString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
                isItOldMSaccess = false; }

            myConxString += myServerName + "\\" + myKnowledgeDBname + ";User Id=" + myUserID +
                ";Jet OLEDB:Encrypt Database=True;Jet OLEDB:Database Password=" + myPW + ";";

            if (dbExt == ".sqlite")
            {
                myConxString = "DSN=SQLite;DataSource=" + myServerName + "\\" + myKnowledgeDBname + ";Version=3;";
                DataProvider = "System.Data.Odbc";
                isItOldMSaccess = false;
                isItSQLite = true;
            }

            // Try to connect to the DB 

            if (testing)
            {
                using (StreamWriter sw = File.AppendText(LogfileName))
                { sw.WriteLine("Using Conx String: " + myConxString); }
            }

            dbIsConnected = false;
            {
                try
                {
                    DataTable provTable = DbProviderFactories.GetFactoryClasses();

                    DbProviderFactory myProviderFactory = DbProviderFactories.GetFactory(DataProvider);
                    myDBconx = myProviderFactory.CreateConnection();
                    myDBconx.ConnectionString = myConxString;

                    myDBconx.Open();
                    dbIsConnected = true;
                    activeDBname = myKnowledgeDBname; 
                    RLockOption = ""; 
                }
                catch (Exception ex)
                {
                    dbIsConnected = false;
                    this.Cursor = Cursors.Arrow;
                    if (testing) { myErrHandler.LogRTerror("BuildAndValidateDBconx", ex); }
                    if (!isSilent)
                    {
                        MessageBox.Show("Unable to connect to Database", "Connect Error");
                        if (optLongErrMessages)
                        { myErrHandler.ShowErrDetails("BuildAndValidateDBconx", ex, "DB Connection Error"); }
                    }
                }
            }

            DBversionIsOk = false;
            CheckDBversion();
            if (!DBversionIsOk)
            {
                try { myDBconx.Close(); }
                catch { }
                dbIsConnected = false;
            }
            return dbIsConnected;
        }

        private void StandardDBfixRoutine(string sqlForErrIDs, string myRoutineName, string guideErrRecovery, string foundDBfixErrs, string magicFixitCommand)
        {
            SharedRoutines DataGrabber = new SharedRoutines();
            DataTable RowsInError = new DataTable();
            try
            {
                DataSet myDS = DataGrabber.GetDataFor(DataProvider, myDBconx, sqlForErrIDs);
                RowsInError = myDS.Tables[0];
            }
            catch (Exception ex)
            {
                if (testing) { myErrHandler.LogRTerror(myRoutineName, ex); }
                MessageBox.Show(guideErrRecovery, "DB Read Error");
                if (optLongErrMessages)
                { myErrHandler.ShowErrDetails(myRoutineName, ex, "DB Read Error"); }
            }

            if (RowsInError.Rows.Count == 0) { return; }

            if (testing)
            {
                using (StreamWriter sw = File.AppendText(LogfileName))
                { sw.WriteLine(foundDBfixErrs); }
            }

            foreach (DataRow dbErrorRow in RowsInError.Rows)
            {
                string dbErrorRowID = dbErrorRow.ItemArray[0].ToString();
                string updCmdSQL = magicFixitCommand;
                updCmdSQL += dbErrorRowID + ")";

                if (testing)
                {
                    using (StreamWriter sw = File.AppendText(LogfileName))
                    { sw.Write(dbErrorRowID + " "); }
                }

                IDbCommand cmd = myDBconx.CreateCommand();
                cmd.CommandText = updCmdSQL;
                int rowsUpd = cmd.ExecuteNonQuery();
            }

            if (testing)
            {
                using (StreamWriter sw = File.AppendText(LogfileName))
                { sw.WriteLine(""); }
            }
        }

        private void CheckDBversion()
        {
            softwareVersion = Assembly.GetExecutingAssembly().GetName().Version;
            if (testing)
            {
                using (StreamWriter sw = File.AppendText(LogfileName))
                { sw.WriteLine("Software Version: {0}", softwareVersion.ToString()); }
            }
            if (!dbIsConnected) { return; }

            string GetItemCmd = "SELECT ParameterValue FROM Sysvars " + RLockOption +
                "WHERE ParameterName = 'Database Version'";
            IDbCommand cmd = myDBconx.CreateCommand();

            cmd.CommandText = GetItemCmd;
            try
            {
                string SoftwareKeyRetd = cmd.ExecuteScalar().ToString();
                dBversion = new Version(SoftwareKeyRetd);
            }
            catch
            {
                MessageBox.Show(this, "Invalid Database Version for orGenta 2.0");
                return;
            }


            if (testing)
            {
                using (StreamWriter sw = File.AppendText(LogfileName))
                {
                    sw.WriteLine("Database Version: {0}", dBversion.ToString());
                    sw.WriteLine("Data Provider: {0}", DataProvider);
                }
            }

            if (softwareVersion.CompareTo(dBversion) < 0)
            {
                string updMsg = "Please upgrade orGenta to " + dBversion.ToString();
                MessageBox.Show(this, updMsg);
                return;
            }
            if (dBversion.Major != softwareVersion.Major)
            {
                MessageBox.Show(this, "Invalid Database Version for orGenta 2.0");
                return;
            }
            DBversionIsOk = true;
        }

    }
}
