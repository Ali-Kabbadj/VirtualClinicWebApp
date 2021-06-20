using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtualClinic.Data;
using VirtualClinic.ViewModels;
using System;
using System.Threading.Tasks;
using VirtualClinic.Models.Identity;
using VirtualClinic.Services.IdentityService;
using VirtualClinic.Services.EmailService;
using System.Linq;

namespace VirtualClinic.Controllers
{
     
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _db;
        private readonly UserTaskService _userService;
        private readonly IMailService _mailService;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                ILogger<AccountController> logger,
                                IWebHostEnvironment environment,
                                ApplicationDbContext context,
                                IMailService mailService)
                                                         {
                _db = context;
                _userManager = userManager;
                _signInManager = signInManager;
                _logger = logger;
                _environment = environment;
                _mailService = mailService;
                 _userService = new UserTaskService(_mailService, _db, _environment, _userManager, _signInManager);
                                                                
                                                          }


        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Title"] = "Register";
            return View();
        }


        [HttpGet]
        public IActionResult UserRegister(string AsDoctor)
        {
            ViewBag.IsDoctor = false;
            ViewData["Title"] = "Register As Patient";
            if (AsDoctor == "true")
            {
                ViewBag.IsDoctor = true;
                ViewData["Title"] = "Register As Doctor";
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserRegister(RegisterViewModel register,string AsDoctor)
        {
            if (ModelState.IsValid)
            {
               var result = await _userService.CreateUser(register,AsDoctor);
               if (result.Succeeded)
               {
                    return View("ConfirmEmailPage", register);
               }
            }
           
            return View(register);
        }

        // Log IN 
        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Title"] = "Log in";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
                if (user != null)
                {
                    var result = await _userService.LoginUser(login ,login.RememberMe,false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("pass", "Invalid Password.");
                }
                ModelState.AddModelError("email", "Invalid Email.");
            }
            
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(login);
                        
        }
   
        public IActionResult ConfirmEmailPage(ApplicationUserViewModel User)
        {
            return View(User);
        }

        // LogOut
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string Email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(Email);
            ViewBag.Email = Email;
            if (user != null)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home", new { ConfirmedEmail = user.Email });
            }
            else
            {
                return NotFound();
            }
        }

        public JsonResult EmailExists(string email)
        {
            //check if any of the email matches the email specified in the Parameter using the ANY extension method.  
            return Json(!_db.Users.Any(x => x.Email == email));
        }
        
         public JsonResult IdCardExists(string IdCard)
        {
            //check if any of the IdCard matches the IdCard specified in the Parameter using the ANY extension method.  
            return Json(!_db.Users.Any(x => x.IdCard == IdCard));
        }

        public JsonResult PhoneExists(string PhoneNumber)
        {
            //check if any of the phone matches the phone specified in the Parameter using the ANY extension method.  
            return Json(!_db.Users.Any(x => x.PhoneNumber == PhoneNumber));
        }
       
    }


}

