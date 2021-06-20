using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualClinic.Data;
using VirtualClinic.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using VirtualClinic.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using VirtualClinic.ViewModels;
using System.Threading;
using System.IO;
using Microsoft.Extensions.Logging;
using MailKit;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using VirtualClinic.Services.EmailService;
using IMailService = VirtualClinic.Services.EmailService.IMailService;

namespace VirtualClinic.Controllers
{
    [Authorize(Roles ="Patient")]
    public class AppointementController : Controller
    {





         private readonly ILogger<AppointementController> _logger;
        private readonly IMailService _mailService;
        private readonly IWebHostEnvironment _environment;



        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _SignrManager;
        private SchedulerTaskService taskService;
       
        string ID;
        public AppointementController(ILogger<AppointementController> logger, IWebHostEnvironment environment, IMailService mailService,ApplicationDbContext context , UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignrManager)
        {
            _db = context;
            _db = context;
            _userManager = userManager;
            this.taskService = new SchedulerTaskService(context,_userManager);
            _SignrManager = SignrManager;
            _environment = environment;
            _mailService = mailService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult MakeAppointement(string doctorid)
        {
            if(doctorid != null)
                ID = doctorid;

            var doctor = _db.Doctors.Find(doctorid);
            ViewBag.min = DateTime.Now;
            @ViewBag.Id = doctor.Id;
            //ViewData["Image"] = doctor.Image;
            ViewData["Name"] = doctor.FirstName + " " + doctor.LastName;
            var doctorViewModel = new DoctorViewModel()
            {
                Id = doctor.Id,
                PhoneNumber = doctor.PhoneNumber,
                IsDoctor = doctor.IsDoctor,
                Image = doctor.Image,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                BirthDate = doctor.Birthday,
                IdCard = doctor.IdCard,
                Country = doctor.Country,
                State = doctor.State,
                City = doctor.City,
                Adress = doctor.Adress,
                Gender = doctor.Gender,
                Appointements = doctor.Appointements,
                Price = doctor.Price,
                Speciality = doctor.Speciality
            };
            return View(doctorViewModel);
        }




       
        [HttpGet]
        public async Task<string> GetCurrentUserId()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }

       

        public virtual JsonResult Read([DataSourceRequest] DataSourceRequest request ,string Id)
        {
            var PatientId = GetCurrentUserId().Result;
            //return Json(taskService.GetRangebyid(GetCurrentUserId().Result).ToDataSourceResult(request));
         
            return Json(taskService.GetAll(PatientId).Where(M => M.DoctorId == Id).ToDataSourceResult(request)); 
        }

