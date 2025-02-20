using System.ComponentModel.DataAnnotations;

namespace latayef.Models
{
    public class Testimonial
    {
        public int Id { get; set; }

        [Required]
        public string Review { get; set; }


        public string Name { get; set; } = "Anonymous";


        [EmailAddress]
        public string Email { get; set; } = "anonymousUser@email.com";


        public bool? IsApproved { get; set; }

        // Relationships
        public string? UserId { get; set; }
        public virtual User? User { get; set; }


        public bool IsAnonymous { get; set; } = true;
    }
}