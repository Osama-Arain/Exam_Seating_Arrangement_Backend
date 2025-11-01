
using ESA.Layers.ContextLayer;
using ESA.Model;
using ESA.Model.Report;
using Microsoft.EntityFrameworkCore;
using ESA.Shared;
using ESA.Views.Report;
using ESA.Views.Shared;
using System.Security.Claims;
using Newtonsoft.Json;

namespace ESA.Repository.Report
{
   

    public interface IReportServiceRepository
    {
      
        // REPORTS
        Task<ApiResponse> GetDateWiseShiftReportAsync(DateTime dateFrom, DateTime dateTo, string EmployeeID, Guid _MenuId, ClaimsPrincipal _User);
      
    }

    public class ReportServiceRepository : IReportServiceRepository
    {
        private readonly AppDBContext _context;
        private readonly Algorithms _algorithm;
        private readonly IConfiguration _configuration;

        public ReportServiceRepository(AppDBContext context, Algorithms algo, IConfiguration configuration)
        {
            _context = context;
            _algorithm = algo;
            _configuration = configuration;
        }


        // REPORTS
        public async Task<ApiResponse> GetDateWiseShiftReportAsync(DateTime dateFrom, DateTime dateTo, string EmployeeID, Guid _MenuId, ClaimsPrincipal _User)
        {
            var apiResponse = new ApiResponse();
            var apiResponseUser = await SecurityHelper.UserMenuPermissionAsync(_MenuId, _User);
            if (apiResponseUser.statusCode.ToString() != StatusCodes.Status200OK.ToString()) { return apiResponseUser; }
            var _UserMenuPermissionAsync = (GetUserPermissionViewModel)apiResponseUser.data;
            if (_UserMenuPermissionAsync.View_Permission)
            {
                List<UserEventLogsViewModel> reportList = new List<UserEventLogsViewModel>();
                var startDate = dateFrom.Date;
                var tillDate = dateTo.Date.AddDays(1);
                try
                {
                    var result = " ".ToList(); /*_algorithm.AttendanceLogGenerate(startDate, tillDate, EmployeeID)*/;
                    apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                    apiResponse.data = result;
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
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
            apiResponse.message = $"Unauthorized request!";
            return apiResponse;

        }
      
    }
}
