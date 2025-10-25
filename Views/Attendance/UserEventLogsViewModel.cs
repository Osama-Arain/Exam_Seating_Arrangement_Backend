using System.ComponentModel.DataAnnotations;

namespace payday_server.Views.Shared
{

    public class UserEventLogsBaseModel
    {
        // public int Code { get; set; }
        [Required]
        public Guid RecordGuid { get; set; }
        public string? facesluiceId { get; set; }
        public string? personId { get; set; }
        public string? facesluiceName { get; set; }
        public string? personName { get; set; }
        public string? Type { get; set; }
        public string? EmployeeCode { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public Guid? PersonalInformationId { get; set; }
        public string? VerifyStatus { get; set; }
        public double? TotalHours { get; set; }
        public double? OvertimeHours { get; set; }
        public bool IsAbsent { get; set; }
        public bool EmptyRecord { get; set; }
        public string? LogType { get; set; }
        public string? Action { get; set; }
        public bool Active { get; set; }

    }

    public class UserEventLogsViewModel : UserEventLogsBaseModel
    {
        public DateTime Date { get; set; }
        public string? Name { get; set; }
        public string? FatherName { get; set; }
        public string? AttendanceType { get; set; }
        public Guid ShiftID { get; set; }
        public string? ShiftName { get; set; }
        public string? Remarks { get; set; }
        public bool PermissionView { get; set; }
        public bool PermissionAdd { get; set; }
        public bool PermissionUpdate { get; set; }
        public bool PermissionDelete { get; set; }

    }

    public class UserEventLogsViewByIdModel : UserEventLogsBaseModel
    {
    }

    public class UserEventLogsAddModel : UserEventLogsBaseModel
    {
        public Guid? CompanyId { get; set; }
        public Guid MenuId { get; set; }
    }

    public class UserEventLogsUpdateModel : UserEventLogsBaseModel
    {
        public Guid CompanyId { get; set; }
        public Guid MenuId { get; set; }

    }

    public class UserEventLogsDeleteModel : UserEventLogsBaseModel
    {
        public Guid MenuId { get; set; }

    }
}