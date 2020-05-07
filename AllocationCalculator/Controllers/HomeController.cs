using AllocationCalculator.Helpers;
using AllocationCalculator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AllocationCalculator.Services;

namespace AllocationCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataRepository repository;
        public HomeController()
        {
            repository = new DataRepository();
        }
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("Importexcel")]
        [HttpPost]
        public ActionResult Importexcel()
        {
            List<SchoolDistrictsModel> districtsModel = new List<SchoolDistrictsModel>();
            List<BasicAllocationSourcesModel> sourcesModel = new List<BasicAllocationSourcesModel>();
            List<MapCharterSchooltoSdsModel> schooltoSdsModel = new List<MapCharterSchooltoSdsModel>();
            List<BasicAllocationPreviousYearsDataModel> previousYearsDataModels = new List<BasicAllocationPreviousYearsDataModel>();
            List<CharterSchoolsModel> schoolsModel = new List<CharterSchoolsModel>();
            if (Request.Files["FileUpload1"].ContentLength > 0)
            {
                DataTable dt = GetDataTable("FileUpload1");
                DataTable dt1 = GetDataTable("FileUpload1", 3);
                FillSchoolDistricts(ref districtsModel, ref sourcesModel, dt);
                FillBasicAllocation(ref sourcesModel, dt1);
            }
            if (Request.Files["FileUpload2"].ContentLength > 0)
            {
                DataTable dt = GetDataTable("FileUpload2");
                FillMapping(ref schooltoSdsModel, dt);
            }
            if (Request.Files["FileUpload3"].ContentLength > 0)
            {
                DataTable dt = GetDataTable("FileUpload3");
                FillPreviousYearsData(ref previousYearsDataModels, dt);
            }
            if (districtsModel.Count > 0) repository.InsertSchoolDistricts(districtsModel);
            if (sourcesModel.Count > 0) repository.InsertBasicAllocationSource(sourcesModel);
            if (previousYearsDataModels.Count > 0) repository.InsertPreviousYearsData(previousYearsDataModels);
            if (schooltoSdsModel.Count > 0)
            {
                var charterSchools = schooltoSdsModel.Select(x => new CharterSchoolsModel { CSAUN = x.CSAUN, CharterSchoolName = x.CSAUNName })
                    .GroupBy(p => new { p.CSAUN, p.CharterSchoolName })
                    .Select(g => g.First()).ToList();
                repository.InsertCharterSchools(charterSchools);
                repository.InsertMappingData(schooltoSdsModel);
            }
            return RedirectToAction("Index");
        }


        private DataTable GetDataTable(string file, int sheet = 0)
        {
            string extension = System.IO.Path.GetExtension(Request.Files[file].FileName).ToLower();
            string connString = "";
            string[] validFileTypes = { ".xls", ".xlsx", ".csv" };
            string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files[file].FileName);
            if (!Directory.Exists(path1))
            {
                Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
            }
            if (validFileTypes.Contains(extension))
            {
                if (System.IO.File.Exists(path1))
                { System.IO.File.Delete(path1); }
                Request.Files[file].SaveAs(path1);
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                DataTable dt = Utility.ConvertXSLXtoDataTable(path1, connString, sheet);
                return dt;
            }
            else
            {
                return null;
            }
        }

        private void FillSchoolDistricts(ref List<SchoolDistrictsModel> districtsModel, ref List<BasicAllocationSourcesModel> sourcesModels, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 9)
                {
                    SchoolDistrictsModel districtModel = new SchoolDistrictsModel();
                    if (dt.Rows[i][3] != null && dt.Rows[i][4] != null)
                    {
                        if (dt.Rows[i][3].ToString() != "" && dt.Rows[i][4].ToString() != "")
                        {
                            districtModel.AUN = int.Parse(dt.Rows[i][3].ToString());
                            districtModel.AgencyName = dt.Rows[i][4].ToString();
                            districtsModel.Add(districtModel);
                        }
                    }
                    BasicAllocationSourcesModel sourceModel = new BasicAllocationSourcesModel();
                    if (dt.Rows[i][3] != null && dt.Rows[i][10] != null)
                    {
                        if (dt.Rows[i][3].ToString() != "" && dt.Rows[i][10].ToString() != "")
                        {
                            sourceModel.AUN = int.Parse(dt.Rows[i][3].ToString());
                            sourceModel.BasicAllocation = decimal.Parse(dt.Rows[i][10].ToString());
                            sourcesModels.Add(sourceModel);
                        }
                    }
                }
            }
        }
        private void FillBasicAllocation(ref List<BasicAllocationSourcesModel> sourcesModel, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 7)
                {
                    if (dt.Rows[i][3] != null && dt.Rows[i][3].ToString() != "")
                    {
                        int _aun = int.Parse(dt.Rows[i][3].ToString());
                        for (int j = 0; j < sourcesModel.Count; j++)
                        {
                            if (sourcesModel[j].AUN == _aun)
                            {
                                sourcesModel[j].TotalForumlaCount = double.Parse(dt.Rows[i][10].ToString());
                                sourcesModel[j].POP517 = double.Parse(dt.Rows[i][11].ToString());
                                sourcesModel[j].PercentageFormula = double.Parse(dt.Rows[i][12].ToString().Trim('%'));
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void FillMapping(ref List<MapCharterSchooltoSdsModel> mapCharters, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0] != null && dt.Rows[i][0].ToString() != "")
                {
                    MapCharterSchooltoSdsModel mapCharter = new MapCharterSchooltoSdsModel();
                    mapCharter.AUN = int.Parse(dt.Rows[i][2].ToString());
                    mapCharter.AgencyName = dt.Rows[i][3].ToString();
                    mapCharter.CSAUN = int.Parse(dt.Rows[i][0].ToString());
                    mapCharter.CSAUNName = dt.Rows[i][1].ToString();
                    mapCharter.NbrEnrolledStuds = double.Parse(dt.Rows[i][4].ToString());
                    mapCharters.Add(mapCharter);
                }

            }
        }
        private void FillPreviousYearsData(ref List<BasicAllocationPreviousYearsDataModel> previousYearsDataModels, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 15)
                {
                    BasicAllocationPreviousYearsDataModel previousYearsDataModel = new BasicAllocationPreviousYearsDataModel();
                    if (dt.Rows[i][4] != null)
                    {
                        if (dt.Rows[i][4].ToString() != "")
                        {
                            previousYearsDataModel.AUN = int.Parse(dt.Rows[i][4].ToString());
                            previousYearsDataModel.StateDeterminedFinalAllocation = decimal.Parse(dt.Rows[i][9].ToString().Trim('$'));
                            previousYearsDataModels.Add(previousYearsDataModel);
                        }
                    }
                }
            }
        }

    }
}