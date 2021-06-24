using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using VirtualClinic.Data;
using VirtualClinic.Models.Identity;
using VirtualClinic.Models.Patient_ns;
using VirtualClinic.ViewModels.Patient_ns;

namespace VirtualClinic.Services.Patient_ns
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _db;
        public PatientService(ApplicationDbContext context)
        {
            _db = context;
        }

        public MedicalFileViewModels GetMedicalFile(string patietnId)
        {
            var medicalfile = _db.MedicalFiles.Where(m => m.patientId == patietnId).First();
            return new MedicalFileViewModels
            {
                height=medicalfile.height,
                Weight=medicalfile.Weight,
                blood_type=medicalfile.blood_type,
                rhesus_factor=medicalfile.rhesus_factor,
                temperature=medicalfile.temperature,
                tension=medicalfile.tension,
                health_history=medicalfile.health_history
            };
        }

        public async Task<bool> MedicalFile(MedicalFileViewModels medicalFile,bool isvalid,string id)
        {
            if(isvalid)
            {
                var medicalfile = new MedicalFile {
                height = medicalFile.height,
                Weight = medicalFile.Weight,
                blood_type = medicalFile.blood_type,
                rhesus_factor = medicalFile.rhesus_factor,
                patientId=id,
                health_history=medicalFile.health_history,
                temperature=medicalFile.temperature,
                tension=medicalFile.tension 
                }; 
                await _db.MedicalFiles.AddAsync(medicalfile);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}