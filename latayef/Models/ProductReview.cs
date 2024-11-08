namespace latayef.Models
{
    public class ProductReview
    {
        public int Id { get; set; }
        public string Review { get; set; }

        // Relationships
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }

}
