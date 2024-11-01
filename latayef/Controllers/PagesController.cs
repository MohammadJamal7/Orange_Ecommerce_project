using Microsoft.AspNetCore.Mvc;

namespace latayef.Controllers
{
    public class PagesController : Controller
    {
        public IActionResult Index()
        {
            return View();

        }
        public IActionResult about()
        {
            return View();
        }

        public IActionResult blog()
        {
            return View();
        }


        public IActionResult blogSingle()
        {
            return View();
        }

        public IActionResult cart() {
            return View();
        }

        public IActionResult checkout() {
            return View();
        }


        public IActionResult contact() {

            return View();
        }

        public IActionResult productSingle() {
            return View();
        }

        public IActionResult shop() {
            return View();
        }
        public IActionResult wishList() { return View(); }
    }
}