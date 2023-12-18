using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Text;

namespace FixPro.Model
{
    public class CalendarModel
    {
        public SelectionRange SelectedRange { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
