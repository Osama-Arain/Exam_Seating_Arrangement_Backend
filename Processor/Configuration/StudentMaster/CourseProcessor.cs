using ESA.Layers.ContextLayer;
using ESA.Manager;
using ESA.Model;
using ESA.Shared;
using ESA.Views.Shared;
using System.Security.Claims;
using ESA.Views.StudentMaster;

namespace ESA.Processor.Payroll.Setup
{
    public class CourseProcessor : IProcessor<CourseBaseModel>
    {
        private AppDBContext _context;
        private IManager? _manager;
        public CourseProcessor(AppDBContext context)
        {
            _context = context;
            _manager = Builder.MakeManagerClass(Enums.ModuleClassName.Course, _context);
        }

        public async Task<ApiResponse> ProcessGet(Guid MenuId, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            if (_manager != null)
            {
                var response = await _manager.GetDataAsync(_User);
                if (Convert.ToInt32(response.statusCode) == 200)
                {
                    var _Table = response.data as IEnumerable<Course>;
                    var apiResponseUser = await SecurityHelper.UserMenuPermissionAsync(MenuId, _User);
                    if (apiResponseUser.statusCode.ToString() != StatusCodes.Status200OK.ToString()) { return apiResponseUser; }
                    
                    var result = (from ViewTable in _Table
                                  select new CourseViewModel
                                  {
                                      Id = ViewTable.Id,
                                      Code = ViewTable.Code,
                                      CourseID = ViewTable.CourseID,
                                      Name = ViewTable.Name,
                                      Active = ViewTable.Active
                                  }).ToList();
                    response.data = result;
                }
                return response;
            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
            apiResponse.message = "Invalid Class";
            return apiResponse;
        }

        public async Task<ApiResponse> ProcessGetById(Guid _Id, Guid _MenuId, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            if (_manager != null)
            {
                var response = await _manager.GetDataByIdAsync(_Id, _User);
                if (Convert.ToInt32(response.statusCode) == 200)
                {
                    var _Table = response.data as Course;
                    var _ViewModel = new CourseViewByIdModel
                    {
                        Id = _Table.Id,
                        Code = _Table.Code,
                        CourseID = _Table.CourseID,
                        Name = _Table.Name,
                        Active = _Table.Active
                    };
                    response.data = _ViewModel;
                }
                return response;
            }
            else
            {
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = "Manager not Found";
                return apiResponse;
            }
        }

        public async Task<ApiResponse> ProcessPost(object _CourseModel, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            var Course = (CourseAddModel)_CourseModel;

            if (_manager != null)
            {
                var _Table = new Course
                {
                    Name = Course.Name,
                    CourseID = Course.CourseID,
                    Active = Course.Active,
                };
                return await _manager.AddAsync(_Table, _User);
            }
            else
            {
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = "Manager not Found!";
                return apiResponse;
            }

        }

        public async Task<ApiResponse> ProcessPut(object _CourseModel, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            var Course = (CourseUpdateModel)_CourseModel;
            if (_manager != null)
            {
                var _Table = new Course
                {
                    Id = Course.Id,
                    Name = Course.Name,
                    CourseID = Course.CourseID,
                    Active = Course.Active,
                };
                return await _manager.UpdateAsync(_Table, _User);

            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
            apiResponse.message = "Invalid Class";
            return apiResponse;
        }

        public async Task<ApiResponse> ProcessDelete(Guid _Id, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            if (_manager != null)
            {
                return await _manager.DeleteAsync(_Id, _User);
            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
            apiResponse.message = "Invalid Class";
            return apiResponse;
        }
    }
}