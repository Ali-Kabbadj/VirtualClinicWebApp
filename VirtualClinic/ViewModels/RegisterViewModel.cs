using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualClinic.ViewModels
{
    public class RegisterViewModel
    {





        [DataType(DataType.Upload)]
        [Display(Name = "Upload Picture")]
        public IFormFile Image { get; set; }

        [Required]
        [Remote("UserNameExists", "Account", ErrorMessage = "Username already in use")]
        [Column(TypeName = "varchar(100)")]
        [PersonalData]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [PersonalData]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [PersonalData]
        public string LastName { get; set; }

        [Required]
        [PersonalData]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(20)")]
        [Remote("IdCardExists", "Account", ErrorMessage = "ID Card already in use")]
        public string IdCard { get; set; }

        [Required]
        [PersonalData]
        [Column(TypeName = "varchar(100)")]
        public string Country { get; set; }

        [Required]
        [PersonalData]
        [Column(TypeName = "varchar(100)")]
        public string City { get; set; }

        [Required]
        [PersonalData]
        [Column(TypeName = "varchar(100)")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please choose gender")]
        [PersonalData]
        public string Gender { get; set; }

        [Required]
        [EmailAddress]
        [Remote("EmailExists", "Account",ErrorMessage="Email already in use")]
        [Display(Name = "Email")]
        [PersonalData]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [PersonalData]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        [Remote("PhoneExists", "Account", ErrorMessage = "Phone Number already in use")]
        [PersonalData]
        public string PhoneNumber { get; set; }

        public bool IsDoctor { get; set; }
        [PersonalData]
        public string Adress { get; set; }        
        public long Price { get; set; }
        public string Specialist { get; set; }
    }
}
