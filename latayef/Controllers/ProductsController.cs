using Ecommerce_Project.ViewModels;
using latayef.Data;
using latayef.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace Ecommerce_Project.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ApplicationContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: ProductsController
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return View(products);  
        }

        [HttpGet]
        public IActionResult FilterByCategory(int categoryId)
        {
            Console.WriteLine("FilterByCategory called with categoryId:", categoryId); // Debugging

            var products = categoryId == 0
                ? _context.Products.ToList()
                : _context.Products.Where(p => p.CategoryId == categoryId).ToList();

            if (!products.Any())
            {
                Console.WriteLine("No products found for category ID: " + categoryId);
            }
            else
            {
                Console.WriteLine($"Found {products.Count} products for category ID: {categoryId}");
            }

            return PartialView("~/Views/Shared/_ProductListPartial.cshtml", products);
        }


        // GET: ProductsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: ProductsController/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]

        public async Task<IActionResult> Create(productModel model)
        {

            if (true)
            {
                string uniqueFileName = null;

                if (model.iamgeFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/products");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.iamgeFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.iamgeFile.CopyToAsync(fileStream);
                    }
                }

                Product product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Discount = model.Discount,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    Size = model.Size,
                    ImgPath = "/images/products/" + uniqueFileName
                };

                _context.Products.Add(product);
                
                await _context.SaveChangesAsync();
                return RedirectToAction("Products", "Dash");
            }

            //ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name", model.CategoryId);
            //return View(model);
        }

        // GET: ProductsController/Edit/5
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products
                .Where(p => p.Id == id)
                .Select(p => new {
                    p.Id,
                    p.Name,
                    p.CategoryId,
                    p.Size,
                    p.Price,
                    p.Description,
                    ExistingImagePath = p.ImgPath
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return Json(product);
        }


        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name", model.CategoryId);
            var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                product.Name = model.Name;
                product.Price = model.Price;
                product.Category = model.Category;
                product.CategoryId = model.CategoryId;
                product.Description = model.Description;

                if (model.iamgeFile != null)
                {
                    if (!string.IsNullOrEmpty(model.ExistingImagePath))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, model.ExistingImagePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/products");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.iamgeFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.iamgeFile.CopyToAsync(fileStream);
                    }

                    product.ImgPath = "/images/products/" + uniqueFileName;
                }

                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Products", "Dash");
            

          
          
        }

        // GET: ProductsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(product.ImgPath))
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImgPath.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
