using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualClinic.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using VirtualClinic.Data;
using VirtualClinic.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Task = VirtualClinic.Models.Task;

namespace VirtualClinic.Controllers.Scheduler
{
    
    public class DoctorSchedulerController : Controller
    {
        ApplicationDbContext _db;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        string ID;
        public  IActionResult DoctorScheduler(string Id)
        {
            ID = Id;
            return View();
        }

        private SchedulerTaskService taskService;
        [HttpGet]
        public async Task<string> GetCurrentUserId()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }

        public DoctorSchedulerController(SignInManager<ApplicationUser> SignInManager, ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _db = context;
            _userManager = userManager;
            _signInManager = SignInManager;
            this.taskService = new SchedulerTaskService(context,_userManager);
        }

        public virtual JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {            
            return Json(taskService.GetAll().Where(Task => (Task.DoctorId == GetCurrentUserId().Result && Task.PatientId==null) || (Task.DoctorId == GetCurrentUserId().Result && Task.state=="Confirmed")).ToDataSourceResult(request)); ;
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

            var Tasks = _db.Tasks.Where(T => T.DoctorId == task.DoctorId && T.TaskId == task.Identifier);

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
                    Identifier = originalTask.Identifier
                };
                Update(request, Model);
                return Json(new[] { Model }.ToDataSourceResult(request, ModelState));
            }
            else
            if (ModelState.IsValid)
            {
                task.DoctorId = GetCurrentUserId().Result;
                taskService.Insert(task, ModelState);
            }
            var normalreturn = taskService.GetAll().Where(Doctor => Doctor.DoctorId == GetCurrentUserId().Result).OrderBy(task => task.TaskID).Last();
           
            return Json(new[] { normalreturn }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Update([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                task.DoctorId = GetCurrentUserId().Result;
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

    

    }
}