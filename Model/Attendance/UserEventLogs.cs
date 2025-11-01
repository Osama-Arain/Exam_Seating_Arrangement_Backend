using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ESA.Model;

namespace ESA.Model.Attendance
{
    [Table("UserEventLogs")]
    public class UserEventLog
    {
        [Key]
        public int Id { get; set; }
        public int? LogId { get; set; }

        [MaxLength(100)]
        public string? FacesluiceId { get; set; }

        [MaxLength(100)]
        public string? FacesluiceName { get; set; }

        [MaxLength(100)]
        public string? PersonName { get; set; }

        [MaxLength(100)]
        public string? PersonId { get; set; }

        [MaxLength(100)]
        public string? PersonType { get; set; }

        [MaxLength(100)]
        public string? RecordID { get; set; }

        [MaxLength(100)]
        public string? RFIDCard { get; set; }

        [MaxLength(100)]
        public string? EmployeeCode { get; set; }
        [MaxLength(100)]
        public string? Direction { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [MaxLength(100)]
        public string? VerifyStatus { get; set; }

        [MaxLength(100)]
        public string? LogType { get; set; }
        public string? Action { get; set; }

        public string? AttendanceType { get; set; }
        public DateTime? DeleteDate { get; set; }

        public DateTime? InsertDate { get; set; }
        public string? Type { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? UserIdDelete { get; set; }
        public Guid? UserIdInsert { get; set; }
        public Guid? UserIdUpdate { get; set; }

        public Guid? PersonalInformationId { get; set; }
        //public PersonalInformation PersonalInformations { get; set; }

        //[ForeignKey("PersonalInformationId")]
        //public PersonalInformation PersonalInformations { get; set; }
        public bool Active { get; set; }
        public Guid? RecordGuid { get; set; } = new Guid();
        public string? CheckType { get; set; }
    }
}