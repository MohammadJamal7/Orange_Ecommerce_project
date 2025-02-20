namespace latayef.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public string Size { get; set; }
        public bool IsFavorite { get; set; }
        public double Discount { get; set; }

        // Relationships
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
    }

}
