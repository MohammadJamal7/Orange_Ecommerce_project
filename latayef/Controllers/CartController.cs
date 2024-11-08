using latayef.Data;
using latayef.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics; // Add this for debugging

namespace Ecommerce_Project.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        public CartController(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<CartItem> cartItems = await _context.CartItems.Include(c => c.Cart).Include(c => c.Product).ToListAsync();

            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            
            // Retrieve the product by ID
            var product = await _context.Products
                .Include(p => p.Category) // Include the Category
                .FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            // Retrieve or create the user's cart
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(item => item.Product)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = user.Id,
                    User = user,
                    Items = new List<CartItem>(),
                    ShippingAddress = "irbid"
                };
                _context.Carts.Add(cart); // Add new cart if it does not exist
                Debug.WriteLine("New cart created and added to context.");
            }

            // Check if the cart item already exists for this product
            var cartItem = cart.Items.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem != null)
            {
                // Update the quantity if the item exists in the cart
                cartItem.Quantity += quantity;
                Debug.WriteLine("Existing cart item found. Quantity updated.");
            }
            else
            {
                // Create a new cart item if it doesn't exist
                cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    CartId = cart.Id,
                    Cart = cart,
                    Product = product,

                };
                cart.Items.Add(cartItem);
                Debug.WriteLine("New cart item added.");
            }

            // Update the cart's total price
            cart.TotalPrice = cart.Items.Sum(item => item.Quantity * item.Product.Price);
            Debug.WriteLine($"Cart TotalPrice updated to: {cart.TotalPrice}");

            try
            {
                await _context.SaveChangesAsync(); // Save changes to the database
                Debug.WriteLine("Cart changes saved successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving cart: {ex.Message}");
                return Json(new { success = false, message = "Failed to save cart changes." });
            }

            return Json(new { success = true, message = "Item added to cart!" });
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }


            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Cart");
        }



    }
}
