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
    
    public partial class tblLEA
    {
        public int AUN { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsCharterSchool { get; set; }
    
        public virtual tblBasicAllocationSource tblBasicAllocationSource { get; set; }
        public virtual tblConcAllocationSource tblConcAllocationSource { get; set; }
        public virtual tblConcAllocationEligibility tblConcAllocationEligibility { get; set; }
        public virtual tblBasicAllocationPreviousYear tblBasicAllocationPreviousYear { get; set; }
        public virtual tblConcAllocationPreviousYear tblConcAllocationPreviousYear { get; set; }
    }
}
