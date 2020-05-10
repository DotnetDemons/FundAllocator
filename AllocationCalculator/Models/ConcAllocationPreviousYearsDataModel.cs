using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllocationCalculator.Models
{
    public class ConcAllocationPreviousYearsDataModel
    {
        public Nullable<int> AUN { get; set; }
        public Nullable<int> ProgramYear { get; set; }
        public Nullable<decimal> StateDeterminedFinalAllocation { get; set; }
        public int ID { get; set; }
    }
}