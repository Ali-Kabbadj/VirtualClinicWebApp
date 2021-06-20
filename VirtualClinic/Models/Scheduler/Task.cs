using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualClinic.Models
{
    public class Task
    {
        public Task()
        {
            this.Tasks1 = new HashSet<Task>();
        }
        [Key]
        public int TaskId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsAllDay { get; set; }
        public string RecurrenceRule { get; set; }
        public Nullable<int> RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public string StartTimezone { get; set; }
        public string EndTimezone { get; set; }
        public string Color { get; set; }
        public virtual ICollection<Task> Tasks1 { get; set; }
        public virtual Task Task1 { get; set; }
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public int Identifier { get; set; }
        public string state { get; set; }
        public string DoctorName { get; set; }
        public string Speciality { get; set; }
        public long Amout { get; set; }
    }
}
