using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AllocationCalculator.Entity;
using AllocationCalculator.Models;

namespace AllocationCalculator.Services
{
    public class DataRepository
    {
        Title1AllocationEntities _db = new Title1AllocationEntities();

        public int InsertSchoolDistricts(List<SchoolDistrictsModel> districtsModels)
        {
            var allRecords = (from a in _db.tblSchoolDistricts select a).ToList();
            _db.tblSchoolDistricts.RemoveRange(allRecords);
            
            foreach (var item in districtsModels)
            {
                tblSchoolDistrict tblData = new tblSchoolDistrict();
                tblData.AgencyName = item.AgencyName;
                tblData.AUN = item.AUN;
                _db.tblSchoolDistricts.Add(tblData);
            }
            return _db.SaveChanges();
        }
        public int InsertBasicAllocationSource(List<BasicAllocationSourcesModel> sourcesModels)
        {
            var allRecords = (from a in _db.tblBasicAllocationSources select a).ToList();
            _db.tblBasicAllocationSources.RemoveRange(allRecords);

            foreach (var item in sourcesModels)
            {
                tblBasicAllocationSource tblData = new tblBasicAllocationSource();
                tblData.BasicAllocation = item.BasicAllocation;
                tblData.AUN = item.AUN;
                tblData.TotalForumlaCount = item.TotalForumlaCount;
                tblData.POP517 = item.POP517;
                tblData.PercentageFormula = item.PercentageFormula;
                _db.tblBasicAllocationSources.Add(tblData);
            }
            return _db.SaveChanges();
        }
        public int InsertMappingData(List<MapCharterSchooltoSdsModel> mapCharterModels)
        {
            var allRecords = (from a in _db.tblMapCharterSchooltoSDs select a).ToList();
            _db.tblMapCharterSchooltoSDs.RemoveRange(allRecords);

            foreach (var item in mapCharterModels)
            {
                tblMapCharterSchooltoSD tblData = new tblMapCharterSchooltoSD();
                tblData.AgencyName = item.AgencyName;
                tblData.AUN = item.AUN;
                tblData.CSAUN = item.CSAUN;
                tblData.CSAUNName = item.CSAUNName;
                tblData.NbrEnrolledStuds = item.NbrEnrolledStuds;
                _db.tblMapCharterSchooltoSDs.Add(tblData);
            }
            return _db.SaveChanges();
        }
        public int InsertPreviousYearsData(List<BasicAllocationPreviousYearsDataModel> previousYearsDataModels)
        {
            var allRecords = (from a in _db.tblBasicAllocationPreviousYearsDatas select a).ToList();
            _db.tblBasicAllocationPreviousYearsDatas.RemoveRange(allRecords);

            foreach (var item in previousYearsDataModels)
            {
                tblBasicAllocationPreviousYearsData tblData = new tblBasicAllocationPreviousYearsData();
                tblData.StateDeterminedFinalAllocation = item.StateDeterminedFinalAllocation;
                tblData.AUN = item.AUN;
                _db.tblBasicAllocationPreviousYearsDatas.Add(tblData);
            }
            return _db.SaveChanges();
        }
        public int InsertCharterSchools(List<CharterSchoolsModel> schoolsModels)
        {
            var allRecords = (from a in _db.tblCharterSchools select a).ToList();
            _db.tblCharterSchools.RemoveRange(allRecords);

            foreach (var item in schoolsModels)
            {
                tblCharterSchool tblData = new tblCharterSchool();
                tblData.CharterSchoolName = item.CharterSchoolName;
                tblData.CSAUN = item.CSAUN;
                _db.tblCharterSchools.Add(tblData);
            }
            return _db.SaveChanges();
        }

    }
}