using ESA.Processor;
using ESA.Views.Shared;
using ESA.Views.StudentMaster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESA.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class CourseController : ControllerBase
{

    private readonly IProcessor<CourseBaseModel> _IProcessor;
    public CourseController(IProcessor<CourseBaseModel> IProcessor)
    {
        _IProcessor = IProcessor;
    }




    [HttpGet]
    [Route("GetCourse")]
    public async Task<IActionResult> GetCourse([FromHeader] Guid _MenuId)
    {
        try
        {
            var result = await _IProcessor.ProcessGet(_MenuId,User);
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



    [HttpGet]
    [Route("GetCourseById")]
    public async Task<IActionResult> GetCourseById(Guid _MenuId, [FromHeader] Guid _Id)
    {
        try
        {
            var result = await _IProcessor.ProcessGetById(_Id, _MenuId, User);
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



    [HttpPost]
    [Route("AddCourse")]
    public async Task<ActionResult> AddCourse(CourseAddModel _coursemodel)
    {
        try
        {
            var result = await _IProcessor.ProcessPost(_coursemodel, User);
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

    [HttpPut]
    [Route("UpdateCourse")]
    public async Task<ActionResult> UpdateCourse(CourseUpdateModel _coursemodel)
    {
        try
        {
            var result = await _IProcessor.ProcessPut(_coursemodel, User);
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

    [HttpDelete]
    [Route("DeleteCourse")]
    public async Task<ActionResult> DeleteCourse([FromHeader] Guid Id)
    {
        try
        {
            var result = await _IProcessor.ProcessDelete(Id, User);
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
