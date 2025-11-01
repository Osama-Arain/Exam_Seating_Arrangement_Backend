using ESA.Layers.ContextLayer;
using ESA.Model;
using ESA.Shared;
using Microsoft.EntityFrameworkCore;
using ESA.Views.Shared;
using System.Net;
using System.Net.Sockets;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ESA.Repository
{
    public interface IAuthServiceRepository
    {
       Task<ApiResponse> LoginAsync (LoginPayloadViewModel _model);
    }

    public class AuthServiceRepository : IAuthServiceRepository
    {
        private readonly AppDBContext _context;
        private readonly IConfiguration _configuration;

        public AuthServiceRepository(AppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ApiResponse> LoginAsync(LoginPayloadViewModel _payload)
        {
            var apiResponse = new ApiResponse();
            try
            {

                var _HashPassword = SecurityHelper.EncryptString("1234567890123456", _payload.Password);
                var _User = await _context.Users
                .Include(r => r.UserRole)
                .Where(a => a.Action != Enums.Operations.D.ToString() &&
                a.Email == _payload.Email).FirstOrDefaultAsync();

                if (_User == null)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "There is no user with that Email address";
                    return apiResponse;
                }

                if (_User.HashPassword != _HashPassword)
                {
                    apiResponse.statusCode = StatusCodes.Status401Unauthorized.ToString();
                    apiResponse.message = "Incorrect Email/Password!";
                    return apiResponse;
                }

                if (!_User.Active)
                {
                    apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                    apiResponse.message = "Account is in-active.";
                    return apiResponse;
                }

                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                var IP = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
                var PC = System.Environment.MachineName.ToString();
                var Header = "";

                var TokenStr = await GetGeneratedToken(_User, IP.ToString(), PC, Header);

                var profile = "";
                var contact = "";
                var salary = "";
                var empCode = "";

                var roleName = "";
                var currentEmpRole = _context.UserRoles.Where(x => x.Id == _User.RoleId).FirstOrDefault();
                if (currentEmpRole != null)
                {
                    roleName = currentEmpRole.Role;
                }




                LoginResponseViewModel _model = new LoginResponseViewModel();
                _model.FirstName = _User.FirstName;
                _model.LastName = _User.LastName;
                _model.Email = _User.Email;
                //_model.TenantName = _User.Tenants.Name;
                _model.Id = _User.Id;
                _model.Contact = _User.Contact;
                _model.EmployeeCode = empCode;
                _model.Profile = profile;
                _model.Contact = contact;
                _model.Salary = salary;
                _model.RoleName = roleName;
                _model.ImgCheckFlag = false;


                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.message = TokenStr;
                apiResponse.data = _model;
                return apiResponse;

            }
            catch (Exception e)
            {

                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = e.Message.ToString() + innerexp;
                return apiResponse;
            }
        }


        private async Task<string> GetGeneratedToken(object _model, string _Ip, string _PC, string _Header)
        {
            var UserTable = (ESA.Model.User)_model;
            string _key = SecurityHelper.security(UserTable.Id + UserTable.NormalizedName + UserTable.Email + DateTime.Now.ToString("ddMMMyyyyHHHmmss"));

            string key = _configuration["AuthSettings:Key"];
            var issuer = _configuration["AuthSettings:Issuer"];
            var audience = _key; //_configuration["AuthSettings:Audience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<System.Security.Claims.Claim>();
            permClaims.Add(new System.Security.Claims.Claim(Enums.Misc.UserId.ToString(), UserTable.Id.ToString()));
            permClaims.Add(new System.Security.Claims.Claim(Enums.Misc.UserName.ToString(), UserTable.NormalizedName));
            permClaims.Add(new System.Security.Claims.Claim(Enums.Misc.Email.ToString(), UserTable.Email));
            permClaims.Add(new System.Security.Claims.Claim(Enums.Misc.Key.ToString(), _key));

            var token = new JwtSecurityToken(issuer,
                audience,
                permClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);


            return tokenAsString;
        }
    }
}