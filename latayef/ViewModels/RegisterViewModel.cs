using System.ComponentModel.DataAnnotations;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string Role { get; set; } // "Admin" or "Customer"

    // Additional fields
    [Phone]
    public string Phone { get; set; }

    public string State { get; set; }
    public string City { get; set; }
    public string Adress { get; set; }
}
