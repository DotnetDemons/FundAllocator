using AllocationCalculator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AllocationCalculator.BusinessLogic
{
    public class AllocationRepository
    {
        public void FillSchoolDistricts(ref List<SchoolDistrictsModel> districtsModel, ref List<AllocationSourcesModel> sourcesModels, DataTable dt)
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
                    AllocationSourcesModel sourceModel = new AllocationSourcesModel();
                    if (dt.Rows[i][3] != null && dt.Rows[i][10] != null)
                    {
                        if (dt.Rows[i][3].ToString() != "" && dt.Rows[i][10].ToString() != "" && dt.Rows[i][4].ToString().ToLower() != "undistributed")
                        {
                            sourceModel.LEAID = int.Parse(dt.Rows[i][3].ToString());
                            sourceModel.BasicAllocation = decimal.Parse(dt.Rows[i][10].ToString());
                            sourceModel.ConcAllocation = decimal.Parse(dt.Rows[i][11].ToString());
                            sourcesModels.Add(sourceModel);
                        }
                    }
                }
            }
        }
        public void FillBasicAllocation(ref List<AllocationSourcesModel> sourcesModel, DataTable dt, int year)
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
                                var percentageFormula = (float.Parse(dt.Rows[i][12].ToString().Trim('%')));
                                sourcesModel[j].PercentageFormula = percentageFormula;
                                sourcesModel[j].ProgramYear = year;
                                break;
                            }
                        }
                    }
                }
            }
        }
        public void FillMapping(ref List<MapCharterSchooltoSdsModel> mapCharters, DataTable dt)
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
        public void FillPreviousYearsData(ref List<BasicAllocationPreviousYearsDataModel> previousYearsDataModels, ref List<AllocationSourcesModel> sourcesModels, DataTable dt, int year)
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
                        AllocationSourcesModel sourcesModel = new AllocationSourcesModel();
                        sourcesModel.AUN = int.Parse(dt.Rows[i][4].ToString());
                        sourcesModel.ProgramYear = year;
                        sourcesModels.Add(sourcesModel);
                    }
                }
            }
        }
        public void FillConcPreviousYearsData(ref List<ConcAllocationPreviousYearsDataModel> previousYearsDataModels, DataTable dt, int year)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 15)
                {
                    ConcAllocationPreviousYearsDataModel previousYearsDataModel = new ConcAllocationPreviousYearsDataModel();
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
                }
            }
        }
        public void FillAUNMapping(ref List<AUNMappingModel> mappingAUNModels, DataTable dt)
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
        public void MapBasicAllocationAUN(ref List<AllocationSourcesModel> sourcesModels, List<AUNMappingModel> mappingModels)
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
        public void MapSchoolDistrictsAUN(ref List<SchoolDistrictsModel> districtsModels, List<AUNMappingModel> mappingModels)
        {
            for (int i = 0; i < districtsModels.Count; i++)
            {
                var _leaid = districtsModels[i].LEAID;
                var _aun = mappingModels.Where(x => x.LEAID == _leaid).Select(x => x.AUN).FirstOrDefault();
                districtsModels[i].AUN = _aun == 0 ? 999999999 : _aun;
            }
        }
        public void FillConcEligibility(ref List<ConcAllocationEligibilityModel> eligibilityModels, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 5)
                {
                    ConcAllocationEligibilityModel eligibilityModel = new ConcAllocationEligibilityModel();
                    if (dt.Rows[i][0] != null)
                    {
                        if (dt.Rows[i][0].ToString() != "" && dt.Rows[i][1].ToString() != "")
                        {
                            eligibilityModel.AUN = int.Parse(dt.Rows[i][0].ToString());
                            eligibilityModel.Year2014 = float.Parse(dt.Rows[i][16].ToString().Trim('%'));
                            eligibilityModel.Year2015 = float.Parse(dt.Rows[i][13].ToString().Trim('%'));
                            eligibilityModel.Year2016 = float.Parse(dt.Rows[i][10].ToString().Trim('%'));
                            eligibilityModel.Year2017 = float.Parse(dt.Rows[i][7].ToString().Trim('%'));
                            eligibilityModels.Add(eligibilityModel);
                        }
                    }
                }
            }
        }
    }
}