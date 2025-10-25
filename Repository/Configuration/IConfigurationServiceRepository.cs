using payday_server.Layers.ContextLayer;
using payday_server.Model;
using payday_server.Shared;
using Microsoft.EntityFrameworkCore;
using payday_server.Views.Service;

namespace payday_server.Repository
{
    public interface IConfigurationServiceRepository
    {
        //LOV's
       
        Task<ApiResponse> GetDepartmentLovAsync(string? _Search);
        
    }

    public class ConfigurationServiceRepository : IConfigurationServiceRepository
    {
        private readonly AppDBContext _context;
        public ConfigurationServiceRepository(AppDBContext context)
        {
            _context = context;
        }
     
        public async Task<ApiResponse> GetDepartmentLovAsync(string? _Search)
        {
            var apiResponse = new ApiResponse();
            try
            {

                //var _department = await (from department in _context.Departments
                //.Where(a => a.Action != Enums.Operations.D.ToString() && a.Active == true && string.IsNullOrEmpty(_Search) ? true : a.Name.Contains(_Search))
                //                         select new ListOfViewServicesModel
                //                         {
                //                             Id = department.Id,
                //                             Name = department.Name
                //                         }).OrderBy(o => o.Name).ToListAsync();
                var _department = "".ToList();

                if (_department == null)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }
                if (_department.Count == 0)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }

                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.data = _department;
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
