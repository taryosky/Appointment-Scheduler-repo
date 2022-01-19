using AppointmentScheduler.Models.ViewModels;
using AppointmentScheduler.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppointmentScheduler.Controllers.Api
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentApiController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly string loginUserId;
        private readonly string role;

        public AppointmentApiController(IAppointmentService appointmentService, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentService = appointmentService;
            loginUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            role = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
        }

        [HttpPost]
        public async Task<IActionResult> SaveCalendarData(AppointmentViewModel model)
        {
            CommonResponse<int> res = new CommonResponse<int>();
            model.AdminId = loginUserId;
            try {
                res.Status = await _appointmentService.AddUpdate(model);
                if(res.Status == 1)
                {
                    res.Message = "Appointment Added successfully";
                }
                if(res.Status == 2)
                {
                    res.Message = "Appointment updated successfuly";
                }
            }catch(Exception e)
            {
                res.Status = 0;
                res.Message = e.Message;
            }

            return Ok(res);
        }

        [HttpGet("GetCalendarData")]
        public async Task<IActionResult> GetDoctorAppointments(string doctorId)
        {
            CommonResponse<ICollection<AppointmentViewModel>> res = new CommonResponse<ICollection<AppointmentViewModel>>();
            if (role == "Patient")
            {
                res.Data = await _appointmentService.GetPatientAppointments(loginUserId);
                res.Status = 1;
            }
            else if(role == "Doctor")
            {
                res.Data = await _appointmentService.GetDoctorAppoints(loginUserId);
                res.Status = 1;
            }
            else
            {
                res.Data = await _appointmentService.GetDoctorAppoints(doctorId);
                res.Status = 1;
            }

            return Ok(res);
            
        }

        //[HttpGet("get-patient-appointment")]
        //public async Task<IActionResult> GetPatientAppointments(string patientId)
        //{
        //    var appoint = await _appointmentService.GetPatientAppointments(patientId);
        //    var res = new CommonResponse<ICollection<AppointmentViewModel>>
        //    {
        //        Status = 1,
        //        Message = "Appointment retrieved",
        //        Data = appoint
        //    };
        //    return Ok(res);
        //}

        [HttpGet("GetCalendarDataById/{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            CommonResponse<AppointmentViewModel> res = new CommonResponse<AppointmentViewModel>();
            res.Data = await _appointmentService.GetAppointById(id);
            res.Status = 1;
            return Ok(res);

        }
    }
}
