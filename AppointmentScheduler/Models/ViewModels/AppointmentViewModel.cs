using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AppointmentScheduler.Models.ViewModels
{
    public class AppointmentViewModel
    {
        public int? Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string StartDate { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        [DisplayName("Doctor")]
        public string DoctorId { get; set; }

        public string Doctor { get; set; }

        [Required]
        [DisplayName("Patient")]
        public string PatientId { get; set; }

        public string Patient { get; set; }
        public string EndDate { get; set; }

        public bool IsDoctorApproved { get; set; }
        public string AdminId { get; set; }

        public string Admin { get; set; }

        public bool IsForClient { get; set; }
    }
}
