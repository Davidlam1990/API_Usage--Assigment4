using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Usage.Models
{
    public class Dividend
    {
        public int DividendId { get; set; }
        public string exDate { get; set; }
        public string paymentDate { get; set; }
        public string recordDate { get; set; }
        public string declaredDate { get; set; }
        public decimal amount { get; set; }
        public string flag { get; set; }
        public string type { get; set; }
        public string qualified { get; set; }
        public string indicated { get; set; }
        public string symbol { get; set; }
        public virtual Company Company { get; set; }
    }
}
