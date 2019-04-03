using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Usage.Models
{
    public class Trade
    {
        public int TradeId { get; set; }
        public decimal price { get; set; }
        public int size { get; set; }
        public string time { get; set; }
        public string timeLabel { get; set; }
        public string venue { get; set; }
        public string venueName { get; set; }
        public string symbol { get; set; }
        public virtual Company Company { get; set; }
    }
}
