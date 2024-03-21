using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace Web_app_Camilla.ViewModels;

public class SubscriberViewModel
{
    [Required]
    [Display(Name = "Subscribe", Prompt ="Enter your email address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    public bool IsSubscribed { get; set; } = false;
}
