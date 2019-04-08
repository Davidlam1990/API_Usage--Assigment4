using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API_Usage.Models;
using System.Net.Http;
using Newtonsoft.Json;
using API_Usage.DataAccess;


namespace API_Usage.Controllers
{
    public class HighlightsController : Controller
    {
        public ApplicationDbContext dbContext;

        string BASE_URL = "https://api.iextrading.com/1.0/";
        HttpClient httpClient;

        /// <summary>
        /// Initialize the database connection and HttpClient object
        /// </summary>
        /// <param name="context"></param>
        /// 
        public HighlightsController(ApplicationDbContext context)
        {
            dbContext = context;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new
                System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }


        // Action Methods for pulling latest top 10 News
        public IActionResult News()
        {
            //Set ViewBag variable first
            ViewBag.dbSucessComp = 0;
            List<News> NewsDet = GetNews();

            //Save companies in TempData, so they do not have to be retrieved again
            TempData["NewsDetails"] = JsonConvert.SerializeObject(NewsDet);

            return View(NewsDet);
        }

        public List<News> GetNews()
        {
            // string to specify information to be retrieved from the API
            string IEXTrading_API_PATH = BASE_URL + "/stock/market/news/last/10";

            // initialize objects needed to gather data
            string Newsdetails = "";
            List<News> NetDet = null;
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            // connect to the API and obtain the response
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // now, obtain the Json objects in the response as a string
            if (response.IsSuccessStatusCode)
            {
                Newsdetails = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // parse the string into appropriate objects
            if (!Newsdetails.Equals(""))
            {
                NetDet = JsonConvert.DeserializeObject<List<News>>(Newsdetails);
            }

            return NetDet;
        }

        public IActionResult PopulateNews()
        {
            // retrieve the companies that were saved in the symbols method
            // saving in TempData is extremely inefficient - the data circles back from the browser
            // better methods would be to serialize to the hard disk, or save directly into the database
            //  in the symbols method. This example has been structured to demonstrate one way to save object data
            //  and retrieve it later
            List<News> NewsDetails = JsonConvert.DeserializeObject<List<News>>(TempData["NewsDetails"].ToString());

            foreach (News news in NewsDetails)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                //So add company only if it doesnt exist, check existence using symbol (PK)
                if (dbContext.News.Where(c => c.NewsID.Equals(news.NewsID)).Count() == 0)
                {
                    dbContext.News.Add(news);
                }
            }

            dbContext.SaveChanges();
            ViewBag.dbSuccessComp = 1;
            return View("News", NewsDetails);
        }
        // **** For fetching data for top gainers****
        public IActionResult TopGainers()
        {
            //Set ViewBag variable first
            ViewBag.dbSucessComp = 0;
            List<TopGainer> TopG = GetTopGainers();

            //Save Top Gainers in TempData, so they do not have to be retrieved again
            TempData["TGDetails"] = JsonConvert.SerializeObject(TopG);

            return View(TopG);
        }

        public List<TopGainer> GetTopGainers()
        {
            // string to specify information to be retrieved from the API
            string IEXTrading_API_PATH = BASE_URL + "/stock/market/list/gainers";

            // initialize objects needed to gather data
            string TGDetails = "";
            List<TopGainer> TopG = null;
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            // connect to the API and obtain the response
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // now, obtain the Json objects in the response as a string
            if (response.IsSuccessStatusCode)
            {
                TGDetails = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // parse the string into appropriate objects
            if (!TGDetails.Equals(""))
            {
                TopG = JsonConvert.DeserializeObject<List<TopGainer>>(TGDetails);

            }

            return TopG;
        }

        public IActionResult PopulateTopGainers()
        {
            // retrieve the companies that were saved in the symbols method
            // saving in TempData is extremely inefficient - the data circles back from the browser
            // better methods would be to serialize to the hard disk, or save directly into the database
            //  in the symbols method. This example has been structured to demonstrate one way to save object data
            //  and retrieve it later
            List<TopGainer> topGainers = JsonConvert.DeserializeObject<List<TopGainer>>(TempData["TGDetails"].ToString());


            foreach (TopGainer topGainer in topGainers)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                //So add company only if it doesnt exist, check existence using symbol (PK)
                if (dbContext.TopGainers.Where(c => c.symbol.Equals(topGainer.symbol)).Count() == 0)
                {
                    dbContext.TopGainers.Add(topGainer);
                }
            }

            dbContext.SaveChanges();
            ViewBag.dbSuccessComp = 1;
            return View("TopGainers", topGainers);
        }

