using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using VirtualClinic.Data;
using VirtualClinic.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using VirtualClinic.Services.IdentityService;
using VirtualClinic.Services.EmailService;
using System.IO;
using MimeKit;
using Microsoft.AspNetCore.Hosting;
using VirtualClinic.Services.Upload;
using VirtualClinic.Models.Identity;
using System.Linq;

namespace VirtualClinic.Services.IdentityService
{
    public class UserTaskService : IUserTaskService<ApplicationUserViewModel>
    {


        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _environment;
        private readonly IMailService _mailService;




        public UserTaskService(IMailService mailService, ApplicationDbContext context, IWebHostEnvironment environment
                , UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _db = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _mailService = mailService;
            _environment = environment;
        }



        public UserTaskService(ApplicationDbContext context, UserManager<ApplicationUser> userManager
                , SignInManager<ApplicationUser> signInManager)
        {
            _db = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }





        // this Method create a user(patient or doctor) and add it to database
        public async Task<IdentityResult> CreateUser(RegisterViewModel register, bool isdoctor,bool IsValid)
        {
            IdentityResult result =new IdentityResult();
            if (IsValid)
            {
                var Upload = new UploadFile(_environment);
                ApplicationUser user = new ApplicationUser();
                byte[] Image = null;
                if (register.Image != null)
                {
                    // Upload its an action on Upload Service that that convert file .png /... to Array of byte
                    Image = Upload.Upload(register.Image);
                }
                else
                {
                    // here we take image from wwwroot as default if the user thasn't upload any image.
                    var path = _environment.WebRootFileProvider.GetFileInfo("Images/Img/NoImage.jpg")?.PhysicalPath;
                    Image = File.ReadAllBytes(path);
                }
                string roleid = "";
                if (isdoctor)
                {
                    user = new Models.Identity.Doctor { IsDoctor = true,Image =Image,FirstName =register.FirstName.ToUpper(),LastName =register.LastName.ToUpper(),UserName = register.UserName,Birthday =register.Birthday,Email=register.Email,PhoneNumber=register.PhoneNumber,IdCard =register.IdCard , Country= register.Country.ToUpper(),State =register.State.ToUpper(),City = register.City.ToUpper(),Adress = register.Adress,Gender = register.Gender,Speciality = register.Specialist ,Price = register.Price , IsActivated = false};
                    roleid = "2301D884-221A-4E7D-B509-0113DCC044E2";
                }
                else
                {
                    user = new Patient { IsDoctor = false, Image = Image, FirstName = register.FirstName.ToUpper(), LastName = register.LastName.ToUpper(), UserName = register.UserName, Birthday = register.Birthday, Email = register.Email, PhoneNumber = register.PhoneNumber, IdCard = register.IdCard, Country = register.Country.ToUpper(), State = register.State.ToUpper(), City = register.City.ToUpper(), Adress = register.Adress, Gender = register.Gender,IsActivated=true};
                    roleid = "2301D884-221A-4E7D-B509-0113DCC045E3";
                }
                result = await _userManager.CreateAsync(user, register.Password);
                AddUserRole(user.Id, roleid);
                await _db.SaveChangesAsync();
            }
            return result;
        }




        // LoginUser Method : it's an action that look if user have authorisation to use the we app. 
        public async Task<SignInResult> LoginUser(LoginViewModel login, bool RememberMe, bool lockoutOnFailure,bool IsValid)
        {
            if (IsValid)
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, login.Password, login.RememberMe, lockoutOnFailure: false);

