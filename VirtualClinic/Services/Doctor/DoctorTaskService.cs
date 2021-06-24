using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using VirtualClinic.Data;
using VirtualClinic.Models;
using VirtualClinic.Models.Identity;
using VirtualClinic.ViewModels;

namespace VirtualClinic.Services.Doctor
{
    public class DoctorTaskService : IDoctorTaskService<DoctorViewModel>
    {
        private readonly ApplicationDbContext db;
        public DoctorTaskService(ApplicationDbContext context)
        {
            db = context;
        }

        public virtual IQueryable<DoctorViewModel> GetAllDoctors()
        {            
                IQueryable<DoctorViewModel> result = db.Doctors.Select(Doctor => new DoctorViewModel
                {
                    Id = Doctor.Id,
                    IsDoctor = Doctor.IsDoctor,
                    Image = Doctor.Image,
                    FirstName = Doctor.FirstName,
                    LastName = Doctor.LastName,
                    BirthDate = Doctor.Birthday,
                    IdCard = Doctor.IdCard,
                    Country = Doctor.Country,
                    State = Doctor.State,
                    City = Doctor.City,
                    Adress = Doctor.Adress,
                    Gender = Doctor.Gender,
                    Speciality = Doctor.Speciality,
                    Price = Doctor.Price,
                    Appointements = db.Tasks.Where(a => a.DoctorId == Doctor.Id).ToList(),
                    IsActivated = Doctor.IsActivated,
                    Email =Doctor.Email,
                    Ratings = Doctor.Ratings

                });
                return result;          
        }

        public virtual IEnumerable<DoctorViewModel> GetDoctorByState(string state)
        {
            var result = GetAllDoctors().ToList().Where(t => (t.State == state));

            return result;
        }

        public virtual DoctorViewModel GetDoctorById(string Id)
        {
            var result = GetAllDoctors().Where(t => (t.Id == Id));

            return result.First();
        }


        public virtual IEnumerable<DoctorViewModel> GetRangebyState(string state)
        {
            var result = GetAllDoctors().ToList().Where(t => (t.State == state));

            return result;
        }
        

        public virtual void Update(DoctorViewModel doctor, ModelStateDictionary modelState)
        {            
                var entity = doctor.ToEntityDoctor();
                db.Doctors.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
        }

        public virtual void Delete(DoctorViewModel doctor, ModelStateDictionary modelState)
        {

            var entity = doctor.ToEntityDoctor();
            db.Doctors.Attach(entity);

            var recurrenceExceptions = db.Tasks.Where(t => t.DoctorId == doctor.Id);

            foreach (var recurrenceException in recurrenceExceptions)
            {
                db.Tasks.Remove(recurrenceException);
            }

            db.Doctors.Remove(entity);
            db.SaveChanges();
        }

        public DoctorViewModel One(Func<DoctorViewModel, bool> predicate)
        {
            return GetAllDoctors().FirstOrDefault(predicate);
        }

        public List<Rating> GetRatings(DoctorViewModel Doctor)
        {
            return GetAllDoctors().Where(d => d.Id == Doctor.Id).First().Ratings;
        }
    }
}
