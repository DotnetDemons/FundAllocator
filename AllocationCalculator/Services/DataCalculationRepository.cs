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

        public List<FinalDataModel> CalculateData(int year = 2018)
        {
            using (AllocationCalculationsEntities _dbBasic = new AllocationCalculationsEntities())
            {
                _dbBasic.BasicAllocation(year);
            }

            using (AllocationCalculationsEntities _dbConc = new AllocationCalculationsEntities())
            {
                _dbConc.ConcAllocation(year);
            }

            AllocationCalculationsEntities _db = new AllocationCalculationsEntities();
            return  (from a in _db.tblBasicAllocationSources
                        join b in _db.tblLEAs on a.AUN equals b.AUN
                        join c in _db.tblConcAllocationSources on a.AUN equals c.AUN
                        select new FinalDataModel
                        {
                            AUN = a.AUN,
                            AgencyName = b.Name,
                            FinalBasicAllocatedAmount = a.FinalBasicAllocationAmt,
                            FinalConcAllocatedAmount = c.FinalConcAllocationAmount
                        }).ToList();
        }
    }
}