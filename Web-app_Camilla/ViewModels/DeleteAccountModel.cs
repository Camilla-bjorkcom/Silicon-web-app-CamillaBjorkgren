using Infrastructure.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Web_app_Camilla.ViewModels;

public class DeleteAccountModel
{
    [Display(Name = "Yes, I want to delete my account.", Order = 3)]
    [Required(ErrorMessage = "You must check the box")]
    [CheckboxRequired(ErrorMessage = "You must check the box to proceed")]
    public bool CheckDeleteAccount { get; set; } = false;
}
