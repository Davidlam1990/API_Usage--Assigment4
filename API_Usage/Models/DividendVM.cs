using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Usage.Models
{
    public class DividendVM
    {
        public List<Company> Companies { get; set; }
        public Dividend Current { get; set; }
        public DividendVM(List<Company> companies, Dividend current)
        {
            Companies = companies;
            Current = current;
        }
    }
}
