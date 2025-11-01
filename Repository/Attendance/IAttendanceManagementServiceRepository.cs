using ESA.Layers.ContextLayer;
using ESA.Model;
using ESA.Shared;
using ESA.Views.Shared;
using System.Security.Claims;

namespace ESA.Repository.Attendance
{
    public interface IAttendanceManagementServiceRepository
    {
        //LOV's
        Task<ApiResponse> GetMonthlyAttendanceAsync(Guid _MenuId, ClaimsPrincipal _User, string DateMonth);
 
    }


    public class AttendanceManagementServiceRepository : IAttendanceManagementServiceRepository
    {
        private readonly AppDBContext _context;
        public AttendanceManagementServiceRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> GetMonthlyAttendanceAsync(Guid _MenuId, ClaimsPrincipal _User, string DateMonth)
        {
            var apiResponse = new ApiResponse();
            var apiResponseUser = await SecurityHelper.UserMenuPermissionAsync(_MenuId, _User);
            List<UserEventLogsViewModel> reportList = new List<UserEventLogsViewModel>();
            if (apiResponseUser.statusCode.ToString() != StatusCodes.Status200OK.ToString()) { return apiResponseUser; }
            var _UserMenuPermissionAsync = (GetUserPermissionViewModel)apiResponseUser.data;
            if (_UserMenuPermissionAsync.View_Permission) 
            {
                try
                {
                   //get attendance from table
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
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
            apiResponse.message = $"Unauthorized request!";
            return apiResponse;
        }
    }
}
