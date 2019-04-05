using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Usage.Models
{
    public class Watchlist
    {
        [Key]
        public int WatchlistId { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
    }
}
