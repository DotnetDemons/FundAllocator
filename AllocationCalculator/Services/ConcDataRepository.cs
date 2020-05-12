using AllocationCalculator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AllocationCalculator.Services
{
    public class ConcDataRepository
    {
        //string connection = System.Configuration.ConfigurationManager.
        //                           ConnectionStrings["FundAllocation"].ConnectionString;

        string connection = "Data Source=UPENDRA-DEVINEN\\UPENDRALOCAL;Initial Catalog=AllocationCalculations;User Id = sa; Password = Sairam@123";
        public void InsertConcAllocationSource(List<AllocationSourcesModel> sourcesModel)
        {
            DataTable concSource = BuildCONCSource();
            foreach (var item in sourcesModel)
            {
                DataRow dr = concSource.NewRow();
                dr["AUN"] = item.AUN;
                if (item.ProgramYear == null)
                {
                    dr["ProgramYear"] = DBNull.Value;
                }
                else
                {
                    dr["ProgramYear"] = item.ProgramYear;
                }
                if (item.ConcAllocation == null)
                {
                    dr["ConcAllocation"] = DBNull.Value;
                }
                else
                {
                    dr["ConcAllocation"] = item.ConcAllocation;
                }
                if (item.TotalForumlaCount == null)
                {
                    dr["TotalFormulaCount"] = DBNull.Value;
                }
                else
                {
                    dr["TotalFormulaCount"] = item.TotalForumlaCount;
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
                dr["ConcAllocationAfterCS"] = DBNull.Value;
                dr["HoldHarmlessRate"] = DBNull.Value;
                dr["HoldHarmlessAmount"] = DBNull.Value;
                dr["HoldHarmlessCheck"] = DBNull.Value;
                dr["LEAsAboveHoldHarmless"] = DBNull.Value;
                dr["AdjustedLEAAboveHoldHarmless"] = DBNull.Value;
                dr["AllocationstoAllLEA"] = DBNull.Value;
                dr["FinalConcAllocationAmount"] = DBNull.Value;
                dr["SumConcAllocationAfterCS"] = DBNull.Value;
                dr["SumAdjustedLEAsAboveHoldHarmless"] = DBNull.Value;
                dr["SumLEAAboveHoldHarmless"] = DBNull.Value;
                concSource.Rows.Add(dr);
            }

            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            
            objbulk.DestinationTableName = "tblConcAllocationSource";

            objbulk.ColumnMappings.Add("AUN", "AUN");
            objbulk.ColumnMappings.Add("ProgramYear", "ProgramYear");
            objbulk.ColumnMappings.Add("ConcAllocation", "ConcAllocation");
            objbulk.ColumnMappings.Add("TotalFormulaCount", "TotalFormulaCount");
            objbulk.ColumnMappings.Add("POP517", "POP517");
            objbulk.ColumnMappings.Add("PercentageFormula", "PercentageFormula");
            objbulk.ColumnMappings.Add("CharterSchoolAdjustment", "CharterSchoolAdjustment");
            objbulk.ColumnMappings.Add("ConcAllocationAfterCS", "ConcAllocationAfterCS");
            objbulk.ColumnMappings.Add("HoldHarmlessRate", "HoldHarmlessRate");
            objbulk.ColumnMappings.Add("HoldHarmlessAmount", "HoldHarmlessAmount");
            objbulk.ColumnMappings.Add("HoldHarmlessCheck", "HoldHarmlessCheck");
            objbulk.ColumnMappings.Add("LEAsAboveHoldHarmless", "LEAsAboveHoldHarmless");
            objbulk.ColumnMappings.Add("AdjustedLEAAboveHoldHarmless", "AdjustedLEAAboveHoldHarmless");
            objbulk.ColumnMappings.Add("AllocationstoAllLEA", "AllocationstoAllLEA");
            objbulk.ColumnMappings.Add("FinalConcAllocationAmount", "FinalConcAllocationAmount");
            objbulk.ColumnMappings.Add("SumConcAllocationAfterCS", "SumConcAllocationAfterCS");
            objbulk.ColumnMappings.Add("SumAdjustedLEAsAboveHoldHarmless", "SumAdjustedLEAsAboveHoldHarmless");
            objbulk.ColumnMappings.Add("SumLEAAboveHoldHarmless", "SumLEAAboveHoldHarmless");
   

            
            //insert bulk Records into DataBase.  
            objbulk.WriteToServer(concSource);
            con.Close();
        }

        private DataTable BuildCONCSource()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("AUN", typeof(int)));
            tbl.Columns.Add(new DataColumn("ProgramYear", typeof(int)));
            tbl.Columns.Add(new DataColumn("ConcAllocation", typeof(decimal)));

            tbl.Columns.Add(new DataColumn("TotalFormulaCount", typeof(double)));
            tbl.Columns.Add(new DataColumn("POP517", typeof(double)));
            tbl.Columns.Add(new DataColumn("PercentageFormula", typeof(double)));

            tbl.Columns.Add(new DataColumn("CharterSchoolAdjustment", typeof(int)));
            tbl.Columns.Add(new DataColumn("ConcAllocationAfterCS", typeof(double)));

            tbl.Columns.Add(new DataColumn("HoldHarmlessRate", typeof(int)));
            tbl.Columns.Add(new DataColumn("HoldHarmlessAmount", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("HoldHarmlessCheck", typeof(decimal)));

            tbl.Columns.Add(new DataColumn("LEAsAboveHoldHarmless", typeof(double)));
            tbl.Columns.Add(new DataColumn("AdjustedLEAAboveHoldHarmless", typeof(double)));
            tbl.Columns.Add(new DataColumn("AllocationstoAllLEA", typeof(double)));

            tbl.Columns.Add(new DataColumn("FinalConcAllocationAmount", typeof(double)));
            tbl.Columns.Add(new DataColumn("SumConcAllocationAfterCS", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("SumAdjustedLEAsAboveHoldHarmless", typeof(decimal)));

            tbl.Columns.Add(new DataColumn("SumLEAAboveHoldHarmless", typeof(decimal)));


            return tbl;
        }
    }
}