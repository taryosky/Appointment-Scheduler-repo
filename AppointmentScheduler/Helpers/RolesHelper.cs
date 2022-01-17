using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Helpers
{
    public static class RolesHelper
    {
        public static string Admin = "Admin";
        public static string Doctor = "Doctor";
        public static string Patient = "Patient";

        public static List<SelectListItem> GetRoles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Text = Admin, Value = Admin},
                new SelectListItem{Text = Doctor, Value = Doctor},
                new SelectListItem{Text = Patient, Value=Patient}
            };
        }
    }
}
