using payday_server.Repository;
using payday_server.Views.Service;
using payday_server.Views.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace payday_server.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class ConfigurationLovServiceController : ControllerBase
{
   
    private readonly IUserSetupServiceRepository IUserSetupServiceRepository;
    public ConfigurationLovServiceController(IUserSetupServiceRepository _IUserSetupServiceRepository)
    {
        IUserSetupServiceRepository = _IUserSetupServiceRepository;
    }

    
    

    [HttpGet]
    [Route("GetUserRoleLov")]
    public async Task<IActionResult> GetUserRoleLov(string? Search){
        try {
            var result = await IUserSetupServiceRepository.GetUserRolesLovAsync (Search);
             if (result == null) {
                return NotFound ();
            }
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

    
   
    [HttpGet]
    [Route("GetUserByRoleLov")]
    public async Task<IActionResult> GetUserByRoleLov([FromHeader] Guid _Id, string? Search){
        try {
            var result = await IUserSetupServiceRepository.GetUserByRoleLovAsync (_Id,Search);
             if (result == null) {
                return NotFound ();
            }
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

    

    [HttpGet]
    [Route("GetMenuOrMenuPermissionUserWise")]
    public async Task<IActionResult> GetMenuOrMenuPermissionUserWise([FromHeader] Guid _roleId,[FromHeader] Guid _userId ){
        try {

            MenuPermissionPayLoadServicesModel _model= new MenuPermissionPayLoadServicesModel();
            _model.RoleId = _roleId;
            _model.UserId = _userId;
            var result = await IUserSetupServiceRepository.GetMenuOrMenuPermissionUserWiseAsync (_model,User);
             if (result == null) {
                return NotFound ();
            }
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
    [Route("UpdateUserPermissions")]
    public async Task<IActionResult> UpdateUserPermissions(UserRolePermissionAddModel model){
        try {

            var result = await IUserSetupServiceRepository.UpdateUserRolePermissionAsync (model,User);
             if (result == null) {
                return NotFound ();
            }
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
