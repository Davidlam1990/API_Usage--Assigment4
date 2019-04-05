using Microsoft.AspNetCore.Mvc;
using API_Usage.DataAccess;
using API_Usage.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;

namespace API_Usage.Controllers
{
    public class TradeController : Controller
    {
        public ApplicationDbContext dbContext;

        //Base URL for the IEXTrading API. Method specific URLs are appended to this base URL.
        string BASE_URL = "https://api.iextrading.com/1.0/";
        HttpClient httpClient;

        /// <summary>
        /// Initialize the database connection and HttpClient object
        /// </summary>
        /// <param name="context"></param>
        public TradeController(ApplicationDbContext context)
        {
            dbContext = context;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new
            System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        /*----------------------------------------------------------------------------------------------------*/
        /*---------------------------------Largest Trades API!!!-------------------------------------------------*/

        public IActionResult Trade(string symbol)
        {
            //Set ViewBag variable first
            ViewBag.dbSuccessChart = 0;
            List<Trade> trades = new List<Trade>();

            if (symbol != null)
            {
                trades = GetTrades(symbol);
                //equities = equities.OrderBy(c => c.date).ToList(); //Make sure the data is in ascending order of date.
            }

            TradeVM tradeViewModel = getTradeVM(trades);

            return View(tradeViewModel);
        }

        public List<Trade> GetTrades(string symbol)
        {
            // string to specify information to be retrieved from the API
            string IEXTrading_API_PATH = BASE_URL + "stock/" + symbol + "/largest-trades";

            // initialize objects needed to gather data
            string trades = "";
            List<Trade> Trades = new List<Trade>();
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            // connect to the API and obtain the response
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // now, obtain the Json objects in the response as a string
            if (response.IsSuccessStatusCode)
            {
                trades = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // parse the string into appropriate objects
            if (!trades.Equals(""))
            {
                // https://stackoverflow.com/a/46280739
                //JObject result = JsonConvert.DeserializeObject<JObject>(companyList);
                Trades = JsonConvert.DeserializeObject<List<Trade>>(trades);
                //trades = trades.GetRange(0, 50);
            }

            // fix the relations. By default the quotes do not have the company symbol
            //  this symbol serves as the foreign key in the database and connects the quote to the company
            foreach (Trade Trade in Trades)
            {
                Trade.symbol = symbol;
            }

            return Trades;
        }

        public TradeVM getTradeVM(List<Trade> trades)
        {
            List<Company> companies = dbContext.Companies.ToList();

            if (trades.Count == 0)
            {
                return new TradeVM(companies, null);
            }
            Trade current = trades.Last();
            return new TradeVM(companies, trades.Last());
        }

        public IActionResult SaveTrades(string symbol)
        {
            List<Trade> trades = GetTrades(symbol);

            // save the quote if the quote has not already been saved in the database

            foreach (Trade trade in trades)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                //So add company only if it doesnt exist, check existence using symbol (PK)
                if (dbContext.Trades.Where(c => c.TradeId.Equals(trade.TradeId)).Count() == 0)
                {
                    dbContext.Trades.Add(trade);
                }
            }

            // persist the data
            dbContext.SaveChanges();

            // populate the models to render in the view
            ViewBag.dbSuccessChart = 1;
            TradeVM tradeViewModel = getTradeVM(trades);
            return View("Trade", tradeViewModel);
        }

    }
}