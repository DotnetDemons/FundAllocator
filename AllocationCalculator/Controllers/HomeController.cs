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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AllocationCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataRepository repository;
        private readonly DataCalculationRepository dataCalculationRepository;
        public HomeController()
        {
            repository = new DataRepository();
            dataCalculationRepository = new DataCalculationRepository();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HomeModel model)
        {

            List<SchoolDistrictsModel> districtsModel = new List<SchoolDistrictsModel>();
            List<BasicAllocationSourcesModel> sourcesModel = new List<BasicAllocationSourcesModel>();
            List<MapCharterSchooltoSdsModel> schooltoSdsModel = new List<MapCharterSchooltoSdsModel>();
            List<BasicAllocationPreviousYearsDataModel> previousYearsDataModels = new List<BasicAllocationPreviousYearsDataModel>();
            List<CharterSchoolsModel> schoolsModel = new List<CharterSchoolsModel>();
            List<AUNMappingModel> mappingAUNModels = new List<AUNMappingModel>();
            if (Request.Files["FileUpload1"].ContentLength > 0)
            {
                DataTable dt = GetDataTable("FileUpload1");
                DataTable dt1 = GetDataTable("FileUpload1", 3);
                FillSchoolDistricts(ref districtsModel, ref sourcesModel, dt);
                FillBasicAllocation(ref sourcesModel, dt1, model.Year);
            }
            if (Request.Files["FileUpload2"].ContentLength > 0)
            {
                DataTable dt = GetDataTable("FileUpload2");
                FillMapping(ref schooltoSdsModel, dt);
            }
            if (Request.Files["FileUpload3"].ContentLength > 0)
            {
                DataTable dt = GetDataTable("FileUpload3");
                FillPreviousYearsData(ref previousYearsDataModels, ref sourcesModel, dt, model.Year);
            }
            if (Request.Files["FileUpload4"].ContentLength > 0)
            {
                DataTable dt = GetDataTable("FileUpload4");
                FillAUNMapping(ref mappingAUNModels, dt);
            }
            if (districtsModel.Count > 0)
            {
                MapSchoolDistrictsAUN(ref districtsModel, mappingAUNModels);
                repository.InsertSchoolDistricts(districtsModel);
            }
            if (sourcesModel.Count > 0)
            {
                MapBasicAllocationAUN(ref sourcesModel, mappingAUNModels);
                repository.InsertBasicAllocationSource(sourcesModel);
            }
            if (previousYearsDataModels.Count > 0) repository.InsertPreviousYearsData(previousYearsDataModels);
            if (schooltoSdsModel.Count > 0)
            {
                var charterSchools = schooltoSdsModel.Select(x => new CharterSchoolsModel { CSAUN = x.CSAUN, CharterSchoolName = x.CSAUNName })
                    .GroupBy(p => new { p.CSAUN, p.CharterSchoolName })
                    .Select(g => g.First()).ToList();
                repository.InsertCharterSchools(charterSchools);
                repository.InsertMappingData(schooltoSdsModel);
            }
            ViewBag.AlertMessage = "Data uploaded successfully, To download report";

            return View();
        }

        public ActionResult ExportData()
        {
            var gv = new GridView();
            gv.DataSource = dataCalculationRepository.CalculateData();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=FinalAllocation.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("Index");
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
                            districtModel.LEAID = int.Parse(dt.Rows[i][3].ToString());
                            districtModel.AgencyName = dt.Rows[i][4].ToString();
                            districtsModel.Add(districtModel);
                        }
                    }
                    BasicAllocationSourcesModel sourceModel = new BasicAllocationSourcesModel();
                    if (dt.Rows[i][3] != null && dt.Rows[i][10] != null)
                    {
                        if (dt.Rows[i][3].ToString() != "" && dt.Rows[i][10].ToString() != "" && dt.Rows[i][4].ToString().ToLower() != "undistributed")
                        {
                            sourceModel.LEAID = int.Parse(dt.Rows[i][3].ToString());
                            sourceModel.BasicAllocation = decimal.Parse(dt.Rows[i][10].ToString());
                            sourcesModels.Add(sourceModel);
                        }
                    }
                }
            }
        }
        private void FillBasicAllocation(ref List<BasicAllocationSourcesModel> sourcesModel, DataTable dt, int year)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 7)
                {
                    if (dt.Rows[i][3] != null && dt.Rows[i][3].ToString() != "")
                    {
                        int _leaid = int.Parse(dt.Rows[i][3].ToString());
                        for (int j = 0; j < sourcesModel.Count; j++)
                        {
                            if (sourcesModel[j].LEAID == _leaid)
                            {
                                sourcesModel[j].TotalForumlaCount = double.Parse(dt.Rows[i][10].ToString());
                                sourcesModel[j].POP517 = double.Parse(dt.Rows[i][11].ToString());
                                var percentageFormula = (double.Parse(dt.Rows[i][12].ToString().Trim('%')));
                                sourcesModel[j].PercentageFormula = percentageFormula;
                                sourcesModel[j].ProgramYear = year;
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
        private void FillPreviousYearsData(ref List<BasicAllocationPreviousYearsDataModel> previousYearsDataModels, ref List<BasicAllocationSourcesModel> sourcesModels, DataTable dt, int year)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 15)
                {
                    BasicAllocationPreviousYearsDataModel previousYearsDataModel = new BasicAllocationPreviousYearsDataModel();
                    if (dt.Rows[i][9] != null)
                    {
                        if (dt.Rows[i][9].ToString() != "" && dt.Rows[i][5].ToString() != "")
                        {
                            previousYearsDataModel.AUN = (dt.Rows[i][4] + "") == "" ? 999999999 : int.Parse(dt.Rows[i][4].ToString());
                            previousYearsDataModel.StateDeterminedFinalAllocation = decimal.Parse(dt.Rows[i][9].ToString().Trim('$'));
                            previousYearsDataModel.ProgramYear = (year - 1);
                            previousYearsDataModels.Add(previousYearsDataModel);
                        }
                    }
                    if (dt.Rows[i][5].ToString() != "" && dt.Rows[i][6].ToString() == "")
                    {
                        BasicAllocationSourcesModel sourcesModel = new BasicAllocationSourcesModel();
                        sourcesModel.AUN = int.Parse(dt.Rows[i][4].ToString());
                        sourcesModel.ProgramYear = year;
                        sourcesModels.Add(sourcesModel);
                    }
                }
            }
        }
        private void FillAUNMapping(ref List<AUNMappingModel> mappingAUNModels, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AUNMappingModel mappingModel = new AUNMappingModel();
                if (dt.Rows[i][0] != null && dt.Rows[i][1] != null)
                {
                    if (dt.Rows[i][0].ToString() != "" && dt.Rows[i][1].ToString() != "")
                    {
                        mappingModel.LEAID = int.Parse(dt.Rows[i][0].ToString());
                        mappingModel.AUN = int.Parse(dt.Rows[i][1].ToString());
                        mappingAUNModels.Add(mappingModel);
                    }
                }
            }
        }

        private void MapBasicAllocationAUN(ref List<BasicAllocationSourcesModel> sourcesModels, List<AUNMappingModel> mappingModels)
        {
            for (int i = 0; i < sourcesModels.Count; i++)
            {
                if (sourcesModels[i].BasicAllocation != null && sourcesModels[i].BasicAllocation != 0)
                {
                    var _leaid = sourcesModels[i].LEAID;
                    var _aun = mappingModels.Where(x => x.LEAID == _leaid).Select(x => x.AUN).FirstOrDefault();
                    sourcesModels[i].AUN = _aun == 0 ? 999999999 : _aun;
                }
            }
        }
        private void MapSchoolDistrictsAUN(ref List<SchoolDistrictsModel> districtsModels, List<AUNMappingModel> mappingModels)
        {
            for (int i = 0; i < districtsModels.Count; i++)
            {
                var _leaid = districtsModels[i].LEAID;
                var _aun = mappingModels.Where(x => x.LEAID == _leaid).Select(x => x.AUN).FirstOrDefault();
                districtsModels[i].AUN = _aun == 0 ? 999999999 : _aun;
            }
        }

    }
}