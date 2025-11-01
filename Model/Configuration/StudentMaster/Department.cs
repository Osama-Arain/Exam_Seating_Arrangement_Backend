using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESA.Model
{
    [Table("Department")]
    public class Department
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public int Code { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }

        public string? Action { get; set; }

        public Guid? UserIdInsert { get; set; }
        public DateTime? InsertDate { get; set; }

        public Guid? UserIdUpdate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public Guid? UserIdDelete { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
