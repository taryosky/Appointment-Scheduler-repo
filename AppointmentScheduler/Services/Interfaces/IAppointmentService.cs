using AppointmentScheduler.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentScheduler.Services.Interfaces
{
    public interface IAppointmentService
    {
        public IEnumerable<DoctorViewModel> GetDoctors();
        public IEnumerable<PatientViewModel> GetPatients();
        public Task<int> AddUpdate(AppointmentViewModel model);
        public Task<ICollection<AppointmentViewModel>> GetDoctorAppoints(string userId);
        public Task<ICollection<AppointmentViewModel>> GetPatientAppointments(string userId);
        public Task<AppointmentViewModel> GetAppointById(int id);

    }
}
