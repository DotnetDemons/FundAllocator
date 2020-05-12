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

        string connection = "Data Source=UPENDRA-DEVINEN\\UPENDRALOCAL;Initial Catalog=AllocationCalculations;User Id = sa; Password = Sairam@123";

        private DataTable BuildSchoolDistrictTable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("AUN", typeof(int)));
            tbl.Columns.Add(new DataColumn("Name", typeof(string)));
            tbl.Columns.Add(new DataColumn("IsCharterSchool", typeof(bool)));
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
                    dr["Name"] = item.AgencyName;
                    dr["IsCharterSchool"] = item.IsCharterSchool;
                    dt.Rows.Add(dr);
                }
            }

            SqlConnection con = new SqlConnection(connection);
            con.Open();
            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            //truncate data from table
            string maptable = "Truncate Table tblMapCharterSchooltoSchoolDistrict";
            SqlCommand mapCom = new SqlCommand(maptable, con);
            mapCom.ExecuteNonQuery();

            //truncate data from table
            string basicSource = "Truncate Table tblBasicAllocationSource";
            SqlCommand BasicSourceCom = new SqlCommand(basicSource, con);
            BasicSourceCom.ExecuteNonQuery();

            //truncate data from table
            string concSource = "Truncate Table tblConcAllocationSource";
            SqlCommand ConcSourceCom = new SqlCommand(concSource, con);
            ConcSourceCom.ExecuteNonQuery();

            //truncate data from table
            string prevTable = "Truncate Table tblBasicAllocationPreviousYear";
            SqlCommand PrevCom = new SqlCommand(prevTable, con);
            PrevCom.ExecuteNonQuery();

            //truncate data from table
            string concPrev = "Truncate Table tblConcAllocationPreviousYear";
            SqlCommand ConcPrevCom = new SqlCommand(concPrev, con);
            ConcPrevCom.ExecuteNonQuery();

            //truncate data from table
            string concElig = "Truncate Table tblConcAllocationEligibility";
            SqlCommand ConcEligCom = new SqlCommand(concElig, con);
            ConcEligCom.ExecuteNonQuery();


            //truncate data from table
            string s = "Delete tblLEA";
            SqlCommand Com = new SqlCommand(s, con);
            Com.ExecuteNonQuery();

            //assign Destination table name  
            objbulk.DestinationTableName = "tblLEA";

            objbulk.ColumnMappings.Add("AUN", "AUN");
            objbulk.ColumnMappings.Add("Name", "Name");
            objbulk.ColumnMappings.Add("IsCharterSchool", "IsCharterSchool");

            //insert bulk Records into DataBase.  
            objbulk.WriteToServer(dt);
            con.Close();
        }

        private DataTable BuildMapCharterSchooltoSDTable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("Charter_AUN", typeof(int)));
            tbl.Columns.Add(new DataColumn("LEA_AUN", typeof(int)));
            tbl.Columns.Add(new DataColumn("NbrEnrolledStudents", typeof(double)));
            tbl.Columns.Add(new DataColumn("LowIncomePercentage", typeof(double)));
            tbl.Columns.Add(new DataColumn("FormulaStudents", typeof(double)));
            tbl.Columns.Add(new DataColumn("BasicAllocationPerPupilAmt", typeof(double)));
            tbl.Columns.Add(new DataColumn("ConcAllocationPerPupilAmt", typeof(double)));
            tbl.Columns.Add(new DataColumn("TargetedAllocationPerPupilAmt", typeof(double)));
            tbl.Columns.Add(new DataColumn("EFIGAllocationPerPupilAmt", typeof(double)));
            tbl.Columns.Add(new DataColumn("TotalSubtracted", typeof(double)));
            return tbl;
        }

        public void InsertMappingData(List<MapCharterSchooltoSdsModel> mapCharterModels)
        {
            DataTable dt = BuildMapCharterSchooltoSDTable();
            foreach (var item in mapCharterModels)
            {
                DataRow dr = dt.NewRow();
                dr["Charter_AUN"] = item.CSAUN;
                dr["LEA_AUN"] = item.AUN;
                dr["NbrEnrolledStudents"] = item.NbrEnrolledStuds;
                dr["LowIncomePercentage"] = DBNull.Value;
                dr["FormulaStudents"] = DBNull.Value;
                dr["BasicAllocationPerPupilAmt"] = DBNull.Value;
                dr["ConcAllocationPerPupilAmt"] = DBNull.Value;
                dr["TargetedAllocationPerPupilAmt"] = DBNull.Value;
                dr["EFIGAllocationPerPupilAmt"] = DBNull.Value;
                dr["TotalSubtracted"] = DBNull.Value;
                dt.Rows.Add(dr);
            }

            SqlConnection con = new SqlConnection(connection);

            con.Open();

            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);



            //assign Destination table name  
            objbulk.DestinationTableName = "tblMapCharterSchooltoSchoolDistrict";

            objbulk.ColumnMappings.Add("Charter_AUN", "Charter_AUN");
            objbulk.ColumnMappings.Add("LEA_AUN", "LEA_AUN");
            objbulk.ColumnMappings.Add("NbrEnrolledStudents", "NbrEnrolledStudents");
            objbulk.ColumnMappings.Add("LowIncomePercentage", "LowIncomePercentage");
            objbulk.ColumnMappings.Add("FormulaStudents", "FormulaStudents");
            objbulk.ColumnMappings.Add("BasicAllocationPerPupilAmt", "BasicAllocationPerPupilAmt");
            objbulk.ColumnMappings.Add("ConcAllocationPerPupilAmt", "ConcAllocationPerPupilAmt");
            objbulk.ColumnMappings.Add("TargetedAllocationPerPupilAmt", "TargetedAllocationPerPupilAmt");
            objbulk.ColumnMappings.Add("EFIGAllocationPerPupilAmt", "EFIGAllocationPerPupilAmt");
            objbulk.ColumnMappings.Add("TotalSubtracted", "TotalSubtracted");


            //insert bulk Records into DataBase.  
            objbulk.WriteToServer(dt);
            con.Close();
        }

        private DataTable BuildBasicAllocationPreviousYearsDataTable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("AUN", typeof(int)));
            tbl.Columns.Add(new DataColumn("ProgramYear", typeof(int)));
            tbl.Columns.Add(new DataColumn("StateDeterminedFinalAllocationAmt", typeof(decimal)));
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
                dr["StateDeterminedFinalAllocationAmt"] = item.StateDeterminedFinalAllocation;
                dt.Rows.Add(dr);
            }

            SqlConnection con = new SqlConnection(connection);

            con.Open();
            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);



            //assign Destination table name  
            objbulk.DestinationTableName = "tblBasicAllocationPreviousYear";

            objbulk.ColumnMappings.Add("AUN", "AUN");
            objbulk.ColumnMappings.Add("ProgramYear", "ProgramYear");
            objbulk.ColumnMappings.Add("StateDeterminedFinalAllocationAmt", "StateDeterminedFinalAllocationAmt");


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
                dt.Rows.Add(dr);
            }

            SqlConnection con = new SqlConnection(connection);

            con.Open();
            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);


            //assign Destination table name  
            objbulk.DestinationTableName = "tblConcAllocationPreviousYear";

            objbulk.ColumnMappings.Add("AUN", "AUN");
            objbulk.ColumnMappings.Add("ProgramYear", "ProgramYear");
            objbulk.ColumnMappings.Add("StateDeterminedFinalAllocation", "StateDeterminedFinalAllocation");



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

        //public void InsertCharterSchools(List<CharterSchoolsModel> schoolsModels)
        //{
        //    DataTable dt = BuildCharterSchoolTable();
        //    foreach (var item in schoolsModels)
        //    {
        //        DataRow dr = dt.NewRow();
        //        dr["CSAUN"] = item.CSAUN;
        //        dr["CharterSchoolName"] = item.CharterSchoolName;
        //        dr["CSID"] = 0;
        //        dt.Rows.Add(dr);
        //    }

        //    SqlConnection con = new SqlConnection(connection);

        //    con.Open();
        //    //create object of SqlBulkCopy which help to insert  
        //    SqlBulkCopy objbulk = new SqlBulkCopy(con);

        //    //truncate data from table
        //    string s = "Truncate Table tblCharterSchool";
        //    SqlCommand Com = new SqlCommand(s, con);
        //    Com.ExecuteNonQuery();

        //    //assign Destination table name  
        //    objbulk.DestinationTableName = "tblCharterSchool";

        //    objbulk.ColumnMappings.Add("CSAUN", "CSAUN");
        //    objbulk.ColumnMappings.Add("CharterSchoolName", "CharterSchoolName");
        //    objbulk.ColumnMappings.Add("CSID", "CSID");


        //    //insert bulk Records into DataBase.  
        //    objbulk.WriteToServer(dt);
        //    con.Close();
        //}

        private DataTable BuildConcAllocationEligibilityTable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("AUN", typeof(int)));
            tbl.Columns.Add(new DataColumn("LEA", typeof(string)));
            tbl.Columns.Add(new DataColumn("PreviousYear1", typeof(double)));
            tbl.Columns.Add(new DataColumn("PreviousYear2", typeof(double)));
            tbl.Columns.Add(new DataColumn("PreviousYear3", typeof(double)));
            tbl.Columns.Add(new DataColumn("PreviousYear4", typeof(double)));
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
                    dr["PreviousYear1"] = DBNull.Value;
                }
                else
                {
                    dr["PreviousYear1"] = item.Year2017;
                }
                if (item.Year2016 == null)
                {
                    dr["PreviousYear2"] = DBNull.Value;
                }
                else
                {
                    dr["PreviousYear2"] = item.Year2016;
                }
                if (item.Year2015 == null)
                {
                    dr["PreviousYear3"] = DBNull.Value;
                }
                else
                {
                    dr["PreviousYear3"] = item.Year2015;
                }
                if (item.Year2014 == null)
                {
                    dr["PreviousYear4"] = DBNull.Value;
                }
                else
                {
                    dr["PreviousYear4"] = item.Year2014;
                }
                dt.Rows.Add(dr);
            }

            SqlConnection con = new SqlConnection(connection);
            con.Open();
            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);



            //assign Destination table name  
            objbulk.DestinationTableName = "tblConcAllocationEligibility";

            objbulk.ColumnMappings.Add("AUN", "AUN");
            objbulk.ColumnMappings.Add("LEA", "LEA");
            objbulk.ColumnMappings.Add("PreviousYear1", "PreviousYear1");
            objbulk.ColumnMappings.Add("PreviousYear2", "PreviousYear2");
            objbulk.ColumnMappings.Add("PreviousYear3", "PreviousYear3");
            objbulk.ColumnMappings.Add("PreviousYear4", "PreviousYear4");

            //insert bulk Records into DataBase.  
            objbulk.WriteToServer(dt);
            con.Close();
        }
    }
}