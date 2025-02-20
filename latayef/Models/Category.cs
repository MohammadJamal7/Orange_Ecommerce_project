namespace latayef.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }

        // Relationships
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }

}
