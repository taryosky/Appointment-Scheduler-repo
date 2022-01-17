using AppointmentScheduler.Helpers;
using AppointmentScheduler.Models;
using AppointmentScheduler.Models.ViewModels;
using AppointmentScheduler.Services.Interfaces;

using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IEnumerable<DoctorViewModel> GetDoctors()
        {
            return _userManager.GetUsersInRoleAsync(RolesHelper.Doctor)
                .Result.Select(u => new DoctorViewModel { Id = u.Id, Name = u.Name });
        }

        public IEnumerable<PatientViewModel> GetPatients()
        {
            return _userManager.GetUsersInRoleAsync(RolesHelper.Patient)
                .Result.Select(u => new PatientViewModel { Id = u.Id, Name = u.Name });
        }
    }
}
