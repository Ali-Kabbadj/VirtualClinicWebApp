using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VirtualClinic.ViewModels;
using VirtualClinic.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using VirtualClinic.Data;
using VirtualClinic.Models.Identity;
using VirtualClinic.ViewModels.Patient_ns;
using VirtualClinic.Services.Patient_ns;
using Microsoft.AspNetCore.Authorization;
using VirtualClinic.Services.IdentityService;

namespace VirtualClinic.Controllers
{
    public class EditProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _db;
        private readonly IPatientService _patentService;
        private readonly IUserTaskService<ApplicationUserViewModel> _userTaskService;
        public EditProfileController(
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                ILogger<AccountController> logger,
                                IWebHostEnvironment environment,
                                ApplicationDbContext context)
        {
            _db = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _environment = environment;
            _patentService = new PatientService(_db);
            _userTaskService = new UserTaskService(context, userManager, signInManager);
        }      

        // Profile 
        [HttpGet]
        public IActionResult Profiler()
        {
            var id = _userManager.GetUserAsync(User).Result.Id;
            var editprofile = _userTaskService.GetProfile(id);
            if(editprofile != null)
                return View(editprofile);
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfiler(EditProfileViewModel editprofile)
        {
            var id = _userManager.GetUserAsync(User).Result.Id;
            var isedit = await _userTaskService.EditProfile(editprofile, id);
            if (isedit)
            {
                editprofile = _userTaskService.GetProfile(id);
                return View("Profiler", editprofile);
            }

                
            return NotFound();
        }

        [HttpGet]
        [Authorize(Roles ="Patient")]
        public IActionResult MedicalFile()
        {
            var idpatient = _userManager.GetUserAsync(User).Result.Id;
            var medicalfile = _patentService.GetMedicalFile(idpatient);
            if(medicalfile != null)
                return View(medicalfile);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MedicalFile(MedicalFileViewModels medicalFile)
        {
            var idpatient = _userManager.GetUserAsync(User).Result.Id;
            var isadded = await _patentService.MedicalFile(medicalFile, ModelState.IsValid,idpatient);
            if(!isadded)
            {
                ModelState.AddModelError("Error", "Something won't wrong");   
            }
            return View(medicalFile);
        }

    }
}
