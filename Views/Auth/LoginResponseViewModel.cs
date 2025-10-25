namespace payday_server.Views.Shared
{

    public class LoginResponseViewModel {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TenantName { get; set; }
        public string Contact { get; set; }
        public bool? ImgCheckFlag { get; set; }
        public string Profile { get; set; }
        public string EmployeeCode { get; set; }
        public string Salary { get; set; }
        public string RoleName { get; set; }


    }

    public class UserLoginInfoViewModel {
        public Guid TenantId { get; set; }
        public string TenantName { get; set; }
    }
}