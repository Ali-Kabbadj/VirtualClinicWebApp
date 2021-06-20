using System;
using System.Collections.Generic;
using VirtualClinic.Models;

namespace VirtualClinic.Services.Doctor
{
    public interface IDoctor
    {
         string Id { get; set; }
         bool IsDoctor { get; set; } 
         byte[] Image { get; set; }
         string FirstName { get; set; }
         string LastName { get; set; }
         DateTime BirthDate { get; set; }
         string IdCard { get; set; }
         string Country { get; set; }
         string State { get; set; }
         string City { get; set; }
         string Adress { get; set; }
         string Gender { get; set; }
         //DateTime CreateDate { get; set; } 
         List<Task> Appointements { get; set; }
        long Price { get; set; }
         public string Speciality { get; set; }
         public bool IsActivated { get; set; }
        public string Email { get; set; }
        
    }
}
