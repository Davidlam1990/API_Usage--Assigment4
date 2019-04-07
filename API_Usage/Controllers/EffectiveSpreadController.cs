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
    public class EffectiveSpreadController : Controller
    {
        public ApplicationDbContext dbContext;

        //Base URL for the IEXTrading API. Method specific URLs are appended to this base URL.
        string BASE_URL = "https://api.iextrading.com/1.0/";
        HttpClient httpClient;

        /// <summary>
        /// Initialize the database connection and HttpClient object
        /// </summary>
        /// <param name="context"></param>
        public EffectiveSpreadController(ApplicationDbContext context)
        {
            dbContext = context;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new
            System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        /*---------------------------------------------------------------------------------------------------------*/
        /*---------------------------------Effective Spread API!!!-------------------------------------------------*/

        public IActionResult EffectiveSpread(string symbol)
        {
            //Set ViewBag variable first
            ViewBag.dbSuccessChart = 0;
            List<EffectiveSpread> effectivespreads = new List<EffectiveSpread>();

            if (symbol != null)
            {
                effectivespreads = GetEffctiveSpreads(symbol);
            }

            EffectiveSpreadVM effectiveSpreadViewModel = getEffectiveSpreadVM(effectivespreads);

            return View(effectiveSpreadViewModel);
        }

        public List<EffectiveSpread> GetEffctiveSpreads(string symbol)
        {
            // string to specify information to be retrieved from the API
            string IEXTrading_API_PATH = BASE_URL + "stock/" + symbol + "/effective-spread";

            // initialize objects needed to gather data
            string effectivespreads = "";
            List<EffectiveSpread> EffectiveSpreads = new List<EffectiveSpread>();
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            // connect to the API and obtain the response
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // now, obtain the Json objects in the response as a string
            if (response.IsSuccessStatusCode)
            {
                effectivespreads = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // parse the string into appropriate objects
            if (!effectivespreads.Equals(""))
            {
                EffectiveSpreads = JsonConvert.DeserializeObject<List<EffectiveSpread>>(effectivespreads);
            }

            // fix the relations. By default the quotes do not have the company symbol
            //  this symbol serves as the foreign key in the database and connects the quote to the company
            foreach (EffectiveSpread Trade in EffectiveSpreads)
            {
                Trade.symbol = symbol;
            }

            return EffectiveSpreads;
        }

        public EffectiveSpreadVM getEffectiveSpreadVM(List<EffectiveSpread> effectivespreads)
        {
            List<Company> companies = dbContext.Companies.ToList();

            if (effectivespreads.Count == 0)
            {
                return new EffectiveSpreadVM(companies, null);
            }
            EffectiveSpread current = effectivespreads.Last();
            return new EffectiveSpreadVM(companies, effectivespreads.Last());
        }

        public IActionResult SaveEffectiveSpreads(string symbol)
        {
            List<EffectiveSpread> effectivespreads = GetEffctiveSpreads(symbol);

            foreach (EffectiveSpread effectivespread in effectivespreads)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                if (dbContext.EffectiveSpreads.Where(c => c.EffectiveSpreadId.Equals(effectivespread.EffectiveSpreadId)).Count() == 0)
                {
                    dbContext.EffectiveSpreads.Add(effectivespread);
                }
            }

            // persist the data
            dbContext.SaveChanges();

            // populate the models to render in the view
            ViewBag.dbSuccessChart = 1;
            EffectiveSpreadVM effectiveSpreadViewModel = getEffectiveSpreadVM(effectivespreads);
            return View("EffectiveSpread", effectiveSpreadViewModel);
        }

        /*------------------------------------------------------------------------------------------------------------*/
        /*---------------------------------End of Effective Spread!!!-------------------------------------------------*/
    }
}