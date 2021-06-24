using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualClinic.Models.Identity
{
    public class Rating
    {
        public string Id { get; set; }
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        [ForeignKey("patient")]
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public byte[] PatientImage { get; set; }
        public double KindoRating { get; set; }
    }
}
