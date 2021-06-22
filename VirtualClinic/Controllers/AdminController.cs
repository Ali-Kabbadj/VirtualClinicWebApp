using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VirtualClinic.Data;
using VirtualClinic.Models.Identity;
using VirtualClinic.Services.Doctor;
using VirtualClinic.Services.IdentityService;
using VirtualClinic.ViewModels;

namespace VirtualClinic.Controllers
{
    
    public class AdminController : Controller
    {
        private UserTaskService _userService;
        private ApplicationDbContext _context;
        private DoctorTaskService _doctorTaskService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = new UserTaskService(_context,_userManager,_signInManager);
            _doctorTaskService = new DoctorTaskService(_context);
        }


        
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User) && User.IsInRole("Administrator"))
            {
                AdminDashboardViewModel M = new AdminDashboardViewModel();
                M.NumberOfDoctors = _context.Doctors.Count();
                M.NumberOfPatients = _context.Patients.Count();
                M.NumberOfConfirmeedAppointments = _context.Tasks.Where(t => t.state == "Confirmed").Count();
                M.NumberOfUnConfirmeedAppointments = _context.Tasks.Where(t => t.state == "Unconfirmed").Count();
                return View(M);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult listOfDoctors()
        {
            var Doctors = _doctorTaskService.GetAllDoctors();
            return View(Doctors);
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult listOfPatients()
        {
            var Patients =  _userService.GetAll().Where(User => User.IsDoctor == false && User.FirstName != "Master");
            return View(Patients);
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ActivateDoctor(string email)
        {

            var doctor =await _context.Doctors.FindAsync( _userManager.FindByEmailAsync(email).Result.Id);
            if(doctor.IsActivated)
                doctor.IsActivated = false;
            else
                doctor.IsActivated = true;
            _context.AppUsers.Attach(doctor);
            _context.Entry(doctor).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("listOfDoctors");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel user)
        {
            if (_signInManager.IsSignedIn(User))
            {
                _signInManager.SignOutAsync();
            }
            var resulte = _userService.LoginUser(user ,user.RememberMe ,false).Result;
            if (resulte.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login");
        }
    }
}
