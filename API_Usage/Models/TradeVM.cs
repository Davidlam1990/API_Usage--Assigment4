using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Usage.Models
{
    public class TradeVM
    {
        public List<Company> Companies { get; set; }
        public Trade Current { get; set; }
        public TradeVM(List<Company> companies, Trade current)
        {
            Companies = companies;
            Current = current;
        }
    }
}
