using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApplicationController: ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        return base.Ok(value);
    }
    
    
}