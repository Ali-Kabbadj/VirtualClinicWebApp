using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtualClinic.Models;
using VirtualClinic.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace VirtualClinic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/NotFound")]
        public IActionResult NotFound()
        {
            return View();
        }

     
    }
}
