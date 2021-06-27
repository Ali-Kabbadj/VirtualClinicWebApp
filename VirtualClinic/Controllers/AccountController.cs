using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtualClinic.Data;
using VirtualClinic.ViewModels;
using System.Threading.Tasks;
using VirtualClinic.Models.Identity;
using VirtualClinic.Services.IdentityService;
using VirtualClinic.Services.EmailService; 
using System.Linq;



//Helooooo brahim
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
            //GetSpecialitiesAsync();
            if (!_signInManager.IsSignedIn(User))
            {

             ViewBag.IsDoctor = false;
            ViewData["Title"] = "Register As Patient";
            if (AsDoctor== "true")
            {
                ViewBag.IsDoctor = true;
                ViewData["Title"] = "Register As Doctor";
            }
            return View();
            }
            return RedirectToAction("LogOut");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserRegister(RegisterViewModel register,bool AsDoctor)
        {
            var result = await _userService.CreateUser(register, AsDoctor, ModelState.IsValid);
                if (result.Succeeded)
                {
                    register.PhoneNumber = register.DialNumber + register.PhoneNumber;
                    IQueryable<ApplicationUser> Users = _db.Users.Where(U => U.Email == register.Email);
                    if (Users.Count() > 0)
                    {
                        _userService.SendConfirmationEmail(Users.First());
                    }
                    return View("ConfirmEmailPage", register);
                }
                return View(register);
           
        }


        // Log IN 
        [HttpGet]
        public IActionResult Login()
        {
            if (!_signInManager.IsSignedIn(User))
            {
            
                ViewData["Title"] = "Log in";
                return View();
            }
            return RedirectToAction("LogOut");
        }


        public IActionResult ActivateAccount(LoginViewModel login)
        {
            return View(login);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
                var user = await _userManager.FindByEmailAsync(login.Email);
                if (user != null)
                {
                    IQueryable<Doctor> ds = _db.Doctors.Where(d => d.Email.ToLower() == login.Email.ToLower());
                    if (ds.Count()>0)
                    {
                        if (!ds.First().IsActivated)
                        {
                            return RedirectToAction("ActivateAccount", "Account",login);
                        }
                    }

                    IQueryable<ApplicationUser> ds1 = _db.Users.Where(d => d.Email.ToLower() == login.Email.ToLower());
                    if (!ds1.First().EmailConfirmed)
                    {
                        RegisterViewModel M = new RegisterViewModel
                        {
                            Email = login.Email,
                            FirstName = ds1.First().FirstName,
                            LastName = ds1.First().LastName
                        };
                        return RedirectToAction("ConfirmEmailPage", "Account", M);
                    }
                    var result = await _userService.LoginUser(login ,login.RememberMe,false,ModelState.IsValid);

                    if (result.Succeeded)
                    {
                        if (login.Email.ToLower() == "Master@Admin.com".ToLower())
                        {
                            await _signInManager.SignOutAsync();
                            return View();
                        }
                        return RedirectToAction("Index", "Home");
                    }
                  
                }
            ModelState.AddModelError(string.Empty, "Invalid Password.");
            return View(login);
        }
   
        public IActionResult ConfirmEmailPage(RegisterViewModel User)
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

        public JsonResult EmailDoesNotExist(string email)
        {
            //check if any of the email matches the email specified in the Parameter using the ANY extension method.  
            return Json(!_db.Users.Any(x => x.Email.ToLower() == email.ToLower()));
        }
        
         public JsonResult IdCardExists(string IdCard)
        {
            //check if any of the IdCard matches the IdCard specified in the Parameter using the ANY extension method.  
            return Json(!_db.Users.Any(x => x.IdCard.ToLower() == IdCard.ToLower()));
        }

        public JsonResult PhoneExists(string PhoneNumber)
        {
            //check if any of the phone matches the phone specified in the Parameter using the ANY extension method.  
            return Json(!_db.Users.Any(x => x.PhoneNumber == PhoneNumber));
        }

        public JsonResult UserNameExists(string UserName)
        {
            //check if any of the UserName matches the phone specified in the Parameter using the ANY extension method.  
            return Json(!_db.Users.Any(x => x.UserName.ToLower() == UserName.ToLower()));
        }

        public JsonResult EmailExist(string email)
        {
            //check if any of the email matches the email specified in the Parameter using the ANY extension method.  
            return Json(_db.Users.Any(x => x.Email.ToLower() == email.ToLower()));
        }

    }
}

