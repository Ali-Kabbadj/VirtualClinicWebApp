using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using VirtualClinic.Models;

namespace VirtualClinic.ViewModels.Payment
{
    public class TaskPayment
    {
        public Task TaskToPay { get; set; }
        public string Token { get; set; }
        [PersonalData]
        [Required]
        public string Email { get; set; }
        public double Amount { get; set; }


    }
}
