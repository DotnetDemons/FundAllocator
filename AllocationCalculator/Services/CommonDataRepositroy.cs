using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AllocationCalculator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AllocationCalculator.Services
{
    public class CommonDataRepositroy
    {
        //string connection = System.Configuration.ConfigurationManager.ConnectionStrings["FundAllocation"].ConnectionString;

        string connection = "Data Source=UPENDRA-DEVINEN\\UPENDRALOCAL;Initial Catalog=Title1Allocation;User Id = sa; Password = Sairam@123";

        private DataTable BuildSchoolDistrictTable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("AUN", typeof(int)));
            tbl.Columns.Add(new DataColumn("AgencyName", typeof(string)));
            tbl.Columns.Add(new DataColumn("ID", typeof(int)));
            return tbl;
        }

        public void InsertSchoolDistricts(List<SchoolDistrictsModel> districtsModels)
        {
            DataTable dt = BuildSchoolDistrictTable();
            foreach (var item in districtsModels)
            {
                if (item.AgencyName.ToLower() != "undistributed")
                {
                    DataRow dr = dt.NewRow();
                    dr["AUN"] = item.AUN;
                    dr["AgencyName"] = item.AgencyName;
                    dr["ID"] = 0;
                    dt.Rows.Add(dr);
                }
            }

            SqlConnection con = new SqlConnection(connection);
            con.Open();
            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            //truncate data from table
            string s = "Truncate Table tblSchoolDistrict";
            SqlCommand Com = new SqlCommand(s, con);
            Com.ExecuteNonQuery();

            //assign Destination table name  
            objbulk.DestinationTableName = "tblSchoolDistrict";

            objbulk.ColumnMappings.Add("AUN", "AUN");
            objbulk.ColumnMappings.Add("AgencyName", "AgencyName");
            objbulk.ColumnMappings.Add("ID", "ID");

            //insert bulk Records into DataBase.  
            objbulk.WriteToServer(dt);
            con.Close();
        }

        private DataTable BuildMapCharterSchooltoSDTable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("AUN", typeof(int)));
            tbl.Columns.Add(new DataColumn("AgencyName", typeof(string)));
            tbl.Columns.Add(new DataColumn("CSAUN", typeof(int)));
            tbl.Columns.Add(new DataColumn("CSAUNName", typeof(string)));
            tbl.Columns.Add(new DataColumn("NbrEnrolledStuds", typeof(double)));
            tbl.Columns.Add(new DataColumn("LowIncomePercentage", typeof(double)));
            tbl.Columns.Add(new DataColumn("FormulaStudents", typeof(double)));
            tbl.Columns.Add(new DataColumn("BasicAllocationPerPupilAmt", typeof(double)));
            tbl.Columns.Add(new DataColumn("ConcAllocationPerPupilAmt", typeof(double)));
            tbl.Columns.Add(new DataColumn("TargetedAllocationPerPupilAmt", typeof(double)));
            tbl.Columns.Add(new DataColumn("EFIGAllocationPerPupilAmount", typeof(double)));
            tbl.Columns.Add(new DataColumn("TotalSubtracted", typeof(double)));
            tbl.Columns.Add(new DataColumn("CID", typeof(int)));
            return tbl;
        }

        public void InsertMappingData(List<MapCharterSchooltoSdsModel> mapCharterModels)
        {
            DataTable dt = BuildMapCharterSchooltoSDTable();
            foreach (var item in mapCharterModels)
            {
                DataRow dr = dt.NewRow();
                dr["AUN"] = item.AUN;
                dr["AgencyName"] = item.AgencyName;
                dr["CSAUN"] = item.CSAUN;
                dr["CSAUNName"] = item.CSAUNName;
                dr["NbrEnrolledStuds"] = item.NbrEnrolledStuds;
                dr["LowIncomePercentage"] = DBNull.Value;
                dr["FormulaStudents"] = DBNull.Value;
                dr["BasicAllocationPerPupilAmt"] = DBNull.Value;
                dr["ConcAllocationPerPupilAmt"] = DBNull.Value;
                dr["TargetedAllocationPerPupilAmt"] = DBNull.Value;
                dr["EFIGAllocationPerPupilAmount"] = DBNull.Value;
                dr["TotalSubtracted"] = DBNull.Value;
                dr["CID"] = 0;
                dt.Rows.Add(dr);
            }

            SqlConnection con = new SqlConnection(connection);

            con.Open();

            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            //truncate data from table
            string s = "Truncate Table tblMapCharterSchooltoSD";
            SqlCommand Com = new SqlCommand(s, con);
            Com.ExecuteNonQuery();

            //assign Destination table name  
            objbulk.DestinationTableName = "tblMapCharterSchooltoSD";

            objbulk.ColumnMappings.Add("AUN", "AUN");
            objbulk.ColumnMappings.Add("AgencyName", "AgencyName");
            objbulk.ColumnMappings.Add("CSAUN", "CSAUN");
            objbulk.ColumnMappings.Add("CSAUNName", "CSAUNName");
            objbulk.ColumnMappings.Add("NbrEnrolledStuds", "NbrEnrolledStuds");
            objbulk.ColumnMappings.Add("LowIncomePercentage", "LowIncomePercentage");
            objbulk.ColumnMappings.Add("FormulaStudents", "FormulaStudents");
            objbulk.ColumnMappings.Add("BasicAllocationPerPupilAmt", "BasicAllocationPerPupilAmt");
            objbulk.ColumnMappings.Add("ConcAllocationPerPupilAmt", "ConcAllocationPerPupilAmt");
            objbulk.ColumnMappings.Add("TargetedAllocationPerPupilAmt", "TargetedAllocationPerPupilAmt");
            objbulk.ColumnMappings.Add("EFIGAllocationPerPupilAmount", "EFIGAllocationPerPupilAmount");
            objbulk.ColumnMappings.Add("TotalSubtracted", "TotalSubtracted");
            objbulk.ColumnMappings.Add("CID", "CID");

            
            //insert bulk Records into DataBase.  
            objbulk.WriteToServer(dt);
            con.Close();
        }

        private DataTable BuildBasicAllocationPreviousYearsDataTable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("AUN", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("ProgramYear", typeof(int)));
            tbl.Columns.Add(new DataColumn("StateDeterminedFinalAllocation", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("ID", typeof(int)));
            return tbl;
        }

        public void InsertPreviousYearsData(List<BasicAllocationPreviousYearsDataModel> previousYearsDataModels)
        {
            DataTable dt = BuildBasicAllocationPreviousYearsDataTable();
            foreach (var item in previousYearsDataModels)
            {
                DataRow dr = dt.NewRow();
                dr["AUN"] = item.AUN;
                dr["ProgramYear"] = item.ProgramYear;
                dr["StateDeterminedFinalAllocation"] = item.StateDeterminedFinalAllocation;
                dr["ID"] = 0;
                dt.Rows.Add(dr);
            }

            SqlConnection con = new SqlConnection(connection);

            con.Open();
            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            //truncate data from table
            string s = "Truncate Table tblBasicAllocationPreviousYearsData";
            SqlCommand Com = new SqlCommand(s, con);
            Com.ExecuteNonQuery();

            //assign Destination table name  
            objbulk.DestinationTableName = "tblBasicAllocationPreviousYearsData";

            objbulk.ColumnMappings.Add("AUN", "AUN");
            objbulk.ColumnMappings.Add("ProgramYear", "ProgramYear");
            objbulk.ColumnMappings.Add("StateDeterminedFinalAllocation", "StateDeterminedFinalAllocation");
            objbulk.ColumnMappings.Add("ID", "ID");

           
            //insert bulk Records into DataBase.  
            objbulk.WriteToServer(dt);
            con.Close();
        }


        private DataTable BuildConcAllocationPreviousYearDataTable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("AUN", typeof(int)));
            tbl.Columns.Add(new DataColumn("ProgramYear", typeof(int)));
            tbl.Columns.Add(new DataColumn("StateDeterminedFinalAllocation", typeof(decimal)));
            tbl.Columns.Add(new DataColumn("ID", typeof(int)));
            return tbl;
        }

        public void InsertConcPreviousYearsData(List<ConcAllocationPreviousYearsDataModel> concPreviousYearsDataModels)
        {
            DataTable dt = BuildConcAllocationPreviousYearDataTable();
            foreach (var item in concPreviousYearsDataModels)
            {
                DataRow dr = dt.NewRow();
                dr["AUN"] = item.AUN;
                dr["ProgramYear"] = item.ProgramYear;
                dr["StateDeterminedFinalAllocation"] = item.StateDeterminedFinalAllocation;
                dr["ID"] = 0;
                dt.Rows.Add(dr);
            }

            SqlConnection con = new SqlConnection(connection);

            con.Open();
            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            //truncate data from table
            string s = "Truncate Table tblConcAllocationPreviousYearsData";
            SqlCommand Com = new SqlCommand(s, con);
            Com.ExecuteNonQuery();

            //assign Destination table name  
            objbulk.DestinationTableName = "tblConcAllocationPreviousYearsData";

            objbulk.ColumnMappings.Add("AUN", "AUN");
            objbulk.ColumnMappings.Add("ProgramYear", "ProgramYear");
            objbulk.ColumnMappings.Add("StateDeterminedFinalAllocation", "StateDeterminedFinalAllocation");
            objbulk.ColumnMappings.Add("ID", "ID");

            
            //insert bulk Records into DataBase.  
            objbulk.WriteToServer(dt);
            con.Close();
        }

        private DataTable BuildCharterSchoolTable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("CSAUN", typeof(int)));
            tbl.Columns.Add(new DataColumn("CharterSchoolName", typeof(string)));
            tbl.Columns.Add(new DataColumn("CSID", typeof(int)));
            return tbl;
        }

        public void InsertCharterSchools(List<CharterSchoolsModel> schoolsModels)
        {
            DataTable dt = BuildCharterSchoolTable();
            foreach (var item in schoolsModels)
            {
                DataRow dr = dt.NewRow();
                dr["CSAUN"] = item.CSAUN;
                dr["CharterSchoolName"] = item.CharterSchoolName;
                dr["CSID"] = 0;
                dt.Rows.Add(dr);
            }

            SqlConnection con = new SqlConnection(connection);

            con.Open();
            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            //truncate data from table
            string s = "Truncate Table tblCharterSchool";
            SqlCommand Com = new SqlCommand(s, con);
            Com.ExecuteNonQuery();

            //assign Destination table name  
            objbulk.DestinationTableName = "tblCharterSchool";

            objbulk.ColumnMappings.Add("CSAUN", "CSAUN");
            objbulk.ColumnMappings.Add("CharterSchoolName", "CharterSchoolName");
            objbulk.ColumnMappings.Add("CSID", "CSID");

            
            //insert bulk Records into DataBase.  
            objbulk.WriteToServer(dt);
            con.Close();
        }

        private DataTable BuildConcAllocationEligibilityTable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("AUN", typeof(double)));
            tbl.Columns.Add(new DataColumn("LEA", typeof(string)));
            tbl.Columns.Add(new DataColumn("Year2017", typeof(double)));
            tbl.Columns.Add(new DataColumn("Year2016", typeof(double)));
            tbl.Columns.Add(new DataColumn("Year2015", typeof(double)));
            tbl.Columns.Add(new DataColumn("Year2014", typeof(double)));
            tbl.Columns.Add(new DataColumn("ID", typeof(int)));
            return tbl;
        }

        public void InsertConcEligibilityData(List<ConcAllocationEligibilityModel> eligibilityModels)
        {
            DataTable dt = BuildConcAllocationEligibilityTable();
            foreach (var item in eligibilityModels)
            {
                DataRow dr = dt.NewRow();
                dr["AUN"] = item.AUN;
                dr["LEA"] = item.C_LEA_;
                if (item.Year2017 == null)
                {
                    dr["Year2017"] = DBNull.Value;
                }
                else
                {
                    dr["Year2017"] = item.Year2017;
                }
                if (item.Year2016 == null)
                {
                    dr["Year2016"] = DBNull.Value;
                }
                else
                {
                    dr["Year2016"] = item.Year2016;
                }
                if (item.Year2015 == null)
                {
                    dr["Year2015"] = DBNull.Value;
                }
                else
                {
                    dr["Year2015"] = item.Year2015;
                }
                if (item.Year2014 == null)
                {
                    dr["Year2014"] = DBNull.Value;
                }
                else
                {
                    dr["Year2014"] = item.Year2014;
                }
                dr["ID"] = 0;
                dt.Rows.Add(dr);
            }

            SqlConnection con = new SqlConnection(connection);
            con.Open();
            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            //truncate data from table
            string s = "Truncate Table tblConcAllocationEligibility";
            SqlCommand Com = new SqlCommand(s, con);
            Com.ExecuteNonQuery();

            //assign Destination table name  
            objbulk.DestinationTableName = "tblConcAllocationEligibility";

            objbulk.ColumnMappings.Add("AUN", "AUN");
            objbulk.ColumnMappings.Add("LEA", "LEA");
            objbulk.ColumnMappings.Add("Year2017", "Year2017");
            objbulk.ColumnMappings.Add("Year2016", "Year2016");
            objbulk.ColumnMappings.Add("Year2015", "Year2015");
            objbulk.ColumnMappings.Add("Year2014", "Year2014");
            objbulk.ColumnMappings.Add("ID", "ID");


            //insert bulk Records into DataBase.  
            objbulk.WriteToServer(dt);
            con.Close();
        }
    }
}