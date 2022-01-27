using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ApiTest.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly string _assemblyInfo = $" Assembly: {typeof(ObjectResultExecutor).Assembly.Location}";

    [HttpGet("1")]
    public IActionResult Test1()
    {
        return Problem("Test1 ✅ - Problem(string) without ProducesAttribute => returns application/problem+json." + _assemblyInfo);
    }

    [Produces("application/json")]
    [HttpGet("2")]
    public IActionResult Test2()
    {
        return Problem("Test2 ❌ - Problem(string) with [Produces(\"application/json\")] => incorrectly returns application/json instead of application/problem+json." + _assemblyInfo);
    }

    [Produces("application/json", "application/problem+json")]
    [HttpGet("3")]
    public IActionResult Test3()
    {
        return Problem("Test3 ❌ - Problem(string) with [Produces(\"application/json\", \"application/problem+json\")] => incorrectly returns application/json instead of application/problem+json." + _assemblyInfo);
    }

    [Produces("application/problem+json", "application/json")]
    [HttpGet("4")]
    public IActionResult Test4()
    {
        return Problem("Test4 ❌ - Problem(string) with [Produces(\"application/problem+json\", \"application/json\")] => returns content-type application/problem+json, but then Ok(string) also returns application/problem+json." + _assemblyInfo);
    }
}
