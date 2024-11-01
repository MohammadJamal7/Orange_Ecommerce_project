namespace latayef.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public string TransactionId { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }

        // Relationships
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }

}
