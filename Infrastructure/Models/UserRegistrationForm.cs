using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class UserRegistrationForm
{
    [DataType(DataType.Text)]
    [Display(Name = "First name", Prompt = "Enter your first name", Order = 1)]
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Last name", Prompt = "Enter your last name", Order = 2)]
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter your email", Order = 3)]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter your password", Order = 4)]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;
}
