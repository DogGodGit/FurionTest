namespace FurionTest.Api;

[ApiController]
[ApiDescriptionSettings("Test", Tag = "Test")]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    // GET: api/Test
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "GET 请求成功" });
    }

    // GET: api/Test/5
    [HttpGet("{id}/{name}")]
    public IActionResult Get(int id, string name)
    {
        return Ok(new { message = $"GET 请求成功, id = {id}" });
    }

    [HttpGet("user/")]
    public IActionResult Get([FromQuery] User u)
    {
        return Ok(new { message = $"GET 请求成功, id = {u.Id}" });
    }

    // POST: api/Test
    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
        return Ok(new { message = "POST 请求成功", data = user });
    }

    // PUT: api/Test/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] User user)
    {
        return Ok(new { message = $"PUT 请求成功, id = {id}", data = user });
    }

    // DELETE: api/Test/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok(new { message = $"DELETE 请求成功, id = {id}" });
    }

    // POST: api/Test/upload
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { message = "未选择文件或文件为空" });
        }

        var filePath = Path.Combine(Path.GetTempPath(), file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { message = "文件上传成功", fileName = file.FileName, filePath });
    }
}