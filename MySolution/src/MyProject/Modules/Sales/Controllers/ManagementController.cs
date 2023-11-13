using Microsoft.AspNetCore.Mvc;

namespace MyProject.Areas.Sales.Controllers
{
    [Area("Sales")]
    public class ManagementController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
