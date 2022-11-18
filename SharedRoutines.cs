using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace orGenta_NNv
{
    public class SharedRoutines
    {
        public void LogRTerror(string RoutineName, Exception ex)
        {
             using (StreamWriter sw = File.AppendText("ErrorLog.txt"))
            {
                string td = DateTime.Now.ToShortDateString();
                sw.WriteLine(td + " " + DateTime.Now.ToLongTimeString());
                sw.WriteLine(RoutineName);
                sw.WriteLine(ex.ToString());
                try { sw.WriteLine(ex.InnerException.ToString()); }
                catch { }
                sw.WriteLine(new String('-', 60));
            }
        }

        public void ShowErrDetails(string RoutineName, Exception ex, string ErrTitle)
        {
            ErrorDisplayDetail myErrDetail = new ErrorDisplayDetail();
            myErrDetail.Text = ErrTitle;
            string BuiltErrText = "Error in " + RoutineName + "\r\n";
            BuiltErrText += ex.ToString();
            try { BuiltErrText += ex.InnerException.ToString(); }
            catch { }
            myErrDetail.tbErrorDesc.Text = BuiltErrText;
            myErrDetail.ShowDialog();
        }

        public DataSet GetDataFor(string DataProvider, IDbConnection DBConnection, string getDatasetSQL)
        {
            DataSet myDS = new DataSet();
            DbProviderFactory myProviderFactory = DbProviderFactories.GetFactory(DataProvider);
            IDbDataAdapter sda = myProviderFactory.CreateDataAdapter();
            IDbCommand mySelCmd = DBConnection.CreateCommand();
            mySelCmd.CommandText = getDatasetSQL;
            mySelCmd.CommandType = CommandType.Text;
            sda.SelectCommand = mySelCmd;
            int myCatCounter = sda.Fill(myDS);
            return myDS;
        }

        public string CleanTheItem(string ItemToClean)
        {
            string holdItem = ItemToClean;
            holdItem = holdItem.Replace("'", "<*");
            holdItem = holdItem.Replace("<*", "''");
            return holdItem;
        }

    }
}
