using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Usage.Models
{
    public class Keystat
    {
        public int KeystatId { get; set; }
        //public string companyName { get; set; }
        public string marketcap { get; set; }
        public string beta { get; set; }
        public decimal week52high { get; set; }
        public decimal week52low { get; set; }
        public string week52change { get; set; }
        public string shortInterest { get; set; }
        public DateTime shortDate { get; set; }
        
        public decimal dividendRate { get; set; }
        public string dividendYield { get; set; }
        public string exDividendDate { get; set; }
        public decimal latestEPS { get; set; }
        public DateTime latestEPSDate { get; set; }
        public string sharesOutstanding { get; set; }
        //float is refered key word//
        public string srcfloat {get;set;}
        public string returnOnEquity { get; set; }
        public decimal consensusEPS { get; set; }
        public string numberOfEstimates { get; set; }
        public string symbol { get; set; }
        public string EBITDA { get; set; }
        public string revenue { get; set; }
        public string grossProfit { get; set; }
        public string cash { get; set; }
        public string debt { get; set; }
        public decimal ttmEPS { get; set; }
        public string revenuePerShare { get; set; }
        public string revenuePerEmployee { get; set; }
        public decimal peRatioHigh { get; set; }
        public decimal peRatioLow { get; set; }
        public string EPSSurpriseDollar { get; set; }
        public string EPSSurprisePercent { get; set; }
        public decimal returnOnAssets { get; set; }
        public string returnOnCapital { get; set; }
        public decimal profitMargin { get; set; }
        public string priceToSales { get; set; }
        public decimal priceToBook { get; set; }
        public string day200MovingAvg { get; set; }
        public string day50MovingAvg { get; set; }
        public decimal institutionPercent { get; set; }
        public string insiderPercent { get; set; }
        public string shortRatio { get; set; }
        public string year5ChangePercent { get; set; }
        public string year2ChangePercent { get; set; }
        public string year1ChangePercent { get; set; }
        public string ytdChangePercent { get; set; }
        public string month6ChangePercent { get; set; }
        public string month3ChangePercent { get; set; }
        public string month1ChangePercent { get; set; }
        public string day5ChangePercent { get; set; }
        
        public virtual Company Company { get; set; }
    }
}
