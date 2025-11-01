using ESA.Layers.ContextLayer;
using ESA.Model;
using ESA.Processor;
using ESA.Repository;
using ESA.Views.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESA.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class DashboardController : ControllerBase
{
     private readonly IDashboardServiceRepository IDashboardServiceRepository;
    public DashboardController(IDashboardServiceRepository _IDashboardServiceRepository)
    {
        IDashboardServiceRepository = _IDashboardServiceRepository;
    }

    
}
