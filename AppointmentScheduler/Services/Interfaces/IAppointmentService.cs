using AppointmentScheduler.Models.ViewModels;
using System.Collections.Generic;


namespace AppointmentScheduler.Services.Interfaces
{
    public interface IAppointmentService
    {
        public IEnumerable<DoctorViewModel> GetDoctors();
        public IEnumerable<PatientViewModel> GetPatients();
    }
}
