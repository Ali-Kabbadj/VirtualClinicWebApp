using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualClinic.Data;
using VirtualClinic.Models;
using VirtualClinic.Models.Identity;
using VirtualClinic.ViewModels.Payment;
using Task = VirtualClinic.Models.Task;

namespace VirtualClinic.Controllers
{

    public class ManagePatientAppointmentsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _SignrManager;
        private SchedulerTaskService taskService;

        public ManagePatientAppointmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignrManager)
        {
            _db = context;
            _db = context;
            _userManager = userManager;
            this.taskService = new SchedulerTaskService(context, _userManager);
            _SignrManager = SignrManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        [HttpGet]
            public async Task<string> GetCurrentUserId()
            {
                ApplicationUser usr = await GetCurrentUserAsync();
                return usr?.Id;
            }

        public IActionResult Index()
        {
            var Appointments = taskService.GetAll(GetCurrentUserId().Result).Where(U => U.PatientId == _userManager.GetUserAsync(User).Result.Id);         
            return View(Appointments);
        }

        public ActionResult CancelAppointment(int TaskId)
        {
            Task T = _db.Tasks.Where(Task => Task.TaskId == TaskId).First();
            taskService.Cancel(T);
            return RedirectToAction("Index");
        }


        public IActionResult Payment(int TaskId)
        {
            TaskPayment Task = new TaskPayment();
            Task.TaskToPay = taskService.GetAll().Where(task => task.TaskID == TaskId).First().ToEntity();
            Task.Amount = _db.Doctors.Where(doctor => doctor.Id == Task.TaskToPay.DoctorId).First().Price;
            ViewBag.PaymentAmount = Task.Amount;
            return RedirectToAction("Index", "Payment", new { id = TaskId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Processing(string stripeToken, string stripeEmail, int TaskId)
        {

            TaskPayment Task = new TaskPayment();
            Task.TaskToPay = taskService.GetAll().Where(task => task.TaskID == TaskId).First().ToEntity();
            Task.Amount = _db.Doctors.Where(doctor => doctor.Id == Task.TaskToPay.DoctorId).First().Price;

            CustomerService customers = new Stripe.CustomerService();
            var charges = new ChargeService();


        
            var charge = charges.Create(new Stripe.ChargeCreateOptions
            {
                
                Amount = long.Parse(Task.Amount.ToString()),
                Description = "Appointment Payment",
                Currency = "usd",
                Source = stripeToken,
                ReceiptEmail = stripeEmail,
                Metadata = new Dictionary<string, string>()
                {
                    {"AppointmentId", Task.TaskToPay.TaskId.ToString() },
                    {"DoctorName", Task.TaskToPay.DoctorName }
                }
            });

            if (charge.Status == "succeeded")
            {
                Models.Task Task1 = _db.Tasks.Where(t => t.TaskId == Task.TaskToPay.TaskId).First();
                Task1.state = "Confirmed";
                _db.Tasks.Attach(Task1);
                _db.Entry(Task1).State = EntityState.Modified;
                _db.SaveChanges();
                string BalanceTransactionId = charge.BalanceTransactionId;
                var email = User.FindFirst("sub")?.Value;
                var customerPaid = charge.Paid;
                customerPaid = true;

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index");
        }

    }
}
