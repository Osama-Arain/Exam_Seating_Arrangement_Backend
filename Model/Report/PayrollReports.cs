using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payday_server.Model.Report
{
    [Table("PayrollReports")]

    public class PayrollReports
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        public Guid EmployeeId { get; set; }
        public string? Name { get; set; }
        public string? EmployeeCode { get; set; }
        public string? Month { get; set; }
        public string? TotalDays { get; set; }
        public string? MonthlyDays { get; set; }
        public string? Paydays { get; set; }
        public string? Leaves { get; set; }
        public string? Holidays { get; set; }
        public string? Absents { get; set; }
        public string? Lates { get; set; }
        public string? EarlyLeaves { get; set; }
        public string? HalfDays { get; set; }
        public string? QuarterDays { get; set; }
        public string? OTHours { get; set; }
        public string? TotalHours { get; set; }
        public string? DeductedDays { get; set; }
        public string? BasicSalary { get; set; }
        public string? AdvanceSalary { get; set; }
        public string? TotalAllowances { get; set; }
        public string? TotalDeductions { get; set; }
        public string? TotalLoan { get; set; }
        public string? DeductedLoan { get; set; }
        public string? OvertimePay { get; set; }
        public string? NetPay { get; set; }
        public string? OTPay { get; set; }

        public bool IsLocked { get; set; }

        public string? Action { get; set; }
        public Guid? UserIdInsert { get; set; }
        public DateTime? InsertDate { get; set; }
        public Guid? UserIdUpdate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? UserIdDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
        [NotMapped]
        public int? LastCode { get; set; }
        [NotMapped]
        public Guid? User { get; set; }
    }
}
