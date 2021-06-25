
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using VirtualClinic.Models;
using VirtualClinic.Models.Identity;
using VirtualClinic.Services.IdentityService;

namespace VirtualClinic.ViewModels
{
    public class ApplicationUserViewModel : IUser
    {
        public string Id { get; set; }
        public bool IsDoctor { get; set; } = false;
        public Byte[] Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string IdCard { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public IFormFile  IFormImage { get; set; }
        public string Email { get; set; }
        //private readonly IWebHostEnvironment hostingEnv;
        public bool RememberMe { get; set; }
        public string PhoneNumber { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsActivated { get; set; }
        public List<Task> Appointements { get; set; }
        public string Speciality { get; set; }
        public long Price { get; set; }

        public List<Rating> Ratings { get; set; }
        public DateTime CreationDate { get; set; }

        public ApplicationUserViewModel()
        {
        }

        //public ApplicationUserViewModel(IWebHostEnvironment HostEnv)
        //{
        //    this.hostingEnv = HostEnv;
        //}

        public Models.Identity.ApplicationUser ToEnity()
        {
            return new Models.Identity.ApplicationUser
            {
                Id =Id,
                IsDoctor = IsDoctor,
                Image = Image,
                FirstName = FirstName,
                LastName = LastName,
                Birthday = Birthday,
                IdCard = IdCard,
                Country = Country,
                State = State,
                City = City,
                Adress = Adress,
                Gender = Gender,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Ratings = Ratings,
                CreateDate = CreationDate
            };
        }

       
    }
}
