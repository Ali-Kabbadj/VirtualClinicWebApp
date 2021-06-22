using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualClinic.Data;
using VirtualClinic.Models.Identity;

namespace VirtualClinic.Models
{
    public class SchedulerTaskService : ISchedulerEventService<TaskViewModel>
    {
        
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        public SchedulerTaskService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            db = context;
            _userManager = userManager;
        }


        public virtual IEnumerable<TaskViewModel> GetRange(DateTime start, DateTime end)
        {
            var result = GetAll().ToList().Where(t => (t.RecurrenceRule != null || (t.Start >= start && t.Start <= end) || (t.End >= start && t.End <= end)));

            return result;
        }
        public virtual IEnumerable<TaskViewModel> GetRangebyid(string doctorid)
        {
            var result = GetAll().ToList().Where(t => (t.DoctorId == doctorid ));

            return result;
        }
        public virtual IQueryable<TaskViewModel> GetAll()
        {

            IQueryable<TaskViewModel> result = db.Tasks.Select(task => new TaskViewModel
            {
                TaskID = task.TaskId,
                Title = task.Title,
                Start = DateTime.SpecifyKind(task.Start, DateTimeKind.Utc),
                End = DateTime.SpecifyKind(task.End, DateTimeKind.Utc),
                StartTimezone = task.StartTimezone,
                EndTimezone = task.EndTimezone,
                Description = task.Description,
                IsAllDay = task.IsAllDay,
                RecurrenceRule = task.RecurrenceRule,
                RecurrenceException = task.RecurrenceException,
                RecurrenceID = task.RecurrenceID,
                DoctorId = task.DoctorId,
                PatientId = task.PatientId,
                Color = task.Color,
                Identifier = task.TaskId,
                state = task.state,
                DoctorName = task.DoctorName,
                Speciality = task.Speciality

            }) ;
            return result;
        }

        public virtual IQueryable<TaskViewModel> GetAll(string idPatient)
        {

            IQueryable<TaskViewModel> result = db.Tasks.Select(task => new TaskViewModel
            {
                TaskID = task.TaskId,
                Title = task.Title,
                Start = DateTime.SpecifyKind(task.Start, DateTimeKind.Utc),
                End = DateTime.SpecifyKind(task.End, DateTimeKind.Utc),
                StartTimezone = task.StartTimezone,
                EndTimezone = task.EndTimezone,
                Description = task.Description,
                IsAllDay = task.IsAllDay,
                RecurrenceRule = task.RecurrenceRule,
                RecurrenceException = task.RecurrenceException,
                RecurrenceID = task.RecurrenceID,
                DoctorId = task.DoctorId,
                PatientId = task.PatientId,
                Color = task.Color,
                Identifier = task.TaskId,
                state = task.state,
                DoctorName = task.DoctorName,
                Speciality =task.Speciality
            }) ;
            return result;
        }

        public virtual void Insert(TaskViewModel task, ModelStateDictionary modelState)
        {
            if (ValidateModel(task, modelState))
            {

                if (string.IsNullOrEmpty(task.Title))
                {
                    task.Title = "";
                }
                task.Identifier = task.TaskID;

                task.Amount = db.Doctors.Where(t => t.Id == task.DoctorId).First().Price;
                var entity = task.ToEntity();
                db.Tasks.Add(entity);
                db.SaveChanges();

            }
        }

        public virtual void Update(TaskViewModel task, ModelStateDictionary modelState)
        {
            if (ValidateModel(task, modelState))
            {

                if (string.IsNullOrEmpty(task.Title))
                {
                    task.Title = "";
                }
                task.Identifier = task.TaskID;
                var entity = task.ToEntity();
                db.Tasks.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public virtual void Delete(TaskViewModel task, ModelStateDictionary modelState)
        {

            var entity = task.ToEntity();
            db.Tasks.Attach(entity);

            var recurrenceExceptions = db.Tasks.Where(t => t.RecurrenceID == task.TaskID);

            foreach (var recurrenceException in recurrenceExceptions)
            {
                db.Tasks.Remove(recurrenceException);
            }

            db.Tasks.Remove(entity);
            db.SaveChanges();
        }

        private bool ValidateModel(TaskViewModel appointment, ModelStateDictionary modelState)
        {
            if (appointment.Start > appointment.End)
            {
                modelState.AddModelError("errors", "End date must be greater or equal to Start date.");
                return false;
            }

            return true;
        }

        public TaskViewModel One(Func<TaskViewModel, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            db.Dispose();
        }



        public virtual void Cancel(Task entity)
        {


            var recurrenceExceptions = db.Tasks.Where(t => t.RecurrenceID == entity.TaskId);

            foreach (var recurrenceException in recurrenceExceptions)
            {
                db.Tasks.Remove(recurrenceException);
            }

            db.Tasks.Remove(entity);
            db.SaveChanges();
        }

    }
}
