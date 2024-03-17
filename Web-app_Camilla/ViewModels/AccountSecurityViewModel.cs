using Infrastructure.Entities;

namespace Web_app_Camilla.ViewModels;

public class AccountSecurityViewModel
{
    public UserEntity? User { get; set; }

    public CreateNewPasswordModel PasswordForm { get; set; } = null!;

    public DeleteAccountModel DeleteAccount { get; set; } = null!;

    public ProfileViewModel ProfileView { get; set; } = null!;

    public bool IsExternalAccount { get; set; }
}
