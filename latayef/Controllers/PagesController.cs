using Ecommerce_Project.ViewModels;
using latayef.Data;
using latayef.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace latayef.Controllers
{
    public class PagesController : Controller
    {

        private readonly ApplicationContext _context;
        public PagesController(ApplicationContext context) { 
         _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IndexPageModel indexPageModel = new IndexPageModel();
            indexPageModel.testimonials = await _context.Testimonials.ToListAsync();
            indexPageModel.products = await _context.Products.ToListAsync();
            indexPageModel.categories = await _context.Categories.ToListAsync();
            return View(indexPageModel);

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

        public async Task<IActionResult> shop() {
            List<Category> categories = await _context.Categories.ToListAsync();

            return View(categories);
        }
        public IActionResult wishList() { return View(); }
    }
}