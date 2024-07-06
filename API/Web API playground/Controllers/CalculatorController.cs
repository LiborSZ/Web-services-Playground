using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly CalculatorServiceClient _client;

    public CalculatorController(CalculatorServiceClient client)
    {
        _client = client;
    }

    [HttpGet("add")]
    public IActionResult Add(int a, int b)
    {
        int result = _client.Add(a, b);
        return Ok(result);
    }
}