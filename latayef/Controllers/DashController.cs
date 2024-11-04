using Ecommerce_Project.ViewModels;
using latayef.Data;
using latayef.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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






        public async Task<IActionResult> PendingTestimonials()
        {
            var pendingTestimonials = await _context.Testimonials
                                                   .Where(t => t.IsApproved == null) // Pending testimonials
                                                   .Include(t => t.User)
                                                   .ToListAsync();
            return View(pendingTestimonials);
        }

        // Approve Testimonial
        [HttpPost]
        public async Task<IActionResult> ApproveTestimonial(int id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }

            testimonial.IsApproved = true;
            _context.Testimonials.Update(testimonial);
            await _context.SaveChangesAsync();

            return RedirectToAction("PendingTestimonials");
        }

        // Reject Testimonial
        [HttpPost]
        public async Task<IActionResult> RejectTestimonial(int id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }

            testimonial.IsApproved = false;
            _context.Testimonials.Update(testimonial);
            await _context.SaveChangesAsync();

            return RedirectToAction("PendingTestimonials");
        }


    }
}
