using AppointmentScheduler.Helpers;
using AppointmentScheduler.Models;
using AppointmentScheduler.Models.ViewModels;
using AppointmentScheduler.Services.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _ctx;

        public AppointmentService(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx)
        {
            _userManager = userManager;
            _ctx = ctx;
        }

        public async Task<int> AddUpdate(AppointmentViewModel model)
        {
            if(model.Id == null || model.Id <= 0)
            {
                var appoint = new Appointment
                {
                    Title = model.Title,
                    Description = model.Description,
                    PatientId = model.PatientId,
                    DoctorId = model.DoctorId,
                    StartDate = DateTime.Parse(model.StartDate),
                    EndDate = DateTime.Parse(model.StartDate).AddMinutes(Convert.ToDouble(model.Duration)),
                    AdminId = model.AdminId,
                    Duration = model.Duration
                };

                _ctx.Appointments.Add(appoint);
                await _ctx.SaveChangesAsync();
                return 1;
            }

            var appointment = _ctx.Appointments.Find(model.Id);
            if (appointment == null)
                return 0;

            appointment.Id = model.Id.Value;
            appointment.Title = model.Title;
            appointment.Description = model.Description;
            appointment.PatientId = model.PatientId;
            appointment.DoctorId = model.DoctorId;
            appointment.StartDate = DateTime.Parse(model.StartDate);
            appointment.EndDate = DateTime.Parse(model.StartDate).AddMinutes(Convert.ToDouble(model.Duration));
            appointment.AdminId = model.AdminId;
            appointment.Duration = model.Duration;
            appointment.IsDoctorApproved = model.IsDoctorApproved;

            _ctx.Appointments.Update(appointment);
            await _ctx.SaveChangesAsync();
            return 2;
        }

        public async Task<AppointmentViewModel> GetAppointById(int id)
        {
            var app = await _ctx.Appointments.Include(x => x.Patient).Include(x => x.Doctor)
                .FirstOrDefaultAsync(x => x.Id == id);
            var appToReturn = new AppointmentViewModel {
                Title = app.Title, AdminId = app.AdminId, Description = app.Description, Id = app.Id, DoctorId = app.DoctorId,
                StartDate = app.StartDate.ToString("yy-MM-dd HH-mm-ss"), EndDate = app.EndDate.ToString("yy-MM-dd HH:mm:ss"),
                Duration = app.Duration, IsDoctorApproved = app.IsDoctorApproved, PatientId = app.PatientId, IsForClient = false,
                Patient = app.Patient.Name,
                Doctor = app.Doctor.Name
            };
            return appToReturn;
        }

        public async Task<ICollection<AppointmentViewModel>> GetDoctorAppoints(string userId)
        {
            return await _ctx.Appointments.Where(x => x.DoctorId == userId).Select(x => new AppointmentViewModel
            {
                Id = x.Id, Title = x.Title, AdminId = x.AdminId, Description = x.Description, DoctorId = x.DoctorId, Duration = x.Duration, 
                IsForClient = false, PatientId = x.PatientId, StartDate = x.StartDate.ToString("yyy-MM-dd HH:mm:ss"), 
                IsDoctorApproved = x.IsDoctorApproved, EndDate = x.EndDate.ToString("yyy-MM-dd HH:mm:ss")
            }).ToListAsync();
        }

        public IEnumerable<DoctorViewModel> GetDoctors()
        {
            return _userManager.GetUsersInRoleAsync(RolesHelper.Doctor)
                .Result.Select(u => new DoctorViewModel { Id = u.Id, Name = u.Name });
        }

        public async Task<ICollection<AppointmentViewModel>> GetPatientAppointments(string userId)
        {
            return await _ctx.Appointments.Where(x => x.PatientId == userId).Select(x => new AppointmentViewModel
            {
                Id = x.Id,
                Title = x.Title,
                AdminId = x.AdminId,
                Description = x.Description,
                DoctorId = x.DoctorId,
                Duration = x.Duration,
                IsForClient = true,
                PatientId = x.PatientId,
                StartDate = x.StartDate.ToString("yyy-MM-dd HH:mm:ss"),
                IsDoctorApproved = x.IsDoctorApproved,
                EndDate = x.EndDate.ToString("yyy-MM-dd HH:mm:ss")
            }).ToListAsync();
        }

        public IEnumerable<PatientViewModel> GetPatients()
        {
            return _userManager.GetUsersInRoleAsync(RolesHelper.Patient)
                .Result.Select(u => new PatientViewModel { Id = u.Id, Name = u.Name });
        }
    }
}
