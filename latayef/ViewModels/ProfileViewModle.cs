using latayef.Models;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Project.ViewModels
{
	public class ProfileViewModle
	{

		public string Name { get; set; }

		//[DataType(DataType.Password)]
		//[MinLength(6, ErrorMessage = "The password must be at least 6 characters long.")]
		//public string Pasword { get; set; }

		public string City { get; set; }
		public string State { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		//public Wishlist Wishlist { get; set; }
		public string PhoneNumber { get; set; }
		public ICollection<Order> orders { get; set; }
		

	}
}
