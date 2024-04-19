using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class UserLoginForm
{
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email", Prompt = "Enter your email", Order = 1)]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "Enter your password", Order = 2)]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;

}
