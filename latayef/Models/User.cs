using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.Collections.Generic;

namespace latayef.Models
{
    public class User : IdentityUser
    {
        // Additional custom properties
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
         

        // Relationships
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual Cart Cart { get; set; }
        
        public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
        public virtual ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>();
    }
}
