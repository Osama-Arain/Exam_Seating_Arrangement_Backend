using payday_server.Layers.ContextLayer;
using payday_server.Repository;
using payday_server.Repository.Report;
using payday_server.Views.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace payday_server.Controllers.Report
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class ReportLovServiceController : ControllerBase
    {
        private readonly IReportServiceRepository IReportServiceRepository;
        public ReportLovServiceController(IReportServiceRepository _IReportServiceRepository)
        {
            IReportServiceRepository = _IReportServiceRepository;
        }

        [HttpGet]
        [Route("GetDateWiseShiftReport")]
        public async Task<IActionResult> GetDateWiseShiftReport([FromHeader] DateTime dateFrom, [FromHeader] DateTime dateTo, [FromHeader] string EmployeeID, [FromHeader] Guid _MenuId)
        {
            try
            {
                var result = await IReportServiceRepository.GetDateWiseShiftReportAsync(dateFrom, dateTo, EmployeeID, _MenuId, User);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                return BadRequest(e.Message.ToString() + innerexp);
            }
        }

       
    }
}
