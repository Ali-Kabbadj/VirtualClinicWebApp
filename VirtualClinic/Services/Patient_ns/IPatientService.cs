using System.Threading.Tasks;
using VirtualClinic.ViewModels.Patient_ns;

namespace VirtualClinic.Services.Patient_ns
{
    interface IPatientService
    {
        bool MedicalFile(MedicalFileViewModels medicalFile,bool isvalid,string idpatient);
        MedicalFileViewModels GetMedicalFile(string patietnId); 
    }
}
