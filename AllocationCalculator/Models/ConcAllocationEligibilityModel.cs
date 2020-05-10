using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllocationCalculator.Models
{
    public class ConcAllocationEligibilityModel
    {
        public Nullable<double> AUN { get; set; }
        public string C_LEA_ { get; set; }
        public Nullable<double> Year2017 { get; set; }
        public Nullable<double> Year2016 { get; set; }
        public Nullable<double> Year2015 { get; set; }
        public Nullable<double> Year2014 { get; set; }
        public int ID { get; set; }
    }
}