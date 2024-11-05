namespace latayef.Models
{
    public class Wishlist
    {
        public int Id { get; set; }

        // Relationships

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }

}
