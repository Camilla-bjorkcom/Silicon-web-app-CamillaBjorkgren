using Infrastructure.Entities;

namespace Web_app_Camilla.ViewModels;

public class AccountDetailsViewModel
{
    public UserEntity User { get; set; } = null!;

    public BasicInfoFormViewModel? BasicInfoForm { get; set; }

    public AddressInfoFormViewModel? AddressInfoForm { get; set; } 

    public ProfileViewModel? ProfileView { get; set; }

}
