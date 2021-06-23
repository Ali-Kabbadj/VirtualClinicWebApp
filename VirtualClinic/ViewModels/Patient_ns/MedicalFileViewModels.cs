using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualClinic.ViewModels.Patient_ns
{
    public class MedicalFileViewModels
    {
        [PersonalData]        
        public float Weight { get; set; }
        [PersonalData]
        public float height { get; set; }
        [PersonalData]
        public float temperature { get; set; }
        [PersonalData]
        public int tension { get; set; }
        [PersonalData]
        public string blood_type { get; set; }
        [PersonalData]
        public string rhesus_factor { get; set; }
        [PersonalData]
        public string health_history { get; set; }
        [PersonalData]
        public string patientId { get; set; }
    }
}
