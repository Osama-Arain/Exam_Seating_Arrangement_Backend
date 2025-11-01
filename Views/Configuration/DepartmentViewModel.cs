using System.ComponentModel.DataAnnotations;

namespace ESA.Views.Payroll
{

    public class DepartmentBaseModel
    {
        public int Code { get; set; }
        [Required]
        public string Name { get; set; }

        public Guid EmployeeBranchId { get; set; }

        public string Type { get; set; }
        public bool Active { get; set; }

    }

    public class DepartmentViewModel : DepartmentBaseModel
    {
        public Guid Id { get; set; }
        public int LastCode { get; set; }

        public string EmployeeBranchName { get; set; }
        public bool PermissionView { get; set; }
        public bool PermissionAdd { get; set; }
        public bool PermissionUpdate { get; set; }
        public bool PermissionDelete { get; set; }

    }

    public class DepartmentViewByIdModel : DepartmentBaseModel
    {
        public Guid Id { get; set; }
    }

    public class DepartmentAddModel : DepartmentBaseModel
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid MenuId { get; set; }
    }

    public class DepartmentUpdateModel : DepartmentBaseModel
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid MenuId { get; set; }

    }

    public class DepartmentDeleteModel : DepartmentBaseModel
    {
        public Guid Id { get; set; }
        public Guid MenuId { get; set; }

    }
}