using latayef.Data;
using latayef.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Controllers
{
    public class OrdersController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _context;

        public OrdersController(UserManager<User> userManager, ApplicationContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            User CurrentUser = await _userManager.GetUserAsync(User);

            var cart = await _context.Carts
                                     .Include(c => c.Items)
                                     .ThenInclude(i => i.Product)
                                     .FirstOrDefaultAsync(c => c.UserId == CurrentUser.Id);

            if (cart == null )
            {
                return RedirectToAction("EmptyCartError"); // Redirect if the cart is empty or null
            }

            // Create a new order with "Pending" status
            Order newOrder = new Order
            {
                CreatedAt = DateTime.Now,
                UserId = CurrentUser.Id,
                Products = new List<OrderItem>(),
                ShippingAddress = CurrentUser.Address,
                TotalPrice = cart.TotalPrice,
                Status = "Pending", // Set initial status to Pending
                IsDelivered = false
            };

            // Add items to the order
            foreach (var cartItem in cart.Items)
            {
                OrderItem orderItem = new OrderItem
                {
                    Product = cartItem.Product,
                    Quantity = cartItem.Quantity
                };
                newOrder.Products.Add(orderItem);
            }

            // Save the order with "Pending" status to the database
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            // Pass the order ID to the view
            ViewData["OrderId"] = newOrder.Id;
            return View(newOrder);
        }



        [HttpPost]
        public async Task<IActionResult> ProcessPayment(
     int orderId, // Retrieve OrderId from form or hidden field
     string CardholderName,
     string CardNumber,
     string ExpiryDate,
     string CVV,
     string BillingAddress)
        {
            // Perform payment processing logic (replace with actual implementation)
            bool paymentSuccess = true; // Placeholder for actual payment verification

            if (paymentSuccess)
            {
                // Retrieve the order by OrderId
                var order = await _context.Orders.Include(o => o.Products)
                                                 .FirstOrDefaultAsync(o => o.Id == orderId);

                if (order == null)
                {
                    return BadRequest("Order not found.");
                }

                // Update order details
                order.Status = "Completed"; // Mark order as completed
                order.ShippingAddress = BillingAddress;

                // Clear the cart after the order is completed
                var cart = await _context.Carts.Include(c => c.Items)
                                                .FirstOrDefaultAsync(c => c.UserId == order.UserId);
                if (cart != null)
                {
                    cart.Items.Clear();
                }

                // Save changes to update order status and clear cart
                await _context.SaveChangesAsync();

                // Redirect to order confirmation or return success
                return RedirectToAction("Index", "Pages");
            }

            // If payment fails, return an error response
            return BadRequest("Payment failed.");
        }



    }
}
