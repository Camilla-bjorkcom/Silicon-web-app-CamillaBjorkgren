using System.ComponentModel.DataAnnotations;

namespace Web_app_Camilla.ViewModels;

public class ProfileViewModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string ProfileImageUrl { get; set; } =
        "profile-image.svg";
}