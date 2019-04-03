using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Usage.Models
{
    public class EffectiveSpreadVM
    {
        public List<Company> Companies { get; set; }
        public EffectiveSpread Current { get; set; }
        public EffectiveSpreadVM(List<Company> companies, EffectiveSpread current)
        {
            Companies = companies;
            Current = current;
        }
    }
}
