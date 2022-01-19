using Microsoft.AspNetCore.Identity;

using System.Collections.Generic;

namespace AppointmentScheduler.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
        //public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
