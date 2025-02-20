using Ecommerce_Project.ViewModels;
using latayef.Data;
using latayef.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationContext _context;

        public CategoryController(IWebHostEnvironment webHostEnvironment, ApplicationContext context)
        {
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
                string uniqueFileName = null;

                // Save the uploaded image to wwwroot/images/categories
                if (model.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/categories");

                    // Ensure the directory exists
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generate a unique file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save the file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(fileStream);
                    }
                }

                // Create a new Category and save the file path to ImagePath
                Category category = new Category
                {
                    Name = model.Name,
                    ImagePath = uniqueFileName != null ? "/images/categories/" + uniqueFileName : "/images/default.jpg" // Set default image if no file uploaded
                };



                // Add to context and save
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                // Redirect to the Index or any other appropriate action
                return RedirectToAction("Index", "Pages");
            
        }


        // GET: CategoriesController/GetCategory/5
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new { c.Id, c.Name })
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound();
            }

            return Json(category);
        }



        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Edit(int id, CategoryViewModel model)
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            // Update the name
            category.Name = model.Name;

            // Check if a new image is provided
            if (model.ImageFile != null)
            {
                // Delete old image if exists
                if (!string.IsNullOrEmpty(category.ImagePath))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, category.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Save new image
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/categories");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }

                category.ImagePath = "/images/categories/" + uniqueFileName;
            }

            // Update the category in the database
            _context.Update(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Dash"); // Redirect to the categories page
        }

        // GET: CategoryController/Delete/5



        // POST: CategoryController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int categoryId)
        {
            try
            {
                // Find the category by ID
                var category = await _context.Categories.FindAsync(categoryId);
                if (category == null)
                {
                    return NotFound(); // Return 404 if category doesn't exist
                }

                // Remove the category from the database
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync(); // Save changes to the database

                // Return success response
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Log exception and return error response
                return StatusCode(500, new { success = false, message = "Internal server error: " + ex.Message });
            }
        }


    }
}
