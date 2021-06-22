using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace VirtualClinic.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {

        public bool IsDoctor { get; set; } = false;
        [PersonalData]
        public Byte[] Image { get; set; }
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        [PersonalData]
        public string IdCard { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        [PersonalData]
        public string Adress { get; set; }
        public string Gender { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}
