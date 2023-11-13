using Microsoft.AspNetCore.Mvc;

namespace MyProject.Areas.Products.Controllers
{
    [Area("Products")]
    public class ManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
