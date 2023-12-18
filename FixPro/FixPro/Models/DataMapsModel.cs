﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace FixPro.Models
{
    public class DataMapsModel
    {
        public Empoylee EmpData { get; set; }
        public int Id { get; set; } = 0;
        public int BranchId { get; set; }
        public int EmployeeId { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string CreateDate { get; set; }
        public string Time { get; set; }
        public Position MPosition { get; set; }
    }

    public class Empoylee
    {
        public int Tracking_id { get; set; }
        public string BranchId { get; set; }
        public string EmployeeId { get; set; }
        public string lat { get; set; }
        public string log { get; set; }
        public string date { get; set; }
        public string time { get; set; }
    }
}
