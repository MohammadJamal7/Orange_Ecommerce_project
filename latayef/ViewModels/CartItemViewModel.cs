using latayef.Models;

namespace Ecommerce_Project.ViewModels
{
    public class CartItemViewModel
    {
        public Cart cart { get; set; }

        public Product product { get; set; }

		public List<CartItem> Items { get; set; } = new List<CartItem>();  // Initialized with an empty list

	}
}
