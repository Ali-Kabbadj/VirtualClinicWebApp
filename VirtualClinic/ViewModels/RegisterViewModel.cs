using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        [Column(TypeName = "varchar(100)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string LastName { get; set; }
        [Required]
        [PersonalData]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [PersonalData]
        [Column(TypeName = "varchar(20)")]
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
        public string Gender { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public bool IsDoctor { get; set; }
        public string Adress { get; set; }
        public long Price { get; set; }
        public string Specialist { get; set; }
    }
}
