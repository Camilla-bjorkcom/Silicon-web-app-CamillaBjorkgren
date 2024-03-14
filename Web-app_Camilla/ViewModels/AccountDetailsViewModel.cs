using Infrastructure.Entities;

namespace Web_app_Camilla.ViewModels;

public class AccountDetailsViewModel
{
    public UserEntity? User { get; set; }

    public BasicInfoFormViewModel BasicInfoForm { get; set; } = null!;

    public AddressInfoFormViewModel AddressInfoForm { get; set; } = null!;

    public ProfileViewModel ProfileView { get; set; } = null!;

}
