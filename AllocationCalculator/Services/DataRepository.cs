using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AllocationCalculator.Entity;
using AllocationCalculator.Models;

namespace AllocationCalculator.Services
{
    public class DataRepository
    {
        AllocationCalculationsEntities _db = new AllocationCalculationsEntities();

        //public int InsertSchoolDistricts(List<SchoolDistrictsModel> districtsModels)
        //{
        //    var allRecords = (from a in _db.tblSchoolDistricts select a).ToList();
        //    _db.tblSchoolDistricts.RemoveRange(allRecords);

        //    var tblData = districtsModels.Select(x => new tblSchoolDistrict { AgencyName = x.AgencyName, AUN = x.AUN }).ToList();
        //    _db.tblSchoolDistricts.AddRange(tblData);

        //    return _db.SaveChanges();
        //}
        //public int InsertBasicAllocationSource(List<AllocationSourcesModel> sourcesModels)
        //{
        //    var allRecords = (from a in _db.tblBasicAllocationSources select a).ToList();
        //    _db.tblBasicAllocationSources.RemoveRange(allRecords);

        //    var tblData = sourcesModels.Select(x => new tblBasicAllocationSource
        //    {
        //        BasicAllocation = x.BasicAllocation,
        //        AUN = x.AUN,
        //        TotalForumlaCount = x.TotalForumlaCount,
        //        POP517 = x.POP517,
        //        PercentageFormula = x.PercentageFormula,
        //        ProgramYear = x.ProgramYear
        //    }).ToList();
        //    _db.tblBasicAllocationSources.AddRange(tblData);

        //    return _db.SaveChanges();
        //}
        //public int InsertMappingData(List<MapCharterSchooltoSdsModel> mapCharterModels)
        //{
        //    var allRecords = (from a in _db.tblMapCharterSchooltoSDs select a).ToList();
        //    _db.tblMapCharterSchooltoSDs.RemoveRange(allRecords);

        //    var tblData = mapCharterModels.Select(x => new tblMapCharterSchooltoSD
        //    {
        //        AgencyName = x.AgencyName,
        //        AUN = x.AUN,
        //        CSAUN = x.CSAUN,
        //        CSAUNName = x.CSAUNName,
        //        NbrEnrolledStuds = x.NbrEnrolledStuds
        //    }).ToList();
        //    _db.tblMapCharterSchooltoSDs.AddRange(tblData);

        //    return _db.SaveChanges();
        //}
        //public int InsertPreviousYearsData(List<BasicAllocationPreviousYearsDataModel> previousYearsDataModels)
        //{
        //    var allRecords = (from a in _db.tblBasicAllocationPreviousYearsDatas select a).ToList();
        //    _db.tblBasicAllocationPreviousYearsDatas.RemoveRange(allRecords);

        //    var tblData = previousYearsDataModels.Select(x => new tblBasicAllocationPreviousYearsData
        //    {
        //        StateDeterminedFinalAllocation = x.StateDeterminedFinalAllocation,
        //        AUN = x.AUN,
        //        ProgramYear = x.ProgramYear
        //    }).ToList();
        //    _db.tblBasicAllocationPreviousYearsDatas.AddRange(tblData);

        //    return _db.SaveChanges();
        //}
        //public int InsertCharterSchools(List<CharterSchoolsModel> schoolsModels)
        //{
        //    var allRecords = (from a in _db.tblCharterSchools select a).ToList();
        //    _db.tblCharterSchools.RemoveRange(allRecords);

        //    var tblData = schoolsModels.Select(x => new tblCharterSchool { CharterSchoolName = x.CharterSchoolName, CSAUN = x.CSAUN }).ToList();
        //    _db.tblCharterSchools.AddRange(tblData);

        //    return _db.SaveChanges();
        //}
        //public int InsertConcPreviousYearsData(List<ConcAllocationPreviousYearsDataModel> concPreviousYearsDataModels)
        //{
        //    var allRecords = (from a in _db.tblConcAllocationPreviousYearsDatas select a).ToList();
        //    _db.tblConcAllocationPreviousYearsDatas.RemoveRange(allRecords);

        //    var tblData = concPreviousYearsDataModels.Select(x => new tblConcAllocationPreviousYearsData
        //    {
        //        StateDeterminedFinalAllocation = x.StateDeterminedFinalAllocation,
        //        AUN = x.AUN,
        //        ProgramYear = x.ProgramYear
        //    }).ToList();
        //    _db.tblConcAllocationPreviousYearsDatas.AddRange(tblData);

        //    return _db.SaveChanges();
        //}
        //public int InsertConcEligibilityData(List<ConcAllocationEligibilityModel> eligibilityModels)
        //{
        //    var allRecords = (from a in _db.tblConcAllocationEligibilities select a).ToList();
        //    _db.tblConcAllocationEligibilities.RemoveRange(allRecords);

        //    var tblData = eligibilityModels.Select(x => new tblConcAllocationEligibility
        //    {
        //        AUN = x.AUN,
        //        Year2014 = x.Year2014,
        //        Year2015 = x.Year2015,
        //        Year2016 = x.Year2016,
        //        Year2017 = x.Year2017
        //    }).ToList();
        //    _db.tblConcAllocationEligibilities.AddRange(tblData);

        //    return _db.SaveChanges();
        //}
    }
}