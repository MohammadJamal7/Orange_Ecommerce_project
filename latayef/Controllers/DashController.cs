using Ecommerce_Project.ViewModels;
using latayef.Data;
using latayef.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Controllers
{
    public class DashController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;


        public DashController(UserManager<User> userManager, ApplicationContext context)
        {
            _userManager = userManager;
            _context = context;


        }
        public async Task<IActionResult> Index()
        {
            var categoriesWithProductCount = await _context.Categories
                .Select(c => new CategoryViewModel
                {
                    Category = c,
                    ProductCount = c.Products.Count(),
                    imagePath = c.ImagePath,

                })
                .ToListAsync();

            var categoryModel = new CategoryModel
            {
                categoryViewModels = categoriesWithProductCount,
            };

            return View(categoryModel);
        }

        public IActionResult Admins()
        {
            return View();
        }
        public IActionResult Orders()
        {
            return View();
        }
        public async Task<IActionResult> Products()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            var productModel = new ProductViewModel
            {
                Products = products,
            };
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            return View(productModel);
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
