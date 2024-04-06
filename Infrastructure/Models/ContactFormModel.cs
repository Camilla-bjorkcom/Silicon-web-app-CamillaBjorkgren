using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class ContactFormModel
{
    public string? Id { get; set; }

    [Display(Name = "Full name", Prompt = "Enter your full name", Order = 0)]
    [Required(ErrorMessage = "Enter your name")]
    public string FullName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 1)]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;

    [Display(Name = "Service", Prompt = "Choose the service you are interested in", Order = 2)]
    public string? ServiceOption { get; set; }

    [Display(Name = "Full name", Prompt = "Enter your message here...", Order = 3)]
    [Required(ErrorMessage = "Enter your message")]
    public string Message { get; set; } = null!;

}

