using ESA.Layers.ContextLayer;
using ESA.Model;
using ESA.Shared;


namespace ESA.Repository
{
    public interface IDashboardServiceRepository
    {
        //LOV's

        Task<ApiResponse> SampleDashboardInfo(string? _Search);


    }

    public class DashboardServiceRepository : IDashboardServiceRepository
    {
        private readonly AppDBContext _context;
        private readonly Algorithms _algorithm;

        public DashboardServiceRepository(AppDBContext context, Algorithms algorithm)
        {
            _context = context;
            _algorithm = algorithm;
        }

        public async Task<ApiResponse> SampleDashboardInfo(string? _Search)
        {
            var apiResponse = new ApiResponse();
            try
            {
                //get data from table   
                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.message = "Success";
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