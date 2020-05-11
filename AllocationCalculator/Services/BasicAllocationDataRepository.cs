using AllocationCalculator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AllocationCalculator.Services
{
    public class BasicAllocationDataRepository
    {
        string connection = System.Configuration.ConfigurationManager.ConnectionStrings["FundAllocation"].ConnectionString;
        public void InsertBasicAllocationSource(List<AllocationSourcesModel> sourcesModel)
        {
            DataTable basicSource = BuildBasicAllocationSource();
            foreach (var item in sourcesModel)
            {
                DataRow dr = basicSource.NewRow();
                dr["AUN"] = item.AUN;
                dr["ProgramYear"] = item.ProgramYear;
                if (item.BasicAllocation == null)
                {
                    dr["BasicAllocation"] = DBNull.Value;
                }
                else
                {
                    dr["BasicAllocation"] = item.BasicAllocation;
                }
                if (item.TotalForumlaCount == null)
                {
                    dr["TotalForumlaCount"] = DBNull.Value;
                }
                else
                {
                    dr["TotalForumlaCount"] = item.TotalForumlaCount;
                }
                if (item.POP517 == null)
                {
                    dr["POP517"] = DBNull.Value;
                }
                else
                {
                    dr["POP517"] = item.POP517;
                }
                if (item.PercentageFormula == null)
                {
                    dr["PercentageFormula"] = DBNull.Value;
                }
                else
                {
                    dr["PercentageFormula"] = item.PercentageFormula;
                }
                dr["CharterSchoolAdjustments"] = DBNull.Value;
                dr["BAllocationAfterCS"] = DBNull.Value;
                dr["HoldHarmlessRate"] = DBNull.Value;
                dr["HoldHarmlessAmount"] = DBNull.Value;
                dr["HoldHarmlessCheck"] = DBNull.Value;
                dr["LEAsAboveHoldHarmless"] = DBNull.Value;
                dr["AdjustedLEAsAboveHoldHarmless"] = DBNull.Value;
                dr["AllocationstoAllLEAS"] = DBNull.Value;
                dr["FINALAllocationAmount"] = DBNull.Value;
                dr["SumBAllocationAfterCS"] = DBNull.Value;
                dr["sumAdjustedLEAsAboveHoldHarmless"] = DBNull.Value;
                dr["sumLEAsAboveHoldHarmless"] = DBNull.Value;
                dr["ID"] = 0;
                basicSource.Rows.Add(dr);
            }

            SqlConnection con = new SqlConnection(connection);
            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            //truncate data from table
            string s = "Truncate Table tblBasicAllocationSource";
            SqlCommand Com = new SqlCommand(s, con);
            Com.ExecuteNonQuery();

            //assign Destination table name  
            objbulk.DestinationTableName = "tblBasicAllocationSource";

            objbulk.ColumnMappings.Add("AUN", "AUN");
            objbulk.ColumnMappings.Add("ProgramYear", "ProgramYear");
            objbulk.ColumnMappings.Add("BasicAllocation", "BasicAllocation");
            objbulk.ColumnMappings.Add("TotalForumlaCount", "TotalForumlaCount");
            objbulk.ColumnMappings.Add("POP517", "POP517");
            objbulk.ColumnMappings.Add("PercentageFormula", "PercentageFormula");
            objbulk.ColumnMappings.Add("CharterSchoolAdjustments", "CharterSchoolAdjustments");
            objbulk.ColumnMappings.Add("BAllocationAfterCS", "BAllocationAfterCS");
            objbulk.ColumnMappings.Add("HoldHarmlessRate", "HoldHarmlessRate");
            objbulk.ColumnMappings.Add("HoldHarmlessAmount", "HoldHarmlessAmount");
            objbulk.ColumnMappings.Add("HoldHarmlessCheck", "HoldHarmlessCheck");
            objbulk.ColumnMappings.Add("LEAsAboveHoldHarmless", "LEAsAboveHoldHarmless");
            objbulk.ColumnMappings.Add("AdjustedLEAsAboveHoldHarmless", "AdjustedLEAsAboveHoldHarmless");
            objbulk.ColumnMappings.Add("AllocationstoAllLEAS", "AllocationstoAllLEAS");
            objbulk.ColumnMappings.Add("FINALAllocationAmount", "FINALAllocationAmount");
            objbulk.ColumnMappings.Add("SumBAllocationAfterCS", "SumBAllocationAfterCS");
            objbulk.ColumnMappings.Add("sumAdjustedLEAsAboveHoldHarmless", "sumAdjustedLEAsAboveHoldHarmless");
            objbulk.ColumnMappings.Add("sumLEAsAboveHoldHarmless", "sumLEAsAboveHoldHarmless");
            objbulk.ColumnMappings.Add("ID", "ID");

            con.Open();
            //insert bulk Records into DataBase.  
            objbulk.WriteToServer(basicSource);
            con.Close();
        }

        private DataTable BuildBasicAllocationSource()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("AUN", typeof(int)));
            tbl.Columns.Add(new DataColumn("ProgramYear", typeof(int)));
            tbl.Columns.Add(new DataColumn("BasicAllocation", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("TotalForumlaCount", typeof(double)));
            tbl.Columns.Add(new DataColumn("POP517", typeof(double)));
            tbl.Columns.Add(new DataColumn("PercentageFormula", typeof(double)));
            tbl.Columns.Add(new DataColumn("CharterSchoolAdjustments", typeof(int)));
            tbl.Columns.Add(new DataColumn("BAllocationAfterCS", typeof(double)));
            tbl.Columns.Add(new DataColumn("HoldHarmlessRate", typeof(int)));
            tbl.Columns.Add(new DataColumn("HoldHarmlessAmount", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("HoldHarmlessCheck", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("LEAsAboveHoldHarmless", typeof(double)));
            tbl.Columns.Add(new DataColumn("AdjustedLEAsAboveHoldHarmless", typeof(double)));
            tbl.Columns.Add(new DataColumn("AllocationstoAllLEAS", typeof(double)));
            tbl.Columns.Add(new DataColumn("FINALAllocationAmount", typeof(double)));
            tbl.Columns.Add(new DataColumn("SumBAllocationAfterCS", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("sumAdjustedLEAsAboveHoldHarmless", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("sumLEAsAboveHoldHarmless", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("ID", typeof(int)));
            return tbl;
        }
    }
}