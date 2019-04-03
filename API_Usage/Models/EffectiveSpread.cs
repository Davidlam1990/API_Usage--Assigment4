using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Usage.Models
{
    public class EffectiveSpread
    {
        public int EffectiveSpreadId { get; set; }
        public string volume { get; set; }
        public string venue { get; set; }
        public string venueName { get; set; }
        public string effectiveSpread { get; set; }
        public string effectiveQuoted { get; set; }
        public string priceImprovement { get; set; }
        public string symbol { get; set; }
        public virtual Company Company { get; set; }
    }
}
