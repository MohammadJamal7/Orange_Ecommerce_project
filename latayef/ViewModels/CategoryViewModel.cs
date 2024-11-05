using latayef.Models;

namespace Ecommerce_Project.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public IFormFile ImageFile { get; set; }
        public int ProductCount { get; set; }
        public string imagePath { get; set; }
    }
}