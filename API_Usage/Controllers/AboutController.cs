﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API_Usage.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}