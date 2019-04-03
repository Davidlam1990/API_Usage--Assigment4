using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Usage.Models
{
    public class KeyStatVM
    {
        public List<Company> Companies { get; set; }
        public Keystat Current { get; set; }
        public KeyStatVM(List<Company> companies, Keystat current)
        {
            Companies = companies;
            Current = current;
        }
    }
}
