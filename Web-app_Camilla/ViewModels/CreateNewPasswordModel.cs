using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Web_app_Camilla.ViewModels;

public class CreateNewPasswordModel
{
    public UserEntity User { get; set; } = null!;

    [ProtectedPersonalData]
    [DataType(DataType.Password)]
    [Display(Name = "Current password", Prompt = "********", Order = 0)]
    public string CurrentPassword { get; set; } = null!;

    [ProtectedPersonalData]
    [DataType(DataType.Password)]
    [Display(Name = "New password", Prompt = "********", Order = 1)]
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression("^(?=.*[a-ö])(?=.*[A-Ö])(?=.*\\d)(?=.*[@$!%*?&])[A-Öa-ö\\d@$!%*?&]{8,}$", ErrorMessage = "Invalid password, must be a strong password")]
    public string NewPassword { get; set; } = null!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password", Prompt = "Confirm your password", Order = 2)]
    [Required(ErrorMessage = "Password must be confirmed")]
    [Compare(nameof(NewPassword), ErrorMessage = "Fields do not match")]
    public string ConfirmPassword { get; set; } = null!;
}
