namespace latayef.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string ShippingAddress { get; set; }

        // Relationships
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }

}
