using Microsoft.AspNetCore.Mvc;

namespace AnimalTrack.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Foo()
    {
        return "woop";
    }
}