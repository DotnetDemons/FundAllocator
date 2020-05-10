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
using AllocationCalculator.BusinessLogic;

namespace AllocationCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataRepository repository;
        private readonly DataCalculationRepository dataCalculationRepository;
        private readonly AllocationRepository basicAllocationRepository;
        private readonly ConcDataRepository concDataRepository;
        public HomeController()
        {
            repository = new DataRepository();
            concDataRepository = new ConcDataRepository();
            dataCalculationRepository = new DataCalculationRepository();
            basicAllocationRepository = new AllocationRepository();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HomeModel model)
        {

            List<SchoolDistrictsModel> districtsModel = new List<SchoolDistrictsModel>();
            List<AllocationSourcesModel> sourcesModel = new List<AllocationSourcesModel>();
            List<MapCharterSchooltoSdsModel> schooltoSdsModel = new List<MapCharterSchooltoSdsModel>();
            List<BasicAllocationPreviousYearsDataModel> previousYearsDataModels = new List<BasicAllocationPreviousYearsDataModel>();
            List<ConcAllocationPreviousYearsDataModel> concPreviousYearsDataModels = new List<ConcAllocationPreviousYearsDataModel>();
            List<CharterSchoolsModel> schoolsModel = new List<CharterSchoolsModel>();
            List<AUNMappingModel> mappingAUNModels = new List<AUNMappingModel>();
            List<ConcAllocationEligibilityModel> eligibilityModels = new List<ConcAllocationEligibilityModel>();

            if (Request.Files["FileUpload1"].ContentLength > 0)
            {
                DataTable dt = GetDataTable("FileUpload1");
                DataTable dt1 = GetDataTable("FileUpload1", 3);
                basicAllocationRepository.FillSchoolDistricts(ref districtsModel, ref sourcesModel, dt);
                basicAllocationRepository.FillBasicAllocation(ref sourcesModel, dt1, model.Year);
            }
            if (Request.Files["FileUpload2"].ContentLength > 0)
            {
                DataTable dt = GetDataTable("FileUpload2");
                basicAllocationRepository.FillMapping(ref schooltoSdsModel, dt);
            }
            if (Request.Files["FileUpload3"].ContentLength > 0)
            {
                DataTable dt = GetDataTable("FileUpload3");
                basicAllocationRepository.FillPreviousYearsData(ref previousYearsDataModels, ref sourcesModel, dt, model.Year);
            }
            if (Request.Files["FileUpload4"].ContentLength > 0)
            {
                DataTable dt = GetDataTable("FileUpload4");
                basicAllocationRepository.FillAUNMapping(ref mappingAUNModels, dt);
            }
            if (Request.Files["FileUpload5"].ContentLength > 0)
            {
                DataTable dt = GetDataTable("FileUpload5");
                basicAllocationRepository.FillConcPreviousYearsData(ref concPreviousYearsDataModels , dt, model.Year);
            }
            if (Request.Files["FileUpload6"].ContentLength > 0)
            {
                DataTable dt = GetDataTable("FileUpload6");
                basicAllocationRepository.FillConcEligibility(ref eligibilityModels, dt);
            }


            if (districtsModel.Count > 0)
            {
                basicAllocationRepository.MapSchoolDistrictsAUN(ref districtsModel, mappingAUNModels);
                repository.InsertSchoolDistricts(districtsModel);
            }
            if (sourcesModel.Count > 0)
            {
                basicAllocationRepository.MapBasicAllocationAUN(ref sourcesModel, mappingAUNModels);
                repository.InsertBasicAllocationSource(sourcesModel);
                concDataRepository.InsertConcAllocationSource(sourcesModel);
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

 

    }
}