using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualClinic.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int NumberOfDoctors { get; set; }
        public int NumberOfPatients { get; set; }
        public int NumberOfConfirmeedAppointments { get; set; }
        public int NumberOfUnConfirmeedAppointments { get; set; }
    }
}
