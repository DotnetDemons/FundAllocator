using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllocationCalculator.Models
{
    public class MapCharterSchooltoSdsModel
    {
        public Nullable<int> CSAUN { get; set; }
        public string CSAUNName { get; set; }
        public Nullable<int> AUN { get; set; }
        public string AgencyName { get; set; }
        public Nullable<double> NbrEnrolledStuds { get; set; }
        public Nullable<double> LowIncomePercentage { get; set; }
        public Nullable<double> FormulaStudents { get; set; }
        public Nullable<decimal> BasicAllocationPerPupilAmt { get; set; }
        public Nullable<decimal> ConcAllocationPerPupilAmt { get; set; }
        public Nullable<decimal> TargetedAllocationPerPupilAmt { get; set; }
        public Nullable<decimal> EFIGAllocationPerPupilAmount { get; set; }
        public Nullable<decimal> TotalSubtracted { get; set; }
        public int CID { get; set; }
    }
}