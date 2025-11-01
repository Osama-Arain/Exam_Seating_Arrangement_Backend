using System.ComponentModel.DataAnnotations;

namespace ESA.Views.Shared
{
    
    public class TenantBaseModel {

       public int Code { get; set; }
       
       [Required]
       public string Name { get; set; }

       [Required]
       public string ShortName { get; set; }
       public string Phone { get; set; }
       public string Mobile { get; set; }
       public string Email { get; set; }
       public string Address { get; set; }
       public string Type { get; set; }
       public bool Active { get; set; }

    }

    public class TenantViewModel : TenantBaseModel {
       public Guid Id { get; set; }
       public string CompanyName{ get; set; }
       public int LastCode{ get; set; }
       
       public bool PermissionView{ get; set; }
       public bool PermissionAdd{ get; set; }
       public bool PermissionUpdate{ get; set; }
       public bool PermissionDelete{ get; set; }

    }

    public class TenantViewByIdModel : TenantBaseModel {
       public Guid Id { get; set; }
       public Guid CompanyId { get; set; }
       public string CompanyName{ get; set; }
    }

    public class TenantAddModel : TenantBaseModel {
       public Guid Id { get; set; }
       public Guid CompanyId { get; set; }
    }
    
    public class TenantUpdateModel : TenantBaseModel {
       public Guid Id { get; set; }
       public Guid CompanyId { get; set; }
    }

     public class TenantDeleteModel : TenantBaseModel {
       public Guid Id { get; set; }
    }
}