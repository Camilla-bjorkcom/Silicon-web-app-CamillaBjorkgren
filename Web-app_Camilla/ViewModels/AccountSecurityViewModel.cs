using Infrastructure.Entities;

namespace Web_app_Camilla.ViewModels;

public class AccountSecurityViewModel
{
    public UserEntity? User { get; set; }

    public CreateNewPasswordModel? PasswordForm { get; set; }

    public DeleteAccountModel? DeleteAccount { get; set; }

    public ProfileViewModel? ProfileView { get; set; } 

    public bool IsExternalAccount { get; set; }
}
