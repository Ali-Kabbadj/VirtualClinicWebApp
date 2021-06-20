using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        [PersonalData]
        public byte[] Image { get; set; }
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public DateTime BirthDate { get; set; }
        [PersonalData]
        public string IdCard { get; set; }
        [PersonalData]
        public string Country { get; set; }
        [PersonalData]
        public string State { get; set; }
        [PersonalData]
        public string City { get; set; }
        [PersonalData]
        public string Adress { get; set; }
        [PersonalData]
        public string Gender { get; set; }
        [PersonalData]
        public string PhoneNumber { get; set; }
        [PersonalData]
        public List<Task> Appointements { get; set; }
        public long Price { get; set; }
        [PersonalData]
        public string Speciality { get; set; }
        public bool IsDoctor { get; set; } = true;
        public IFormFile IFormImage { get; set; }
        public bool IsActivated { get; set; }
        [PersonalData]
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
