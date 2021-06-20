using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using VirtualClinic.Models;
using VirtualClinic.Models.Identity;
using VirtualClinic.Services.Doctor;

namespace VirtualClinic.ViewModels
{
    public class DoctorViewModel:IDoctor
    {
        public string Id { get; set; }
        public byte[] Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdCard { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; } 
        public string Adress { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public List<Task> Appointements { get; set; }
        public long Price { get; set; }
        public string Speciality { get; set; }
        public bool IsDoctor { get; set; }
        public IFormFile IFormImage { get; set; }
        public bool IsActivated { get; set; } 
        public string Email { get; set; }



        public Doctor ToEntityDoctor()
        {
            return new Doctor
            {
                Id = Id,
                PhoneNumber = PhoneNumber,
                IsDoctor = IsDoctor,
                Image = Image,
                FirstName = FirstName,
                LastName = LastName,
                Birthday = BirthDate,
                IdCard = IdCard,
                Country = Country,
                State = State,
                City = City,
                Adress = Adress,
                Gender = Gender,
                Appointements = Appointements,
                Price = Price,
                Speciality = Speciality,
                IsActivated =IsActivated,
                Email =Email
            };
        }
    }
}
