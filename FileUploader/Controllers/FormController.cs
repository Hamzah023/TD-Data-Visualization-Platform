using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileUploader.Controllers
{
    [ApiController] //what does this do? it is a decorator that tells the compiler that this class is an API controller, an API controller is a class that derives from ControllerBase and is used to handle web API requests
    [Route("Form")]
    public class FormController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            Console.WriteLine("THIS IS A SIGN OH MY GOD");
            Console.WriteLine("get request sent");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html");
            return PhysicalFile(filePath, "text/html");
        }

        [HttpPost]
        [Route("Form/Login2")]
        public async Task<IActionResult> Login2(string username, string password)
        {
            Console.WriteLine("post request sent");
            if (username != null && password != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                Console.WriteLine("post request sent and configured");
                return Ok(new { redirectUrl = Url.Action("Index", "Home") });
            }

            return BadRequest(new { message = "Invalid username or password" });
        }
    }
}

//take the username and password
//perform a search in the database using LINQ or sql or something
//if the username and password match a table, file a claim and authenticate using cookies
//then sign in user
// if not then ViewData["Error"] = invalid user or pass and return view();

//first need to set up database