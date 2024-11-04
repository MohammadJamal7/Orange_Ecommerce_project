using Ecommerce_Project.ViewModels;
using latayef.Data;
using latayef.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Collections.Immutable;
using System.Runtime.InteropServices;

namespace latayef.Controllers
{
    public class PagesController : Controller
    {

        private readonly ApplicationContext _context;
<<<<<<< HEAD
        private readonly UserManager<User> _userManager;
        public PagesController(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
=======
        public PagesController(ApplicationContext context)
        {
            _context = context;
>>>>>>> 446c9ae5bd4c662a4c25b240455339df8bacdd92
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

        public IActionResult cart()
        {
            return View();
        }

        public IActionResult checkout()
        {
            return View();
        }


        public IActionResult contact()
        {

            return View();
        }

        public IActionResult productSingle()
        {
            return View();
        }

        public async Task<IActionResult> shop()
        {
            List<Category> categories = await _context.Categories.ToListAsync();

            return View(categories);
        }
        public IActionResult wishList() { return View(); }

        
        public async Task<IActionResult> profile() {

            User user = await _userManager.GetUserAsync(User);
             
            if (user ==null)
            {
                return RedirectToAction("Login", "Account");
            }

           
            ProfileViewModle UserProfile = new ProfileViewModle{ 
                  Name = user.Name,
                  Email = user.Email,
                  PhoneNumber = user.PhoneNumber,
                  Address = user.Address,
                  City = user.City,
                  State = user.State,
                  Pasword = user.PasswordHash
                 
                  
            };
			return View(UserProfile);
        }
    }
}