namespace ESA.Views.Dashboard
{
    public class ESSDashboardViewModel
    {
        public string TotalAttendanceDays { get; set; }
        public string TotalPresentDays { get; set; }
        public string TotalAbsentDays { get; set; }
        public string TotallateDays { get; set; }

        // REQUESTS
        public List<DashboardRequestsViewModel> RequestsList { get; set; }


        // MONTHLY ATTENDACE CALENDAR
        public List<DashboardHolidaysViewModel> AttendanceList { get; set; }

        // LEAVES
        //public string TotalLeaves { get; set; }
        //public string AvailedLeaves { get; set; }
        //public string RemainingLeaves { get; set; }

        public List<object> LeaveData { get; set; }

        // ATTENDANCE RATE
        public double AttendanceRate { get; set; }
        public double AbsentRate { get; set; }

        // ATTENDANCE MONTHLY BAR

        public object MonthlyAttendanceData { get; set; }

        //public string TotalDates {get;set;}
        //public string TotalAttendancePresent {get;set;}
        //public string TotalAttendanceLate {get;set;}
        //public string TotalAttendanceEarlyGoing {get;set;}
        //public string TotalAttendanceHalfday {get;set;}
        //public string TotalAttendanceQuarterday {get;set;}
        //public string TotalAttendanceAbsen {get;set;}
    }

    public class DashboardRequestsViewModel
    {
        public string Type { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }

    public class DashboardHolidaysViewModel
    {
        public string Date { get; set; }
        public string Status { get; set; }
    }
}
