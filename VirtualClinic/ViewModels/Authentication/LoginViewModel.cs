using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualClinic.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [PersonalData]
        [Remote("EmailExist", "Account", ErrorMessage = "No Account is Assigned to this Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [PersonalData]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
