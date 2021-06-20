using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualClinic.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using VirtualClinic.Services.Doctor;
using VirtualClinic.Services.IdentityService;
using Microsoft.AspNetCore.Identity;
using VirtualClinic.Models.Identity;

namespace VirtualClinic.Controllers
{    
    public class DoctorsProfilesController : Controller
    {
        private readonly DoctorTaskService DoctorService;
        private readonly ApplicationDbContext _db;
        private UserTaskService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public DoctorsProfilesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _db = context;
            this.DoctorService = new DoctorTaskService(context);
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = new UserTaskService(_db, _userManager, _signInManager);
        }
        [HttpGet]
        public IActionResult Index()
        {
            var doctors =  DoctorService.GetAllDoctors().Where(User => User.IsDoctor== true); 
            return View(doctors);
        }
        [HttpGet]
        public IActionResult filter(string Searcchspeciality)
        {
            if (String.IsNullOrEmpty(Searcchspeciality))
                return BadRequest();
            
            var doctors = DoctorService.GetAllDoctors().Where(d => d.Speciality.Contains(Searcchspeciality));
            return View( doctors);
        }
        [HttpGet]
        public IActionResult Details(string id)
        {
            if (id == null)
                return BadRequest();
            var doctor = DoctorService.GetDoctorById(id);
            if (doctor == null)
                return NotFound();
            ViewBag.Totalappointement = _db.Tasks.Where(t => t.DoctorId.Equals(id) && t.PatientId!=null).Count();
            return View(doctor);
        }
        [Authorize(Roles = "Doctor")]
        public IActionResult Profile(string id)
        {
            if (id == null)
                return BadRequest();
            var doctor = _db.Doctors.Find(id);
            if (doctor == null)
                return NotFound();
            //var appointements = _db.Doctors.Where(user => user.Id.Equals(id));
            return View(doctor);
        }
    }
}
