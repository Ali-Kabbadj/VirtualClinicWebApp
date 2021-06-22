using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualClinic.Models.Identity;
using VirtualClinic.ViewModels;

namespace VirtualClinic.Services.IdentityService
{
    public interface IUserTaskService<T> where T : class , IUser
    {
        IQueryable<ApplicationUser> GetAll();
        Task<T> GetUserById(string id);
        Task<T> GetUserByEmail(string email);
        Task<T> GetUserByUserName(string userName);
        Task<IdentityResult> CreateUser(RegisterViewModel User , string isDoctor,bool IsValid);
        Task<SignInResult> LoginUser(LoginViewModel login, bool RememberMe, bool lockoutOnFailure,bool IsValid);
        void AddUserRole(string userId, string RoleId);
        void SendConfirmationEmail(ApplicationUser user);
        //IQueryable<ApplicationUserViewModel> ModelsToViewModels(IQueryable<ApplicationUser> Users);
        //T ModelToViewModel(ApplicationUser User);

        //void SendConfirmationEmail(ApplicationUser user);

    }
}
