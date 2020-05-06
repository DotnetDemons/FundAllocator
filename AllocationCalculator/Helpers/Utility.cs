using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace AllocationCalculator.Helpers
{
    public class Utility
    {
        public static DataTable ConvertXSLXtoDataTable(string strFilePath, string connString, int sheet)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                oledbConn.Open();
                using (DataTable Sheets = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null))
                {
                    if(Sheets.Rows.Count>0)
                    {
                        string worksheets = Sheets.Rows[sheet]["TABLE_NAME"].ToString();
                        OleDbCommand cmd = new OleDbCommand(String.Format("SELECT * FROM [{0}]", worksheets), oledbConn);
                        OleDbDataAdapter oleda = new OleDbDataAdapter();
                        oleda.SelectCommand = cmd;
                        oleda.Fill(ds);
                    }
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                oledbConn.Close();
            }
            return dt;
        }
    }
}