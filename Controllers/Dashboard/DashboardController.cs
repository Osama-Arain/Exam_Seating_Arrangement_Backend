using payday_server.Layers.ContextLayer;
using payday_server.Model;
using payday_server.Processor;
using payday_server.Repository;
using payday_server.Views.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace payday_server.Controllers;

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
