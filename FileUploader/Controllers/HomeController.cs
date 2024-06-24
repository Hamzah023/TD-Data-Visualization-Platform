using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FileUploader.Models;

namespace FileUploader.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ViewData["Message"] = "Please select a file to upload";
            return View("Index");
        }

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", file.FileName);

        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileContents = await System.IO.File.ReadAllTextAsync(filePath);

            
            
            ViewData["SuccessMessage"] = "File uploaded successfully!";
            ViewData["FileContents"] = fileContents;

            return View("Index");
        }
        catch (Exception ex)
        {
            ViewData["ErrorMessage"] = $"Error uploading file: {ex.Message}";
            return View("Index");
        }

    }
}

