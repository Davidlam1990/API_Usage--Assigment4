using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Usage.Models
{
    public class LogoVM
    {
        public List<Company> Companies { get; set; }
        public Logo Current { get; set; }
        public LogoVM(List<Company> companies, Logo current)
        {
            Companies = companies;
            Current = current;
        }
    }
    public class CompanyDetailVM
    {
        public List<Company> Companies { get; set; }
        public CompanyDetail CurrentCD { get; set; }
        public CompanyDetailVM(List<Company> companies, CompanyDetail current)
        {
            Companies = companies;
            CurrentCD = current;


        }
    }
   



}
