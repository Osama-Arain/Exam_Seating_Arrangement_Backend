using Microsoft.AspNetCore.Mvc;
using payday_server.Model;
using payday_server.Repository.Admin;
using payday_server.Repository.Attendance;

using payday_server.Processor;
using payday_server.Repository;
using payday_server.Views.Shared;
using payday_server.Repository.Attendance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace payday_server.Controllers.Admin
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class AdminAccessController : ControllerBase
    {
        private readonly IAdminServiceRepository IAdminServiceRepository;

        public AdminAccessController(IAdminServiceRepository _IAdminServiceRepository)
        {
            IAdminServiceRepository = _IAdminServiceRepository;
        }

        
        //[HttpGet]
        //[Route("FlushHolidays")]
        //public async Task<IActionResult> FlushHolidays()
        //{
        //    try
        //    {
        //        var result = await IAdminServiceRepository.FlushHolidaysAsync();
        //        if (result == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        string innerexp = "";
        //        if (e.InnerException != null)
        //        {
        //            innerexp = " Inner Error : " + e.InnerException.ToString();
        //        }
        //        return BadRequest(e.Message.ToString() + innerexp);
        //    }
        //}


        //[HttpGet]
        //[Route("FlushRosters")]
        //public async Task<IActionResult> FlushRosters()
        //{
        //    try
        //    {
        //        var result = await IAdminServiceRepository.FlushRostersAsync();
        //        if (result == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        string innerexp = "";
        //        if (e.InnerException != null)
        //        {
        //            innerexp = " Inner Error : " + e.InnerException.ToString();
        //        }
        //        return BadRequest(e.Message.ToString() + innerexp);
        //    }
        //}


    }
}
