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
using System.Collections.Generic;

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
            doctor.Ratings.AddRange(_db.Ratings.Where(d => d.DoctorId == id));
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


        [HttpPost]
        public IActionResult Rating(string x , string y)
        {
            if (_db.Ratings.Where(R => R.PatientId == _userManager.GetUserAsync(User).Result.Id && R.DoctorId == y).Count()>0)
            {
               Rating R = _db.Ratings.Where(R => R.PatientId == _userManager.GetUserAsync(User).Result.Id && R.DoctorId == y).First();
                R.KindoRating = double.Parse(x);
                _db.Ratings.Attach(R);
                _db.Entry(R).State = EntityState.Modified;
                _db.SaveChanges();
            }
            else
            {
                Random ran=new Random();
                Rating R = new Rating()
                {
                    Id = ran.Next(1, 999999999).ToString(),
                    DoctorId = y,
                    PatientId = _userManager.GetUserAsync(User).Result.Id,
                    PatientName = _userManager.GetUserAsync(User).Result.UserName,
                    PatientImage = _userManager.GetUserAsync(User).Result.Image,
                    KindoRating = double.Parse(x)
                    
                };
                _db.Ratings.Add(R);
                _db.SaveChanges();
            }

            return RedirectToAction("Details", "DoctorsProfiles", y);
        }

    }
}
