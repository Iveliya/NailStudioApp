﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NailStudioApp.Web.ViewModel.Appointment
{
    public class CreateAppointmentViewModel
    {
        public Guid ServiceId { get; set; }
        public Guid StaffMemberId { get; set; }
        public DateTime SelectedDate { get; set; }
        public TimeSpan SelectedTime { get; set; }
        public List<SelectListItem> Services { get; set; }
        public List<SelectListItem> StaffMembers { get; set; }
        public List<SelectListItem> AvailableTimes { get; set; }
    }
}