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
    public class DividendController : Controller
    {
        public ApplicationDbContext dbContext;

        //Base URL for the IEXTrading API. Method specific URLs are appended to this base URL.
        string BASE_URL = "https://api.iextrading.com/1.0/";
        HttpClient httpClient;

        /// <summary>
        /// Initialize the database connection and HttpClient object
        /// </summary>
        /// <param name="context"></param>
        public DividendController(ApplicationDbContext context)
        {
            dbContext = context;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new
            System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        /*----------------------------------------------------------------------------------------------------*/
        /*---------------------------------Largest Trades API!!!-------------------------------------------------*/

        public IActionResult Dividend(string symbol)
        {
            //Set ViewBag variable first
            ViewBag.dbSuccessChart = 0;
            List<Dividend> diviends = new List<Dividend>();

            if (symbol != null)
            {
                diviends = GetDividends(symbol);
                //equities = equities.OrderBy(c => c.date).ToList(); //Make sure the data is in ascending order of date.
            }

            DividendVM diviendViewModel = getDividendVM(diviends);

            return View(diviendViewModel);
        }

        public List<Dividend> GetDividends(string symbol)
        {
            // string to specify information to be retrieved from the API
            string IEXTrading_API_PATH = BASE_URL + "stock/" + symbol + "/dividends/5y";

            // initialize objects needed to gather data
            string dividends = "";
            List<Dividend> Dividends = new List<Dividend>();
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            // connect to the API and obtain the response
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // now, obtain the Json objects in the response as a string
            if (response.IsSuccessStatusCode)
            {
                dividends = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // parse the string into appropriate objects
            if (!dividends.Equals(""))
            {
                // https://stackoverflow.com/a/46280739
                //JObject result = JsonConvert.DeserializeObject<JObject>(companyList);
                Dividends = JsonConvert.DeserializeObject<List<Dividend>>(dividends);
                //trades = trades.GetRange(0, 50);
            }

            // fix the relations. By default the quotes do not have the company symbol
            //  this symbol serves as the foreign key in the database and connects the quote to the company
            foreach (Dividend Dividend in Dividends)
            {
                Dividend.symbol = symbol;
            }

            return Dividends;
        }

        public DividendVM getDividendVM(List<Dividend> dividends)
        {
            List<Company> companies = dbContext.Companies.ToList();

            if (dividends.Count == 0)
            {
                return new DividendVM(companies, null);
            }
            Dividend current = dividends.Last();
            return new DividendVM(companies, dividends.Last());
        }

        public IActionResult SaveDividends(string symbol)
        {
            List<Dividend> dividends = GetDividends(symbol);

            // save the quote if the quote has not already been saved in the database

            foreach (Dividend dividend in dividends)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                //So add company only if it doesnt exist, check existence using symbol (PK)
                if (dbContext.Dividends.Where(c => c.DividendId.Equals(dividend.DividendId)).Count() == 0)
                {
                    dbContext.Dividends.Add(dividend);
                }
            }

            // persist the data
            dbContext.SaveChanges();

            // populate the models to render in the view
            ViewBag.dbSuccessChart = 1;
            DividendVM diviendViewModel = getDividendVM(dividends);
            return View("Dividend", diviendViewModel);
        }
    }
}