        // **** For fetching data for top losers****
        public IActionResult TopLosers()
        {
            //Set ViewBag variable first
            ViewBag.dbSucessComp = 0;
            List<TopLoser> TopL = GetTopLosers();

            //Save Top Losers in TempData, so they do not have to be retrieved again
            TempData["TLDetails"] = JsonConvert.SerializeObject(TopL);

            return View(TopL);
        }

        public List<TopLoser> GetTopLosers()
        {
            // string to specify information to be retrieved from the API
            string IEXTrading_API_PATH = BASE_URL + "/stock/market/list/losers";

            // initialize objects needed to gather data
            string TLDetails = "";
            List<TopLoser> TopL = null;
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            // connect to the API and obtain the response
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // now, obtain the Json objects in the response as a string
            if (response.IsSuccessStatusCode)
            {
                TLDetails = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // parse the string into appropriate objects
            if (!TLDetails.Equals(""))
            {
                TopL = JsonConvert.DeserializeObject<List<TopLoser>>(TLDetails);

            }



            return TopL;
        }

        public IActionResult PopulateTopLosers()
        {
            // retrieve the companies that were saved in the symbols method
            // saving in TempData is extremely inefficient - the data circles back from the browser
            // better methods would be to serialize to the hard disk, or save directly into the database
            //  in the symbols method. This example has been structured to demonstrate one way to save object data
            //  and retrieve it later
            List<TopLoser> topLosers = JsonConvert.DeserializeObject<List<TopLoser>>(TempData["TLDetails"].ToString());


            foreach (TopLoser topLoser in topLosers)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                //So add company only if it doesnt exist, check existence using symbol (PK)
                if (dbContext.TopLosers.Where(c => c.symbol.Equals(topLoser.symbol)).Count() == 0)
                {
                    dbContext.TopLosers.Add(topLoser);
                }
            }

            dbContext.SaveChanges();
            ViewBag.dbSuccessComp = 1;
            return View("TopLosers", topLosers);
        }
        // **** For fetching data for Most actives****
        public IActionResult MostActive()
        {
            //Set ViewBag variable first
            ViewBag.dbSucessComp = 0;
            List<MostActive> MA = GetMostActive();

            //Save Most Active in TempData, so they do not have to be retrieved again
            TempData["MADetails"] = JsonConvert.SerializeObject(MA);

            return View(MA);
        }

        public List<MostActive> GetMostActive()
        {
            // string to specify information to be retrieved from the API
            string IEXTrading_API_PATH = BASE_URL + "/stock/market/list/mostactive";

            // initialize objects needed to gather data
            string MADetails = "";
            List<MostActive> MA = null;
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            // connect to the API and obtain the response
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // now, obtain the Json objects in the response as a string
            if (response.IsSuccessStatusCode)
            {
                MADetails = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // parse the string into appropriate objects
            if (!MADetails.Equals(""))
            {
                MA = JsonConvert.DeserializeObject<List<MostActive>>(MADetails);

            }

            return MA;
        }
        public IActionResult PopulateMostActive()
        {
            // retrieve the companies that were saved in the symbols method
            // saving in TempData is extremely inefficient - the data circles back from the browser
            // better methods would be to serialize to the hard disk, or save directly into the database
            //  in the symbols method. This example has been structured to demonstrate one way to save object data
            //  and retrieve it later
            List<MostActive> mostActive = JsonConvert.DeserializeObject<List<MostActive>>(TempData["MADetails"].ToString());


            foreach (MostActive mostactive in mostActive)
            {
                //Database will give PK constraint violation error when trying to insert record with existing PK.
                //So add company only if it doesnt exist, check existence using symbol (PK)
                if (dbContext.MostActives.Where(c => c.symbol.Equals(mostactive.symbol)).Count() == 0)
                {
                    dbContext.MostActives.Add(mostactive);
                }
            }

            dbContext.SaveChanges();
            ViewBag.dbSuccessComp = 1;
            return View("MostActive", mostActive);
        }
    }
}
