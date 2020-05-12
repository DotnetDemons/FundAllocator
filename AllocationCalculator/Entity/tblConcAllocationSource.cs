//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AllocationCalculator.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblConcAllocationSource
    {
        public int AUN { get; set; }
        public Nullable<int> ProgramYear { get; set; }
        public Nullable<decimal> ConcAllocation { get; set; }
        public Nullable<double> TotalFormulaCount { get; set; }
        public Nullable<double> POP517 { get; set; }
        public Nullable<double> PercentageFormula { get; set; }
        public Nullable<int> CharterSchoolAdjustment { get; set; }
        public Nullable<double> ConcAllocationAfterCS { get; set; }
        public Nullable<int> HoldHarmlessRate { get; set; }
        public Nullable<decimal> HoldHarmlessAmount { get; set; }
        public Nullable<decimal> HoldHarmlessCheck { get; set; }
        public Nullable<double> LEAsAboveHoldHarmless { get; set; }
        public Nullable<double> AdjustedLEAAboveHoldHarmless { get; set; }
        public Nullable<double> AllocationstoAllLEA { get; set; }
        public Nullable<double> FinalConcAllocationAmount { get; set; }
        public Nullable<decimal> SumConcAllocationAfterCS { get; set; }
        public Nullable<decimal> SumAdjustedLEAsAboveHoldHarmless { get; set; }
        public Nullable<decimal> SumLEAAboveHoldHarmless { get; set; }
    
        public virtual tblLEA tblLEA { get; set; }
    }
}
