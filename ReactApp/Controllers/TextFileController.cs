using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // For IFormFile
using System.IO; // For StreamReader
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

[Route("api")]
//[EnableCors]
public class TextFileController : ControllerBase
{
    private readonly ILogger<TextFileController> _logger;

    public TextFileController(ILogger<TextFileController> logger)
    {
        _logger = logger;
    }

    [HttpPost("Upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        _logger.LogInformation("Upload called.");
        if (file == null || file.Length == 0)
        {
            _logger.LogInformation("No file uploaded.");
            return BadRequest("No file uploaded.");
        }

        string fileContent;
        _logger.LogInformation("File uploaded.");

        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            fileContent = await reader.ReadToEndAsync();
        }
        _logger.LogInformation("File read.");
        _logger.LogInformation("File Content: " + fileContent);  
        return Ok(new
        {
            fileName = file.FileName,
            fileContent = fileContent,
        });
    }
}