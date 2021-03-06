using AppointmentScheduler.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        public IActionResult Index()
        {
            ViewBag.Duration = Helpers.RolesHelper.GetTimeDuration();
            ViewBag.DoctorList = _appointmentService.GetDoctors();
            ViewBag.PatientList = _appointmentService.GetPatients();
            return View();
        }
    }
}
