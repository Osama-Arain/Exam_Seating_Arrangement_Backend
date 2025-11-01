using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESA.Model
{
    [Table ("User")]
    public class User {

        [Key]
        public Guid Id { get; set; } = new Guid ();

        [Required]
        public int Code { get; set; } 


        [Required]
        [StringLength (250)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NormalizedName { get; set; } = "";

        public string Contact { get; set; }

        public string CNIC { get; set; }

        public string Email { get; set; }
        
        public string HashPassword { get; set; }

        public Guid RoleId { get; set; }
        [ForeignKey("RoleId")]
        public UserRole UserRole { get; set; }

        public DateTime PermitForm { get; set; }
        public DateTime PermitTo { get; set; }

        public bool TenantsCheck { get; set; }
        
        [Required]
        [StringLength(1)]
        public string Type { get; set; }

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