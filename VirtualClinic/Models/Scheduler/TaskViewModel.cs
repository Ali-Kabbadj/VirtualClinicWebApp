using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualClinic.Models
{
    public class TaskViewModel : ISchedulerEvent
    {


        public int TaskID { get; set; }
        public string Title { get; set; } = "No Title";
        public string Description { get; set; }

        private DateTime start;
        public DateTime Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value.ToUniversalTime();
            }
        }

        public string StartTimezone { get; set; }

        private DateTime end;
        public DateTime End
        {
            get
            {
                return end;
            }
            set
            {
                end = value.ToUniversalTime();
            }
        }

        public string EndTimezone { get; set; }

        public string RecurrenceRule { get; set; }
        public int? RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public bool IsAllDay { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string Color { get; set; }
        public int Identifier { get; set; }
        public string state { get; set; }
        public string DoctorName { get; set; }
        public TimeSpan Duration { get; set; }
        public string Speciality { get; set; }
        public long Amount { get; set; }


        public Task ToEntity()
        {
            return new Task
            {
                TaskId = TaskID,
                Title = Title,
                Start = Start,
                StartTimezone = StartTimezone,
                End = End,
                EndTimezone = EndTimezone,
                Description = Description,
                RecurrenceRule = RecurrenceRule,
                RecurrenceException = RecurrenceException,
                RecurrenceID = RecurrenceID,
                IsAllDay = IsAllDay,
                PatientId = PatientId,
                DoctorId = DoctorId,
                Color = Color,
                Identifier = Identifier,
                state = state,
                Speciality = Speciality,
                DoctorName = DoctorName,
                Amout = Amount
            };
        }
    }
}
