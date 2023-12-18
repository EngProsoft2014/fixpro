using System;
using System.Collections.Generic;
using System.Text;

namespace FixPro.Models
{
    public class EmployeesInPageModel
    {
        public List<EmployeeModel> EmployeesInPage { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
