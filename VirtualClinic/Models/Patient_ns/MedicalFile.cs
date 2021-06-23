using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualClinic.Models.Patient_ns
{
    public class MedicalFile
    {
        [Key]
        public int Id { get; set; }
        public float Weight { get; set; }
        public float height { get; set; }
        public float temperature { get; set; }
        public int tension { get; set; }
        public string blood_type { get; set; }
        public string rhesus_factor { get; set; }
        public string health_history { get; set; }
        [ForeignKey("Patient")]
        public string patientId { get; set; }
    }
}
