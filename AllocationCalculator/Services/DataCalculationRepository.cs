using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AllocationCalculator.Entity;
using AllocationCalculator.Models;

namespace AllocationCalculator.Services
{
    public class DataCalculationRepository
    {
        Title1AllocationEntities _db = new Title1AllocationEntities();
        public List<FinalDataModel> CalculateData(int year = 2018)
        {
            _db.BasicAllocation(year);
            _db.ConcAllocation(year);

            return (from a in _db.tblBasicAllocationSources
                    join b in _db.tblSchoolDistricts on a.AUN equals b.AUN
                    join c in _db.tblConcAllocationSources on a.AUN equals c.AUN
                    select new FinalDataModel
                    {
                        AUN = a.AUN,
                        AgencyName = b.AgencyName,
                        FinalBasicAllocatedAmount = a.FINALAllocationAmount,
                        FinalConcAllocatedAmount = c.FINALConcAllocationAmount
                    }).ToList();
        }
    }
}