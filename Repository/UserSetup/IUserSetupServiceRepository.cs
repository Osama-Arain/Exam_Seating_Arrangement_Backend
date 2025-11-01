using System.Reflection;
using System;
using ESA.Layers.ContextLayer;
using ESA.Model;
using ESA.Shared;
using Microsoft.EntityFrameworkCore;
using ESA.Views.Shared;
using ESA.Views.Service;
using System.Security.Claims;

namespace ESA.Repository
{
    public interface IUserSetupServiceRepository
    {
        //LOV's
        Task<ApiResponse> GetUserRolesLovAsync(string? _Search);
        Task<ApiResponse> GetUserByRoleLovAsync(Guid _role, string? _Search);
        Task<ApiResponse> GetMenuOrMenuPermissionUserWiseAsync(object model, ClaimsPrincipal _User);
        Task<ApiResponse> UpdateUserRolePermissionAsync(object model, ClaimsPrincipal _User);
    }

    public class UserSetupServiceRepository : IUserSetupServiceRepository
    {
        private readonly AppDBContext _context;

        public UserSetupServiceRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> GetUserRolesLovAsync(string? _Search)
        {
            var apiResponse = new ApiResponse();
            try
            {

                var _userroles = await (from userroles in _context.UserRoles
                .Where(a => a.Action != Enums.Operations.D.ToString() && a.Active == true && string.IsNullOrEmpty(_Search) ? true : a.Role.Contains(_Search))
                                        select new ListOfViewServicesModel
                                        {
                                            Id = userroles.Id,
                                            Name = userroles.Role
                                        }).OrderBy(o => o.Name).ToListAsync();

                if (_userroles == null)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }
                if (_userroles.Count == 0)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }

                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.data = _userroles;
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

        public async Task<ApiResponse> GetUserByRoleLovAsync(Guid _role, string? _Search)
        {
            var apiResponse = new ApiResponse();
            try
            {

                var _User = await (from user in _context.Users
                .Where(a => a.RoleId == _role && a.Action != Enums.Operations.D.ToString() && a.Active == true && string.IsNullOrEmpty(_Search) ? true : a.NormalizedName.Contains(_Search))
                                   select new ListOfViewServicesModel
                                   {
                                       Id = user.Id,
                                       Name = "[" + user.Code + "] - " + user.NormalizedName
                                   }).OrderBy(o => o.Name).ToListAsync();

                if (_User == null)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }
                if (_User.Count == 0)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }

                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.data = _User;
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


        public async Task<ApiResponse> GetMenuOrMenuPermissionUserWiseAsync(object model, ClaimsPrincipal _User)
        {
            var apiResponse = new ApiResponse();
            try
            {

                var _model = (MenuPermissionPayLoadServicesModel)model;
                var _Table = await _context.UserRoles.Where(a => a.Id == _model.RoleId).FirstOrDefaultAsync();
                if (_Table == null)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Role not found";
                    return apiResponse;
                }
                List<UsersPermissions> _TableRolePermission = (List<UsersPermissions>)await _context.UsersPermissions.Where(x => x.RoleId == _model.RoleId && x.UserId == _model.UserId).ToListAsync();
                List<MenuPermissionViewModel> _MenuPerView = new List<MenuPermissionViewModel>();

                //var _Menu = await _context.MenuSubCategories.Include(c => c.MenuModule).Where(x => x.Active == true  && x.MenuModule.Active == true && x.Action != Enums.Operations.D.ToString ()).ToListAsync ();
                //foreach (var item in _Menu) {

                //    var _Permission = _TableRolePermission.Where (x => x.MenuId == item.Id).FirstOrDefault ();
                //    bool _View = false, _Insert = false, _Update = false, _Delete = false, _Print = false, _Check = false, _Approve = false;
                //    if (_Permission != null) {
                //        _View = _Permission.Show_Permission;
                //        _Insert = _Permission.Insert_Permission;
                //        _Update = _Permission.Update_Permission;
                //        _Delete = _Permission.Delete_Permission;
                //        _Print = _Permission.Print_Permission;
                //        _Check = _Permission.Check_Permission;
                //        _Approve = _Permission.Approve_Permission;

                //    }
                //    _MenuPerView.Add (new MenuPermissionViewModel { ModuleId = item.MenuModuleId, ModuleName = item.MenuModule.Name, MenuId = item.Id, MenuName = item.Name.Trim (), MenuAlias = item.Alias.Trim (), Insert_Permission = _Insert, View_Permission = _View, Update_Permission = _Update, Delete_Permission = _Delete, Print_Permission = _Print, Check_Permission = _Check, Approve_Permission = _Approve });
                //}
                var _ViewModel = new MenuPermissionViewRoleModel
                {
                    Id = _Table.Id,
                    Name = _Table.Role,
                    menuPerViews = _MenuPerView,
                };
                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.data = _ViewModel;
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

        public async Task<ApiResponse> UpdateUserRolePermissionAsync(object model, ClaimsPrincipal _User)
        {
            var apiResponse = new ApiResponse();
            try
            {

                var _request = (UserRolePermissionAddModel)model;

                var UserRolePermissions = new List<UsersPermissions>();

                foreach (var item in _request.UserRolePermissions)
                {
                    var _Table = new UsersPermissions
                    {
                        Show_Permission = item.View_Permission,
                        Insert_Permission = item.Insert_Permission,
                        Update_Permission = item.Update_Permission,
                        Delete_Permission = item.Delete_Permission,
                        Print_Permission = item.Print_Permission,
                        Check_Permission = item.Check_Permission,
                        Approve_Permission = item.Approved_Permission,
                        RoleId = item.RolesId,
                        UserId = item.UserId
                    };
                    UserRolePermissions.Add(_Table);
                }

                var _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();

                var _model = (List<UsersPermissions>)UserRolePermissions;

                var user = await _context.Users.Where(x => x.Id == _model[0].UserId && x.RoleId == _model[0].RoleId).FirstOrDefaultAsync();
                var _modelDetails = _context.UsersPermissions.Where(a => a.RoleId == _model[0].RoleId && a.UserId == _model[0].UserId).ToList();
                if (_modelDetails != null)
                {
                    foreach (var item in _modelDetails)
                    {
                        _context.UsersPermissions.Remove(item);
                    }
                    _context.SaveChanges();
                }
                foreach (var item in _model)
                {
                    item.UserIdUpdate = Guid.Parse(_UserId);
                    item.UpdateDate = DateTime.Now;
                    item.Action = Enums.Operations.E.ToString();
                    item.Type = Enums.Operations.S.ToString();
                    await _context.UsersPermissions.AddAsync(item);
                }
                await _context.SaveChangesAsync();

                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.message = "Permissions Saved for User " + user.NormalizedName;
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

    }
}