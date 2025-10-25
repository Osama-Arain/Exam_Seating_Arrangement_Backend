using payday_server.Layers.ContextLayer;
using payday_server.Manager;
using payday_server.Model;
using Microsoft.EntityFrameworkCore;
using payday_server.Shared;
using payday_server.Views.Shared;
using System.Security.Claims;

namespace payday_server.Processor.Payroll.Setup
{
    public class UserProcessor : IProcessor<UsersBaseModel>
    {
         private AppDBContext _context;
         private IManager? _manager;
         public  UserProcessor (AppDBContext context) {
            _context = context;
            _manager = Builder.MakeManagerClass(Enums.ModuleClassName.User, _context); 
        }

        public async Task<ApiResponse> ProcessGet(Guid MenuId, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse ();
            if (_manager != null) {
                var response = await _manager.GetDataAsync(_User);
                if (Convert.ToInt32 (response.statusCode) == 200) {
                    var _Table = response.data as IEnumerable<User>;
                    var apiResponseUser = await SecurityHelper.UserMenuPermissionAsync(MenuId, _User);
                    if (apiResponseUser.statusCode.ToString() != StatusCodes.Status200OK.ToString()) { return apiResponseUser; }
                    var _UserMenuPermissionAsync = (GetUserPermissionViewModel)apiResponseUser.data;

                     int? maxId = _context.Users.Max(entity => (int?)entity.Code);
                    if (!maxId.HasValue || maxId.Value == 0)
                        maxId = 1;
                    else
                        maxId = maxId.Value + 1;

                    var result = (from ViewTable in _Table select new UsersViewModel {
                            Id = ViewTable.Id,
                            Code = ViewTable.Code,
                            NormalizedName = ViewTable.FirstName + " " + ViewTable.LastName ,
                            Contact = ViewTable.Contact,
                        CNIC = ViewTable.CNIC,
                            Email = ViewTable.Email,
                            Role = ViewTable.UserRole.Role,
                            Type = ViewTable.Type,
                            Active = ViewTable.Active,
                            LastCode = (int ) maxId,
                            PermissionView = _UserMenuPermissionAsync.View_Permission, 
                            PermissionAdd = _UserMenuPermissionAsync.Insert_Permission,
                            PermissionUpdate = _UserMenuPermissionAsync.Update_Permission,
                            PermissionDelete = _UserMenuPermissionAsync.Delete_Permission,
                    }).ToList();
                    response.data = result;
                }
                return response;
            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
            apiResponse.message = "Invalid Class";
            return apiResponse;
        }

        public async Task<ApiResponse> ProcessGetById(Guid _Id, Guid _MenuId, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse ();
            if (_manager != null) {
                var response = await _manager.GetDataByIdAsync (_Id,_User);
                if (Convert.ToInt32 (response.statusCode) == 200) {
                    var _Table = response.data as User;
                    var _ViewModel = new UsersViewByIdModel {
                        Id = _Table.Id,
                        Code = _Table.Code,
                        FirstName = _Table.FirstName ,
                        LastName =  _Table.LastName ,
                        TenantsCheck = _Table.TenantsCheck,
                        Contact = _Table.Contact,
                        CNIC = _Table.CNIC,
                        PermitForm = _Table.PermitForm,
                        PermitTo = _Table.PermitTo,
                        HashPassword = SecurityHelper.DecryptString("1234567890123456",_Table.HashPassword),
                        Email = _Table.Email,
                        Role = _Table.UserRole.Role,
                        RoleId = _Table.RoleId,
                        Type = _Table.Type,
                        Active = _Table.Active
                    };
                    response.data = _ViewModel;
                }
                return response;
            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
            apiResponse.message = "Invalid Class";
            return apiResponse;
        }

        public async Task<ApiResponse> ProcessPost(object _Usermodel, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse ();
            var User = (UsersAddModel) _Usermodel;
            
            if (_manager != null) {
                var _Table = new User {
                    Code = User.Code,
                    FirstName = User.FirstName ,
                    LastName =  User.LastName ,
                    NormalizedName = User.FirstName + " " + User.LastName ,
                    TenantsCheck = User.TenantsCheck,
                    Contact = User.Contact,
                    CNIC = User.CNIC,
                    PermitForm = User.PermitForm,
                    PermitTo = User.PermitTo,
                    HashPassword =  SecurityHelper.EncryptString("1234567890123456",User.HashPassword),
                    Email = User.Email,
                    RoleId = User.RoleId,
                    Type = User.Type,
                    Active = User.Active,
                };
                return await _manager.AddAsync (_Table,_User);
            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
            apiResponse.message = "Invalid Class";
            return apiResponse;
        }

        public async Task<ApiResponse> ProcessPut(object _Usermodel, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse ();
            var User = (UsersUpdateModel) _Usermodel;
            if (_manager != null) {
                var _Table = new User {
                    Id = User.Id,
                     Code = User.Code,
                    FirstName = User.FirstName ,
                    LastName =  User.LastName ,
                    NormalizedName = User.FirstName + " " + User.LastName ,
                    TenantsCheck = User.TenantsCheck,
                    Contact = User.Contact,
                    CNIC = User.CNIC,
                    PermitForm = User.PermitForm,
                    PermitTo = User.PermitTo,
                    HashPassword =  SecurityHelper.EncryptString("1234567890123456",User.HashPassword),
                    Email = User.Email,
                    RoleId = User.RoleId,
                    Type = User.Type,
                    Active = User.Active,
                };
                return await _manager.UpdateAsync (_Table,_User);

            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
            apiResponse.message = "Invalid Class";
            return apiResponse;
        }

        public async Task<ApiResponse> ProcessDelete(Guid _Id, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse ();
            if (_manager != null) {
                return await _manager.DeleteAsync (_Id,_User);
            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
            apiResponse.message = "Invalid Class";
            return apiResponse;
        }
    }
}