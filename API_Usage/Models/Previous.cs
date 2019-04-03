using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Usage.Models
{
    public class Previous
    {
        public int PreviousId { get; set; }
        public string symbol { get; set; }
        public DateTime date { get; set; }
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal close { get; set; }
        public string volume { get; set; }
        public string unadjustedVolume { get; set; }
        public float change { get; set; }
        public float changePercent { get; set; }
        public float vwap { get; set; }
        public virtual Company Company { get; set; }
    }
}
