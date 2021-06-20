using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualClinic.Models.Identity
{
    public class Patient : ApplicationUser
    {
        public ApplicationUser ToApplicationUser()
        {
            return new ApplicationUser
            {
                Id = Id,
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
                PhoneNumber = PhoneNumber
            };
        }
    }
}
