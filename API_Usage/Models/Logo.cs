using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Usage.Models
{
    public class Logo

    {
        public int LogoID { get; set; }
        public string URL { get; set; }
        public string symbol { get; set; }
        public virtual Company Company { get; set; }
    }

    public class CompanyDetail

    {
        [Key]
        public string symbol { get; set; }
        public string companyName { get; set; }
        public string exchange { get; set; }
        public string industry { get; set; }
        public string website { get; set; }
        public string description { get; set; }
        public string CEO { get; set; }
        public string issueType { get; set; }
        public string sector { get; set; }
        public virtual Company Company { get; set; }

    }
}
