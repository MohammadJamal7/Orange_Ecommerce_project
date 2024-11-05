using Ecommerce_Project.ViewModels;
using latayef.Data;
using latayef.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc.Rendering;
=======
using Microsoft.CodeAnalysis.CSharp.Syntax;
>>>>>>> c20f83b450ff987bbca765e3110a2286747b0ab5
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Controllers
{
    public class DashController : Controller
    {
<<<<<<< HEAD

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
=======
		private readonly UserManager<User> _userManager;
		private readonly ApplicationContext _context;
>>>>>>> c20f83b450ff987bbca765e3110a2286747b0ab5


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

			return View(categoriesWithProductCount);
		}

		public async Task<IActionResult> Admins()
		{
			var usersInRole = await _userManager.GetUsersInRoleAsync("Customer");
			return View(usersInRole);
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
        public async Task<IActionResult> Testio()
        {
            var pendingTestimonials = await _context.Testimonials
                                                  .Where(t => t.IsApproved == null)
                                                  .Include(t => t.User)
                                                  .ToListAsync();
            return View(pendingTestimonials);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
           
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Json(new { success = true, userId = id });

            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }


            return Json(new { success = false, message = "Error deleting user." });

        }



        //public async Task<IActionResult> PendingTestimonials()
        //{

        //    return RedirectToAction("Testio" , pendingTestimonials);
        //}

        // Approve Testimonial
        [HttpPost]
        public async Task<IActionResult> ApproveTestimonial(int id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial != null)
            {
                testimonial.IsApproved = true;
                await _context.SaveChangesAsync();
                RedirectToAction("Testio", "Dash");
            }

            return RedirectToAction("Testio", "Dash");
        }
     

        // Reject Testiomonial
        [HttpPost]
        public async Task<IActionResult> RejectTestimonial(int id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial != null)
            {
                testimonial.IsApproved = false; // Or set a rejected flag if applicable
                await _context.SaveChangesAsync();
                RedirectToAction("Testio", "Dash");
            }

            return RedirectToAction("Testio", "Dash");
        }


    }
}
