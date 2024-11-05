using latayef.Models;

namespace Ecommerce_Project.ViewModels
{
    public class CategoryModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ExistingImagePath { get; set; }
        public Category category { get; set; }
        public int ProductCount { get; set; }
        public string imagePath { get; set; }
        public List<CategoryViewModel> categoryViewModels { get; set; }

    }
}
