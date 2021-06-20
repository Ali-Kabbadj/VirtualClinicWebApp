using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualClinic.Services.IdentityService
{
    public interface IUser
    {
        public string Id { get; set; }
        public bool IsDoctor { get; set; }
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
        public bool RememberMe { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActivated { get; set; }
        public string Speciality { get; set; }
        public long Price { get; set; }

    }
}
