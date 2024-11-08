namespace latayef.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public string ShippingAddress { get; set; }
        public bool IsDelivered { get; set; }

        // Relationships
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> Products { get; set; } = new List<OrderItem>();
    }

}
