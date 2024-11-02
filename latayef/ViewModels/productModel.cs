using latayef.Models;

namespace Ecommerce_Project.ViewModels
{
    public class productModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public string Size { get; set; }
        public bool IsFavorite { get; set; }
        public double Discount { get; set; }
        public IFormFile iamgeFile { get; set; }
        public string ExistingImagePath { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
