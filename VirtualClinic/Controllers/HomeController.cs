using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtualClinic.Models;
using VirtualClinic.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using VirtualClinic.Models.Identity;

namespace VirtualClinic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
      
        public HomeController(ILogger<HomeController> logger,UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager)
        { 
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Index()
        {

            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "LogedOutHome");
            }
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
