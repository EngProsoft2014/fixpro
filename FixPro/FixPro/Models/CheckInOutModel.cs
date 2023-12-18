﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FixPro.Models
{
    public class CheckInOutModel
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public int? BrancheId { get; set; }
        public string BranchName { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Date { get; set; }
        public string HoursFrom { get; set; }
        public string HoursTo { get; set; }
        //public int? TotalHour { get; set; }
        //public int? TotalMinate { get; set; }
        public string DurationHours { get; set; }
        public string DurationMinutes { get; set; }
        public string SheetColor { get; set; }
        public string Notes { get; set; }
        public bool? Active { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? MytimeSheet { get { return (EmployeeId == int.Parse(Helpers.Settings.UserId)) ? 1 : 0; } }
        public int? ShowEndTimeBtn { get { return (EmployeeId == int.Parse(Helpers.Settings.UserId) && Id != 0) ? 1 : 0; } }
    }
}
