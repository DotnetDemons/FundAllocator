using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllocationCalculator.Models
{
    public class BasicAllocationSourcesModel
    {
        public Nullable<int> AUN { get; set; }
        public Nullable<int> ProgramYear { get; set; }
        public Nullable<decimal> BasicAllocation { get; set; }
        public Nullable<double> TotalForumlaCount { get; set; }
        public Nullable<double> POP517 { get; set; }
        public Nullable<double> PercentageFormula { get; set; }
        public Nullable<int> CharterSchoolAdjustments { get; set; }
        public Nullable<double> BAllocationAfterCS { get; set; }
        public Nullable<int> HoldHarmlessRate { get; set; }
        public Nullable<decimal> HoldHarmlessAmount { get; set; }
        public Nullable<decimal> HoldHarmlessCheck { get; set; }
        public Nullable<double> LEAsAboveHoldHarmless { get; set; }
        public Nullable<double> AdjustedLEAsAboveHoldHarmless { get; set; }
        public Nullable<double> AllocationstoAllLEAS { get; set; }
        public Nullable<double> FINALAllocationAmount { get; set; }
        public Nullable<decimal> SumBAllocationAfterCS { get; set; }
        public Nullable<decimal> sumAdjustedLEAsAboveHoldHarmless { get; set; }
        public Nullable<decimal> sumLEAsAboveHoldHarmless { get; set; }
        public int ID { get; set; }
        public Nullable<int> LEAID { get; set; }
    }
}