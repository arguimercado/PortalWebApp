using Microsoft.AspNetCore.Mvc;

namespace WebApp.Client.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssetController : ControllerBase
{

    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Hello World");
    }
}
