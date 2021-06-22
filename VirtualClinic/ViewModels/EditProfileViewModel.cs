using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VirtualClinic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualClinic.ViewModels
{
    public class EditProfileViewModel
    {
        public bool IsDoctor { get; set; }
        public IFormFile ImageName { get; set; }
        [DataType(DataType.Upload)]
        public Byte[] Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdCard { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        [Required]
        [PersonalData]
        [Column(TypeName = "varchar(100)")]
        public string Adress { get; set; }
        public long Price { get; set; }
        public string Specialist { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [PersonalData]
        [Column(TypeName = "varchar(100)")]
        public string State { get; set; }

       
    }
}
