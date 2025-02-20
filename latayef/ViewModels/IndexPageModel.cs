using latayef.Models;

namespace Ecommerce_Project.ViewModels
{
    public class IndexPageModel
    {
        public Testimonial testimonial { get; set; }
        public List<Testimonial> testimonials { get; set; }

        public Product product { get; set; }
        public List<Product> products { get; set; }

        public Category category { get; set; }

        public List<Category> categories { get; set; }
    }
}
