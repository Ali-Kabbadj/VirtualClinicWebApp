using System.Threading.Tasks;
using VirtualClinic.ViewModels.Patient_ns;

namespace VirtualClinic.Services.Patient_ns
{
    interface IPatientService
    {
        Task<bool> MedicalFile(MedicalFileViewModels medicalFile,bool isvalid);
        MedicalFileViewModels GetMedicalFile(string patietnId); 
    }
}
