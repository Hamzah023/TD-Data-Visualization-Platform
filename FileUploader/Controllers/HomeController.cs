using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FileUploader.Models;
using ExcelDataReader;
using System.Data;

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

    public IActionResult ExcelFileReader()
    {
        return View();
    }

    public IActionResult TextFileReader()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ViewData["Message"] = "Please select a file to upload";
            return View("TextFileReader");
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

            return View("TextFileReader");
        }
        catch (Exception ex)
        {
            ViewData["Error" +
                "Message"] = $"Error uploading file: {ex.Message}";
            return View("TextFileReader");
        }
    }


    [HttpPost]
    public async Task<IActionResult> ExcelFileReader(IFormFile file)
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        if (file == null || file.Length == 0)
        {
            return Json(new { success = false, message = "Please select a file to upload." });
        }

        var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "ExcelUploads");

        if (!Directory.Exists(uploadDirectory))
        {
            Directory.CreateDirectory(uploadDirectory);
        }

        var filePath = Path.Combine(uploadDirectory, file.FileName);

        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var excelData = new List<List<object>>();
            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            var rowData = new List<object>();

                            for (int column = 0; column < reader.FieldCount; column++)
                            {
                                rowData.Add(reader.GetValue(column));
                            }
                            excelData.Add(rowData);
                        }
                    } while (reader.NextResult());
                }
            }

            return Json(new { success = true, excelData });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = $"Error uploading file: {ex.Message}" });
        }
    }
}
