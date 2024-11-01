namespace latayef.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string Review { get; set; }

        // Relationships
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

}
