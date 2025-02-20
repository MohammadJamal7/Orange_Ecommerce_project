﻿using latayef.Data;
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
            User currentUser = await _userManager.GetUserAsync(User);

            var cart = await _context.Carts
                                     .Include(c => c.Items)
                                     .ThenInclude(i => i.Product)
                                     .FirstOrDefaultAsync(c => c.UserId == currentUser.Id);

            if (cart == null || !cart.Items.Any())
            {
                // Set a message in ViewData to display in the view
                string catalogUrl = Url.Action("shop", "Pages"); // Adjust action/controller as needed
                ViewData["CartMessage"] = $"Your cart is empty. <a href='{catalogUrl}'>Browse products</a> to add items to your cart.";
                // Return the same view without redirecting
                return View();
            }

            // Create a new order with "Pending" status
            Order newOrder = new Order
            {
                CreatedAt = DateTime.Now,
                UserId = currentUser.Id,
                Products = new List<OrderItem>(),
                ShippingAddress = currentUser.Address,
                TotalPrice = cart.TotalPrice,
                Status = "Pending",
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
