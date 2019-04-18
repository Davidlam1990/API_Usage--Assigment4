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
    public class LogoController : Controller
    {
        public ApplicationDbContext dbContext;

        //Base URL for the IEXTrading API. Method specific URLs are appended to this base URL.
        string BASE_URL = "https://api.iextrading.com/1.0/";
        HttpClient httpClient;

        /// <summary>
        /// Initialize the database connection and HttpClient object
        /// </summary>
        /// <param name="context"></param>
        public LogoController(ApplicationDbContext context)
        {
            dbContext = context;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new
            System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        /*---------------------------------------------------------------------------------------------------------*/
        /*---------------------------------Logo API!!!-------------------------------------------------*/

        public IActionResult Logo(string symbol)
        {
            //Set ViewBag variable first
            ViewBag.dbSuccessChart = 0;
            Logo logos = new Logo();

            if (symbol != null)
            {
                logos = GetLogos(symbol);
            }

            LogoVM LogoViewModel = getLogoVM(logos);
            ViewData["Logo"] = LogoViewModel;

            return View(LogoViewModel);
        }

        public Logo GetLogos(string symbol)
        {
            // string to specify information to be retrieved from the API
            string IEXTrading_API_PATH = BASE_URL + "stock/" + symbol + "/logo";

            // initialize objects needed to gather data
            string logos = "";
            Logo Logos = new Logo();
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            // connect to the API and obtain the response
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // now, obtain the Json objects in the response as a string
            if (response.IsSuccessStatusCode)
            {
                logos = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // parse the string into appropriate objects
            if (!logos.Equals(""))
            {
                Logos = JsonConvert.DeserializeObject<Logo>(logos);

            }

            
            return Logos;
        }

        public LogoVM getLogoVM(Logo logos)
        {
            List<Company> companies = dbContext.Companies.ToList();
           
            Logo current = logos;
            return new LogoVM(companies, logos);
        }



        /*------------------------------------------------------------------------------------------------------------*/
        /*---------------------------------End of Logo!!!-------------------------------------------------*/

        /*---------------------------------------------------------------------------------------------------------*/
        /*---------------------------------Company Details API!!!-------------------------------------------------*/

        public IActionResult CompanyDetail(string symbol)
        {
            //Set ViewBag variable first
            ViewBag.dbSuccessChart = 0;
            CompanyDetail companydetails = new CompanyDetail();

            if (symbol != null)
            {
                companydetails = GetCompDetails(symbol);
            }

            CompanyDetailVM CDViewModel = getCompDetailVM(companydetails);
            ViewData["CompanyDetail"] = CDViewModel;

            return View(CDViewModel);
        }

        public CompanyDetail GetCompDetails(string symbol)
        {
            // string to specify information to be retrieved from the API
            string IEXTrading_API_PATH = BASE_URL + "stock/" + symbol + "/company";

            // initialize objects needed to gather data
            string companydetails = "";
            CompanyDetail CompanyDetails = new CompanyDetail();
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);

            // connect to the API and obtain the response
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // now, obtain the Json objects in the response as a string
            if (response.IsSuccessStatusCode)
            {
                companydetails = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // parse the string into appropriate objects
            if (!companydetails.Equals(""))
            {
                CompanyDetails = JsonConvert.DeserializeObject<CompanyDetail>(companydetails);
            }

            return CompanyDetails;
        }

        public CompanyDetailVM getCompDetailVM(CompanyDetail companydetails)
        {
            List<Company> companies = dbContext.Companies.ToList();
            
            CompanyDetail current = companydetails;

            return new CompanyDetailVM(companies, companydetails);
        }


       
        
    }

}