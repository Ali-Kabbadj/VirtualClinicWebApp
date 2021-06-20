using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using VirtualClinic.ViewModels;

namespace VirtualClinic.Services.Doctor
{
    public interface IDoctorTaskService<T> where T : class,IDoctor
    {
        IQueryable<T> GetAllDoctors();
        void Update(T Doctor, ModelStateDictionary modelState);
        void Delete(T Doctor, ModelStateDictionary modelState);
    }
}
