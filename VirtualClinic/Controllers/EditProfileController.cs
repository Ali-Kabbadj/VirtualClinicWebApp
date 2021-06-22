using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VirtualClinic.ViewModels;
using VirtualClinic.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using VirtualClinic.Data;
using VirtualClinic.Models.Identity;

namespace VirtualClinic.Controllers
{
    public class EditProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _db;

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
        }
       

        // Profile 
        [HttpGet]
        public IActionResult Profiler(EditProfileViewModel editprofile, string id)
        {
            var profile = _db.Users.Find(id);

            if (profile == null)
            {
                return NotFound();
            }

            
            if (profile.IsDoctor)
            {
                var doctor = (Doctor)profile;
                editprofile = new EditProfileViewModel()
                {
                    
                    
                    Image = doctor.Image,
                    BirthDate = doctor.Birthday,
                    City = doctor.City,
                    Country = doctor.Country,
                    Email = doctor.Email,
                    FirstName = doctor.FirstName,
                    IdCard = doctor.IdCard,
                    LastName = doctor.LastName,
                    PhoneNumber = doctor.PhoneNumber,
                    Adress = doctor.Adress,
                    Specialist = doctor.Speciality,
                    Price = doctor.Price,
                    State = doctor.State
                    

                };
            }
            else
            {
                var patient = (Patient)profile;
                editprofile = new EditProfileViewModel()
                {

                    Image = patient.Image ,
                    BirthDate = patient.Birthday,
                    City = patient.City,
                    Country = patient.Country,
                    Email = patient.Email,
                    FirstName = patient.FirstName,
                    IdCard = patient.IdCard,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    State = patient.State
                };
            }

            editprofile.IsDoctor = profile.IsDoctor;
            return View(editprofile);
        }

        [HttpPost]
        public async Task<IActionResult> Profiler(EditProfileViewModel editprofile, string id, string i)
        {
            var Uploader = new Services.Upload.UploadFile(_environment);
            byte[] ImageFile = Uploader.Upload(editprofile.ImageName);
            var profile = _db.Users.Find(id);
            if (profile == null)
            {
                return NotFound();
            }
            if (editprofile.ImageName == null)
                ImageFile = profile.Image;
            if (profile.IsDoctor)
            {
                var doctor = (Doctor)profile;
                doctor.Image = ImageFile;
                doctor.Adress = editprofile.Adress;
                doctor.Birthday = editprofile.BirthDate;
                doctor.City = editprofile.City;
                doctor.Country = editprofile.Country;
                doctor.Email = editprofile.Email;
                doctor.FirstName = editprofile.FirstName;
                doctor.IdCard = editprofile.IdCard;
                doctor.LastName = editprofile.LastName;
                doctor.Price = editprofile.Price;
                doctor.PhoneNumber = editprofile.PhoneNumber;
                doctor.Speciality = editprofile.Specialist;
                doctor.State = editprofile.State;
            }
            else
            {
                var patient = (Patient)profile;
                patient.Adress = editprofile.Adress;
                patient.Image = ImageFile;
                patient.Birthday = editprofile.BirthDate;
                patient.City = editprofile.City;
                patient.Country = editprofile.Country;
                patient.Email = editprofile.Email;
                patient.FirstName = editprofile.FirstName;
                patient.IdCard = editprofile.IdCard;
                patient.LastName = editprofile.LastName;
                patient.PhoneNumber = editprofile.PhoneNumber;
                patient.Adress = editprofile.Adress;
            }
            editprofile.Image = ImageFile;
            await _userManager.UpdateAsync(profile);
            await _db.SaveChangesAsync();
            _db.SaveChanges();
            return Profiler(editprofile,_userManager.GetUserId(User) );
        }


        
    }
}
