using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ReactApp.Controllers;

[ApiController]
[Route("api")]
public class TextFileController : ControllerBase
{
    

    private readonly ILogger<TextFileController> _logger;

    public TextFileController(ILogger<TextFileController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [Route("Upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        Console.WriteLine("Upload called.");
        if (file == null || file.Length == 0)
            {
                Console.WriteLine("No file uploaded.");
                return BadRequest("No file uploaded.");
            }

            string fileContent;
            Console.WriteLine("File uploaded.");
            
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                fileContent = await reader.ReadToEndAsync();
                
            }
            Console.WriteLine("File read.");
            return Ok(new
            {
                fileName = file.FileName,
                fileContent = fileContent
            });
    }
}

