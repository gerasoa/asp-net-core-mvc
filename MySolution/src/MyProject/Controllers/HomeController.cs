using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MyProject.Configuration;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiConfiguration ApiConfig;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IOptions<ApiConfiguration> apiConfiguration, IStringLocalizer<HomeController> localizer)
        {
            ApiConfig = apiConfiguration.Value;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var user = ApiConfig.UserKey;
            var secret = ApiConfig.UserSecret;
            var domain = ApiConfig.Domain;

            ViewData["Message"] = _localizer["Seja bem vindo!"];

            return View();
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        [Route("cookies")]
        public IActionResult Cookie()
        {
            var cookiesOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddYears(1)
            };

            Response.Cookies.Append("MyCookie", "dataCookie", cookiesOptions);

            return View();
        }


        [Route("test")]
        public IActionResult Test()
        {
            throw new Exception("Something is wrong!");

            return View("Index");
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelError = new ErrorViewModel();

            if(id == 500)
            {
                modelError.Message = "An error has occurred! Please try again later or contact our support.";
                modelError.Title = "An error has occurred!";
                modelError.ErrorCode = id;
            }
            else if(id ==404)
            {
                modelError.Message = "The page you are looking for does not exist! <br />If you have any questions contact our support";
                modelError.Title = "Page not found";
                modelError.ErrorCode = id;
            }
            else if (id ==403)
            {
                modelError.Message = "You’re not allowed to do this.";
                modelError.Title = "access denied";
                modelError.ErrorCode = id;
            }
            else
            {
                return StatusCode(500);
            }

            return View("Error", modelError);
        }
    }
}
