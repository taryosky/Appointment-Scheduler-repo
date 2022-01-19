using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string  Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public int Duration { get; set; }
        public string DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public ApplicationUser Doctor { get; set; }

        public string PatientId { get; set; }

        [ForeignKey("PatientId")]
        public ApplicationUser Patient { get; set; }

        public bool IsDoctorApproved { get; set; }
        public string AdminId { get; set; }
        [ForeignKey("AdminId")]
        public ApplicationUser Admin { get; set; }
    }
}
