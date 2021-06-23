using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using VirtualClinic.Models;
using VirtualClinic.Models.Identity;

namespace VirtualClinic.ViewModels
{
    public class PatientViewModel 
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
        public List<Task> Appointements { get; set; }
        public bool IsDoctor { get; set; } = false;
        public IFormFile IFormImage { get; set; }



        public Patient ToEntityPatient()
        {
            return new Patient
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
                Gender = Gender
            };
        }
    }
}
