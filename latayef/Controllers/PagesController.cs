﻿using Ecommerce_Project.ViewModels;
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

        private readonly UserManager<User> _userManager;
        public PagesController(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
       
        
        public async Task<IActionResult> Index()
        {
            IndexPageModel indexPageModel = new IndexPageModel();
            indexPageModel.testimonials =  await _context.Testimonials
                                                  .Where(t => t.IsApproved ==true)
                                                  
                                                  .ToListAsync();
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
            List<Product> products = await _context.Products.ToListAsync();

            ProductCategoryViewModel viewModel = new ProductCategoryViewModel
            {
                Categories = categories,
                Products = products
            };

            return View(viewModel);
        }

        public IActionResult wishList() { return View(); }

        
        public async Task<IActionResult> profile() {

            User user = await _userManager.Users.Include(usr => usr.Orders).FirstOrDefaultAsync(usr => usr.Id == _userManager.GetUserId(User));
             
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
                  orders = user.Orders
                 
                  
            };
			return View(UserProfile);
        }
    }
}