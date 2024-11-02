namespace Ecommerce_Project.ViewModels
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ExistingImagePath { get; set; }

    }
}
