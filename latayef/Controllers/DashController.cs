using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Project.Controllers
{
    public class DashController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Admins()
        {
            return View();
        }
        public IActionResult Orders()
        {
            return View();
        }
        public IActionResult Products()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Testio()
        {
            return View();
        }
    }
}
