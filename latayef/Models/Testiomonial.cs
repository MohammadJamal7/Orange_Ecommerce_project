using System.ComponentModel.DataAnnotations;

namespace latayef.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string Review { get; set; }
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; } = "anonymousUser@email.com";


        // Relationships
        public int ? UserId { get; set; }
        public virtual User ?User { get; set; }
    }

}
