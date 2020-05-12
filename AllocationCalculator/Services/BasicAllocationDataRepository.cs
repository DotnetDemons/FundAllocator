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
        //string connection = System.Configuration.ConfigurationManager.ConnectionStrings["FundAllocation"].ConnectionString;

        string connection = "Data Source=UPENDRA-DEVINEN\\UPENDRALOCAL;Initial Catalog=AllocationCalculations;User Id = sa; Password = Sairam@123";

        public void InsertBasicAllocationSource(List<AllocationSourcesModel> sourcesModel)
        {
            DataTable basicSource = BuildBasicAllocationSource();
            foreach (var item in sourcesModel)
            {
                DataRow dr = basicSource.NewRow();
                dr["AUN"] = item.AUN;
                if (item.ProgramYear == null)
                {
                    dr["ProgramYear"] = DBNull.Value;
                }
                else
                {
                    dr["ProgramYear"] = item.ProgramYear;
                }
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
                dr["CharterSchoolAdjustment"] = DBNull.Value;
                dr["BasicAllocationAfterCS"] = DBNull.Value;
                dr["HoldHarmlessRate"] = DBNull.Value;
                dr["HoldHarmlessAmount"] = DBNull.Value;
                dr["HoldHarmlessCheck"] = DBNull.Value;
                dr["LEAAboveHoldHarmless"] = DBNull.Value;
                dr["AdjustedLEAAboveHoldHarmless"] = DBNull.Value;
                dr["AllocationstoAllLEA"] = DBNull.Value;
                dr["FinalBasicAllocationAmt"] = DBNull.Value;
                dr["SumBasicAllocationAfterCS"] = DBNull.Value;
                dr["SumAdjustedLEAsAboveHoldHarmless"] = DBNull.Value;
                dr["SumLEAsAboveHoldHarmless"] = DBNull.Value;

                basicSource.Rows.Add(dr);
            }

            SqlConnection con = new SqlConnection(connection);

            con.Open();

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
            objbulk.ColumnMappings.Add("CharterSchoolAdjustment", "CharterSchoolAdjustment");
            objbulk.ColumnMappings.Add("BasicAllocationAfterCS", "BasicAllocationAfterCS");
            objbulk.ColumnMappings.Add("HoldHarmlessRate", "HoldHarmlessRate");
            objbulk.ColumnMappings.Add("HoldHarmlessAmount", "HoldHarmlessAmount");
            objbulk.ColumnMappings.Add("HoldHarmlessCheck", "HoldHarmlessCheck");
            objbulk.ColumnMappings.Add("LEAAboveHoldHarmless", "LEAAboveHoldHarmless");
            objbulk.ColumnMappings.Add("AdjustedLEAAboveHoldHarmless", "AdjustedLEAAboveHoldHarmless");
            objbulk.ColumnMappings.Add("AllocationstoAllLEA", "AllocationstoAllLEA");
            objbulk.ColumnMappings.Add("FinalBasicAllocationAmt", "FinalBasicAllocationAmt");
            objbulk.ColumnMappings.Add("SumBasicAllocationAfterCS", "SumBasicAllocationAfterCS");
            objbulk.ColumnMappings.Add("SumAdjustedLEAsAboveHoldHarmless", "SumAdjustedLEAsAboveHoldHarmless");
            objbulk.ColumnMappings.Add("SumLEAsAboveHoldHarmless", "SumLEAsAboveHoldHarmless");
       
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
            tbl.Columns.Add(new DataColumn("CharterSchoolAdjustment", typeof(int)));
            tbl.Columns.Add(new DataColumn("BasicAllocationAfterCS", typeof(double)));
            tbl.Columns.Add(new DataColumn("HoldHarmlessRate", typeof(int)));
            tbl.Columns.Add(new DataColumn("HoldHarmlessAmount", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("HoldHarmlessCheck", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("LEAAboveHoldHarmless", typeof(double)));
            tbl.Columns.Add(new DataColumn("AdjustedLEAAboveHoldHarmless", typeof(double)));
            tbl.Columns.Add(new DataColumn("AllocationstoAllLEA", typeof(double)));
            tbl.Columns.Add(new DataColumn("FinalBasicAllocationAmt", typeof(double)));
            tbl.Columns.Add(new DataColumn("SumBasicAllocationAfterCS", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("SumAdjustedLEAsAboveHoldHarmless", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("SumLEAsAboveHoldHarmless", typeof(decimal)));

            return tbl;
        }
    }
}