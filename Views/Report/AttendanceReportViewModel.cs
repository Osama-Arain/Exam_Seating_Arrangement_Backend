using System.ComponentModel.DataAnnotations;

namespace ESA.Views.Report
{
    public class AttendanceReportViewModel
    {
        public int Code { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string Month { get; set; }
        public int TotalLates { get; set; }
        public int TotalAbsents { get; set; }
        public int TotalAllowedLateEarlyLeaves { get; set; }
        public int TotalEarlyLeaves { get; set; }
        public double TotalDeductedLatesEarly { get; set; }
        public int TotalAllowedHalfdays { get; set; }
        public double TotalHalfdays { get; set; }
        public double TotalDeductedHalfdays { get; set; }
        public double TotalQuarterDays { get; set; }
        public double TotalDeductedQuarterDays { get; set; }
        public List<DateTime>? TotalMissingAttendanceDays { get; set; }
        public double TotalOvertime { get; set; }

        public double TotalLeaves { get; set; }
        public double TotalHolidays { get; set; }
        public double TotalWorkingHours { get; set; }
        public double TotalRequiredHours { get; set; }
        public double TotalPaydays { get; set; }
        public double DeductedPaydays { get; set; }
        public double FinalizedPaydays { get; set; }
        public double totalAttendance { get; set; }
        public string MonthlyDays { get; set; }
        public double DeductedDays { get; set; }
    }
}