                    return result;
                }
            }
            return null;
        }

        //Logout
        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }




        // we use this Method to send confirmation email to users 
        public void SendConfirmationEmail(ApplicationUser user)
        {
            // path of temlate that send to users
            string FilePath = _environment.WebRootFileProvider.GetFileInfo("Templates/HtmlMailConfirmationPageModel.html")?.PhysicalPath; ;
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            string Body = $"<a  class=\"link-white\" style = \"color: #ffffff; text-decoration: none;\" href =\"https://localhost:44348/Account/ConfirmEmail?Email={user.Email}\">Confirm</a>";
            MailText = MailText.Replace("[username]", user.UserName).Replace("[email]", user.Email).Replace("[link]", Body);
            MailRequest mailRequest = new MailRequest();
            mailRequest.ToEmail = user.Email;
            mailRequest.UserName = user.UserName;
            mailRequest.Subject = $"Please Confirm Your Email";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            mailRequest.Body = builder.ToMessageBody();
            _mailService.SendEmailAsync(mailRequest);
        }        



      
        


        public async void AddUserRole(string userId ,string RoleId)
        {
            var userrole = new IdentityUserRole<string>();
            userrole.RoleId = RoleId;
            userrole.UserId = userId;
            await _db.UserRoles.AddAsync(userrole);  
        }



        // using this method to get all users 
        public IQueryable<ApplicationUser> GetAll()
        {
            IQueryable<ApplicationUser> Users = _db.AppUsers.Select(User => new ApplicationUser
            {
                IsDoctor = User.IsDoctor,
                Image = User.Image,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Birthday = User.Birthday,
                IdCard = User.IdCard,
                Country = User.Country,
                State = User.State,
                City = User.City,
                Adress = User.Adress,
                Gender = User.Gender,
                Email = User.Email,
                Ratings = User.Ratings,
                CreateDate = User.CreateDate

            });
            return Users;
        }


        // using this method to get users by their id
        public async Task<ApplicationUserViewModel> GetUserById(string id)
        {
            var User = await _userManager.FindByIdAsync(id);
            var userModel = User.ToViewModel();
            return userModel;
        }


        // ...... by their email
        public async Task<ApplicationUserViewModel> GetUserByEmail(string email)
        {
            var User = await _userManager.FindByEmailAsync(email);
            var userModel = User.ToViewModel();
            return userModel;
        }


        // .... by their user name
        public async Task<ApplicationUserViewModel> GetUserByUserName(string userName)
        {
            var User = await _userManager.FindByNameAsync(userName);
            var userModel = User.ToViewModel();
            return userModel;
        }

        // using this method to convert all users ApplicationUser class to ApplicationUserViewModel (model to view model)
        //public IQueryable<ApplicationUserViewModel> ModelsToViewModels(IQueryable<ApplicationUser> Users)
        //{
        //    IQueryable<ApplicationUserViewModel> UsersViewModel = Users.Select(UserViewModel => new ApplicationUserViewModel()
        //    {
        //        IsDoctor = UserViewModel.IsDoctor,
        //        Image = UserViewModel.Image,
        //        FirstName = UserViewModel.FirstName,
        //        LastName = UserViewModel.LastName,
        //        Birthday = UserViewModel.Birthday,
        //        IdCard = UserViewModel.IdCard,
        //        Country = UserViewModel.Country,
        //        State = UserViewModel.State,
        //        City = UserViewModel.City,
        //        Adress = UserViewModel.Adress,
        //        Gender = UserViewModel.Gender,
        //        Email = UserViewModel.Email
        //    });
        //    return UsersViewModel;
        //}
        //// using this method to convert ApplicationUser class to ApplicationUserViewModel (model to view model)
        //public ApplicationUserViewModel ModelToViewModel(ApplicationUser User)
        //{
        //    ApplicationUser UsersViewModel = new ApplicationUserViewModel()
        //    {
        //        IsDoctor = User.IsDoctor,
        //        Image = User.Image,
        //        FirstName = User.FirstName,
        //        LastName = User.LastName,
        //        Birthday = User.Birthday,
        //        IdCard = User.IdCard,
        //        Country = User.Country,
        //        State = User.State,
        //        City = User.City,
        //        Adress = User.Adress,
        //        Gender = User.Gender,
        //        Email = User.Email
        //    }.ToEnity();
        //    return UsersViewModel; throw new NotImplementedException();
        //}


        // get edit profile
        public EditProfileViewModel GetProfile(string id)
        {
            var profile = _db.Users.Find(id);
            EditProfileViewModel editprofile;
            if (profile == null)
            {
                return null;
            }
            if (profile.IsDoctor)
            {
                var doctor = (Models.Identity.Doctor)profile;
                editprofile = new EditProfileViewModel()
                {
                    Image = doctor.Image,
                    BirthDate = doctor.Birthday,
                    City = doctor.City,
                    Country = doctor.Country,
                    Email = doctor.Email,
                    FirstName = doctor.FirstName,
                    IdCard = doctor.IdCard,
                    LastName = doctor.LastName,
                    PhoneNumber = doctor.PhoneNumber,
                    Adress = doctor.Adress,
                    Specialist = doctor.Speciality,
                    Price = doctor.Price,
                    State = doctor.State
                };
            }
            else
            {
                var patient = (Patient)profile;
                editprofile = new EditProfileViewModel()
                {
                    Image = patient.Image,
                    BirthDate = patient.Birthday,
                    City = patient.City,
                    Country = patient.Country,
                    Email = patient.Email,
                    FirstName = patient.FirstName,
                    IdCard = patient.IdCard,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                    State = patient.State,
                    Gender=patient.Gender,
                    Adress=patient.Adress                   
                };
            }
            editprofile.IsDoctor = profile.IsDoctor;
            return editprofile;
        }

        public async Task<bool> EditProfile(EditProfileViewModel editprofile, string id)
        {
            var Uploader = new Services.Upload.UploadFile(_environment);
            byte[] ImageFile = Uploader.Upload(editprofile.ImageName);
            var profile = _db.Users.Find(id);
            if (profile == null)
            {
                return false;
            }
            if (editprofile.ImageName == null)
                ImageFile = profile.Image;
            if (profile.IsDoctor)
            {
                var doctor = (Models.Identity.Doctor)profile;
                doctor.Image = ImageFile;
                doctor.Adress = editprofile.Adress;
                doctor.Birthday = editprofile.BirthDate;
                doctor.City = editprofile.City;
                doctor.Country = editprofile.Country;
                doctor.Email = editprofile.Email;
                doctor.FirstName = editprofile.FirstName;
                doctor.IdCard = editprofile.IdCard;
                doctor.LastName = editprofile.LastName;
                doctor.Price = editprofile.Price;
                doctor.PhoneNumber = editprofile.PhoneNumber;
                doctor.Speciality = editprofile.Specialist;
                doctor.State = editprofile.State;
            }
            else
            {
                var patient = (Patient)profile;
                patient.Adress = editprofile.Adress;
                patient.Image = ImageFile;
                patient.Birthday = editprofile.BirthDate;
                patient.City = editprofile.City;
                patient.Country = editprofile.Country;
                patient.Email = editprofile.Email;
                patient.FirstName = editprofile.FirstName;
                patient.IdCard = editprofile.IdCard;
                patient.LastName = editprofile.LastName;
                patient.PhoneNumber = editprofile.PhoneNumber;
                patient.Adress = editprofile.Adress;
            }
            editprofile.Image = ImageFile;
            await _userManager.UpdateAsync(profile);
            await _db.SaveChangesAsync();
            _db.SaveChanges();
            return true;
        }
    }
}