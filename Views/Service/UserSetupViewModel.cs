using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace payday_server.Views.Service
{


    public class MenuPermissionPayLoadServicesModel
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }

    }

    public class MenuPermissionViewModel
    {
        [Required]
        public Guid ModuleId { get; set; }

        [Required]
        public string ModuleName { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public Guid MenuId { get; set; }

        [Required]
        public string MenuName { get; set; }

        [Required]
        public string MenuAlias { get; set; }

        [Required]
        public bool View_Permission { get; set; }

        [Required]
        public bool Insert_Permission { get; set; }

        [Required]
        public bool Update_Permission { get; set; }

        [Required]
        public bool Delete_Permission { get; set; }

        [Required]
        public bool Print_Permission { get; set; }

        [Required]
        public bool Check_Permission { get; set; }

        [Required]
        public bool Approve_Permission { get; set; }

    }

    public class MenuPermissionViewRoleModel
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<MenuPermissionViewModel> menuPerViews { get; set; }

    }
}