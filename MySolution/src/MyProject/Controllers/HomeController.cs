using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyProject.Configuration;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiConfiguration ApiConfig;

        public HomeController(IOptions<ApiConfiguration> apiConfiguration)
        {
            ApiConfig = apiConfiguration.Value;            
        }

        public IActionResult Index()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var user = ApiConfig.UserKey;
            var secret = ApiConfig.UserSecret;
            var domain = ApiConfig.Domain;


            return View();
        }
    }
}
