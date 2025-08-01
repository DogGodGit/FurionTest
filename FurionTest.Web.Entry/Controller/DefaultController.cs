using Microsoft.AspNetCore.Mvc;

namespace WebApplication1;

[ApiController]
[Route("[controller]")]
public class DefaultController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Furion 集成测试";
    }
}
