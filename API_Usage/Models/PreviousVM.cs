using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Usage.Models
{
    public class PreviousVM
    {
        public List<Company> Companies { get; set; }
        public Previous Current { get; set; }
        public PreviousVM(List<Company> companies, Previous current)
        {
            Companies = companies;
            Current = current;
        }
    }
}