        public virtual JsonResult Destroy([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                taskService.Delete(task, ModelState);
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Create([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            var Tasks = _db.Tasks.Where(T => T.PatientId == task.PatientId && T.TaskId == task.Identifier);

            if (Tasks.Count() != 0)
            {
                var originalTask = Tasks.First();
                originalTask.Start = task.Start;
                originalTask.End = task.End;
                var Model = new TaskViewModel()
                {
                    TaskID = originalTask.TaskId,
                    Title = originalTask.Title,
                    Start = originalTask.Start,
                    StartTimezone = originalTask.StartTimezone,
                    End = originalTask.End,
                    EndTimezone = originalTask.EndTimezone,
                    Description = originalTask.Description,
                    RecurrenceRule = originalTask.RecurrenceRule,
                    RecurrenceException = originalTask.RecurrenceException,
                    RecurrenceID = originalTask.RecurrenceID,
                    IsAllDay = originalTask.IsAllDay,
                    PatientId = originalTask.PatientId,
                    DoctorId = originalTask.DoctorId,
                    Color = originalTask.Color,
                    Identifier = originalTask.Identifier,
                    state=originalTask.state
                };
                Update(request, Model);
                return Json(new[] { Model }.ToDataSourceResult(request, ModelState));
            }
            else
            if (ModelState.IsValid)
            {
                task.PatientId = GetCurrentUserId().Result;
                task.Color = "3";
                task.state = "Unconfirmed";
                task.DoctorName = _userManager.FindByIdAsync(task.DoctorId).Result.FirstName + " " + _userManager.FindByIdAsync(task.DoctorId).Result.LastName;
                task.Speciality = _db.Doctors.Find(task.DoctorId).Speciality;
                taskService.Insert(task, ModelState);
            }
            var normalreturn = taskService.GetAll().Where(Doctor => Doctor.DoctorId == task.DoctorId).OrderBy(task => task.TaskID).Last();
            //return Json(new[] { taskService.GetAll().Where(Doctor => Doctor.DoctorId == GetCurrentUserId().Result).Last() }.ToDataSourceResult(request, ModelState));
            return Json(new[] { normalreturn }.ToDataSourceResult(request, ModelState));




            //if (ModelState.IsValid)
            //{
            //    if (_userManager.FindByIdAsync(GetCurrentUserId().Result).Result.IsDoctor)
            //    {
            //        task.Color = "1";
            //    }
            //    else
            //    {
            //        task.Color = "3";
            //    }
            //    task.PatientId = GetCurrentUserId().Result;
            //    taskService.Insert(task, ModelState);
            //}

            //return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Update([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                taskService.Update(task, ModelState);
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            taskService.Dispose();
            base.Dispose(disposing);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        private readonly DateTime[] SpecialDays = new DateTime[] { new DateTime(2008, 6, 26), new DateTime(2008, 6, 27) };
















        [HttpPost]
        public IActionResult MakeAppointement(TaskViewModel appointement, string doctorid, string patientid)
        {
            if (doctorid == null || patientid == null)
                return BadRequest();
            var doctor = _db.Doctors.Find(doctorid);
            var patient = _db.Patients.Find(patientid);
            if (doctor == null || patient == null)
                return NotFound();
            appointement.DoctorId = doctorid;
            appointement.PatientId = patientid;
            appointement.Color = "#EA1111 ";
            if (ModelState.IsValid)
            {

                _db.Tasks.Add(appointement.ToEntity());
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(appointement);
        }



        //public static string CreateRandomWordNumberCombination()
        //{
        //    Random rnd = new Random();
        //    //Dictionary of strings
        //    string[] words = { "Bold", "Think", "Friend", "Pony", "Fall", "Easy", "implosion", "propitious", "irksome", "debacle", "assail", "undermine" };
        //    //Random number from - to
        //    int randomNumber = rnd.Next(2000, 3000);
        //    //Create combination of word + number
        //    string randomString = $"{words[rnd.Next(0, words.Length)]}{randomNumber}";

        //    return randomString;

        //}

        public async System.Threading.Tasks.Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var currentTime = DateTime.UtcNow;
                var tasks = _db.Tasks.ToList();
                //run background task every second
                if (currentTime.Second == DateTime.Now.Second)
                {
                    foreach (var item in tasks)
                    {
                        if(item.Start.Hour - DateTime.Now.Hour == 1 && item.Start.Year == DateTime.Now.Year && item.Start.Month==DateTime.Now.Month)
                        {
                            _logger.LogInformation(currentTime.ToString());
                            SendReminderEmail(await _userManager.FindByIdAsync(item.PatientId));
                        }
                        
                    }
                    
                }

 

            }
        }

 

        public void SendReminderEmail(ApplicationUser user)
        {
            string FilePath = _environment.WebRootFileProvider.GetFileInfo("Templates/HtmlMailConfirmationPageModel.html")?.PhysicalPath; ;
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            string Body = "Don't Forget Your Appoinetement We Wait for you";
            MailText = MailText.Replace("[username]", user.UserName).Replace("[email]", user.Email).Replace("[link]", Body);
            MailRequest mailRequest = new MailRequest();
            mailRequest.ToEmail = user.Email;
            mailRequest.UserName = user.UserName;
            mailRequest.Subject = "Appointement Reminder";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            mailRequest.Body = builder.ToMessageBody();
            _mailService.SendEmailAsync(mailRequest);
        }
    }
}
