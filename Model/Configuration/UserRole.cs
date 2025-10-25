using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace payday_server.Model
{
    [Table ("UserRole")]
    public class UserRole {

        [Key]
        public Guid Id { get; set; } = new Guid ();

        [Required]
        public int Code { get; set; } 

        [Required]
        [StringLength (250)]
        public string Role { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        [StringLength(1)]
        public string Type { get; set; }

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