using System;
using System.Collections.Generic;
using System.Linq;
using VirtualClinic.Models;

namespace VirtualClinic.ViewModels.Payment
{
    public class TaskPayment
    {
        public Task TaskToPay { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public double Amount { get; set; }


    }
}
