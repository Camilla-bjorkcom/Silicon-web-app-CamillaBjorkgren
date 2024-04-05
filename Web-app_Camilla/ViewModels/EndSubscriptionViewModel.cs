using System.ComponentModel.DataAnnotations;

namespace Web_app_Camilla.ViewModels;

public class EndSubscriptionViewModel
{
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email address", Prompt = "Enter your email address to end the newsletter subscription")]
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{1,}$", ErrorMessage = "Invalid email address")]
    [Required(ErrorMessage = "You must enter an valid email address")]
    public string Email { get; set; } = null!;
}
