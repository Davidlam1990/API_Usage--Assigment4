using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Usage.Models
{
    public class TopGainer
    {
        [Key]
        public string symbol { get; set; }
        public string companyName { get; set; }
        public string primaryExchange { get; set; }
        public string sector { get; set; }
        public float open { get; set; }
        public string openTime { get; set; }
        public float close { get; set; }
        public string closeTime { get; set; }
        public float high { get; set; }
        public float low { get; set; }
        public float latestPrice { get; set; }
        public string latestSource { get; set; }
        public string latestTime { get; set; }
        public string latestUpdate { get; set; }
        public Int64 latestVolume { get; set; }
        public float delayedPrice { get; set; }
        public string delayedPriceTime { get; set; }
        public float previousClose { get; set; }
        public float change { get; set; }
        public float changePercent { get; set; }
        public Int64 avgTotalVolume { get; set; }
        public Int64 marketCap { get; set; }
        public float week52High { get; set; }
        public float week52Low { get; set; }
        public float ytdChange { get; set; }
    }

    public class TopLoser
    {
        [Key]
        public string symbol { get; set; }
        public string companyName { get; set; }
        public string primaryExchange { get; set; }
        public string sector { get; set; }
        public float open { get; set; }
        public string openTime { get; set; }
        public float close { get; set; }
        public string closeTime { get; set; }
        public float high { get; set; }
        public float low { get; set; }
        public float latestPrice { get; set; }
        public string latestSource { get; set; }
        public string latestTime { get; set; }
        public string latestUpdate { get; set; }
        public Int64 latestVolume { get; set; }
        public float delayedPrice { get; set; }
        public string delayedPriceTime { get; set; }
        public float previousClose { get; set; }
        public float change { get; set; }
        public float changePercent { get; set; }
        public Int64 avgTotalVolume { get; set; }
        public Int64 marketCap { get; set; }
        public float week52High { get; set; }
        public float week52Low { get; set; }
        public float ytdChange { get; set; }
    }

    public class MostActive
    {
        [Key]
        public int ID { get; set; }
        public string symbol { get; set; }
        public string companyName { get; set; }
        public string primaryExchange { get; set; }
        public string sector { get; set; }
        public float open { get; set; }
        public string openTime { get; set; }
        public float close { get; set; }
        public string closeTime { get; set; }
        public float high { get; set; }
        public float low { get; set; }
        public float latestPrice { get; set; }
        public string latestSource { get; set; }
        public string latestTime { get; set; }
        public string latestUpdate { get; set; }
        public Int64 latestVolume { get; set; }
        public float delayedPrice { get; set; }
        public string delayedPriceTime { get; set; }
        public float previousClose { get; set; }
        public float change { get; set; }
        public float changePercent { get; set; }
        public Int64 avgTotalVolume { get; set; }
        public Int64 marketCap { get; set; }
        public float week52High { get; set; }
        public float week52Low { get; set; }
        public float ytdChange { get; set; }
    }

    public class News

    {
        [Key]
        public int NewsID { get; set; }
        public string datetime { get; set; }
        public string headline { get; set; }
        public string source { get; set; }
        public string url { get; set; }
        public string summary { get; set; }
        public string related { get; set; }
        public string image { get; set; }
    }


}
