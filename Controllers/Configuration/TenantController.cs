using payday_server.Layers.ContextLayer;
using payday_server.Model;
using payday_server.Processor;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using payday_server.Views.Shared;

namespace payday_server.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]

public class TenantController : ControllerBase
{
    private readonly IProcessor<TenantBaseModel> _IProcessor;
    public TenantController(IProcessor<TenantBaseModel> IProcessor)
    {
        _IProcessor = IProcessor;
    }

    [HttpGet]
    [Route("GetTenant")]
    public async Task<IActionResult> GetTenant([FromHeader] Guid _MenuId){
        try {
            var result = await _IProcessor.ProcessGet (_MenuId, User);
            return Ok(result);
        } 
        catch (Exception e) {
            string innerexp = "";
            if (e.InnerException != null) {
                innerexp = " Inner Error : " + e.InnerException.ToString ();
            }
            return BadRequest (e.Message.ToString () + innerexp);
        }
    }

    [HttpGet]
    [Route("GetTenantById")]
    public async Task<ActionResult> GetTenantById(Guid _MenuId,[FromHeader] Guid _Id){
         try {
            var result = await _IProcessor.ProcessGetById (_Id,_MenuId,User);
            return Ok (result);
        } 
        catch (Exception e) {
            string innerexp = "";
            if (e.InnerException != null) {
                innerexp = " Inner Error : " + e.InnerException.ToString ();
            }
            return BadRequest (e.Message.ToString () + innerexp);
        }
    }


    [HttpPost]
    [Route("AddTenant")]
    public async Task<ActionResult> AddTenant(TenantAddModel Tenant){
        try {
                var result = await _IProcessor.ProcessPost (Tenant, User);
                return Ok (result);
            } 
        catch (Exception e) {
            string innerexp = "";
            if (e.InnerException != null) {
                innerexp = " Inner Error : " + e.InnerException.ToString ();
            }
            return BadRequest (e.Message.ToString () + innerexp);
        }
    }

    [HttpPut]
    [Route("UpdateTenant")]
    public async Task<ActionResult> UpdateTenant(TenantUpdateModel Tenant){
        try {
                var result = await _IProcessor.ProcessPut (Tenant, User);
                return Ok (result);

            } 
        catch (Exception e) {
            string innerexp = "";
            if (e.InnerException != null) {
                innerexp = " Inner Error : " + e.InnerException.ToString ();
            }
            return BadRequest (e.Message.ToString () + innerexp);
        }
    }

    [HttpDelete]
    [Route("DeleteTenant")]
    public async Task<ActionResult> DeleteTenant([FromHeader] Guid Id){
        try {
            var result = await _IProcessor.ProcessDelete (Id, User);
            return Ok (result);
        } 
        catch (Exception e) {
            string innerexp = "";
            if (e.InnerException != null) {
                innerexp = " Inner Error : " + e.InnerException.ToString ();
            }
            return BadRequest (e.Message.ToString () + innerexp);
        }
    }

}
