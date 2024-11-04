using Ecommerce_Project.ViewModels;
using latayef.Data;
using latayef.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Project.Controllers
{
    public class ReviewingController : Controller
    {
        private readonly ApplicationContext _context;

        public ReviewingController(ApplicationContext context)
        {
            _context = context;
           
        }
        
   
        [HttpPost]
        public async Task<IActionResult> Tesionmonial(IndexPageModel testio)
        {

            _context.Testimonials.Add(testio.testimonial);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index", "Pages"); 
            
           
        }

    }
}
