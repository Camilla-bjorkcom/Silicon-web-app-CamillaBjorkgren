using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;
using Web_app_Camilla.ViewModels;

namespace Web_app_Camilla.Controllers;

[Authorize]
public class AccountController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, AccountService accountService) : Controller
{
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AccountService _accountService = accountService;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (!_signInManager.IsSignedIn(User))
            return RedirectToAction("SignIn", "Auth");

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var viewModel = new AccountDetailsViewModel();
            viewModel.ProfileView ??= await PopulateProfileViewAsync();
            viewModel.BasicInfoForm ??= await PopulateBasicInfoFormAsync();
            viewModel.AddressInfoForm ??= await PopulateAddressInfoAsync();
            return View(viewModel);
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Index(AccountDetailsViewModel viewModel)
    {
        if (viewModel.BasicInfoForm != null)
        {
            if (viewModel.BasicInfoForm.FirstName != null && viewModel.BasicInfoForm.LastName != null && viewModel.BasicInfoForm.Email != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user.FirstName = viewModel.BasicInfoForm.FirstName;
                    user.LastName = viewModel.BasicInfoForm.LastName;
                    user.Email = viewModel.BasicInfoForm.Email;
                    user.UserName = viewModel.BasicInfoForm.Email;
                    user.Bio = viewModel.BasicInfoForm.Bio;
                    user.PhoneNumber = viewModel.BasicInfoForm.Phone;
                    user.Modified = DateTime.Now;


                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("ErrorUpdating", "Data did not update correctly");
                        ViewData["ErrorMessage"] = "Data did not update basic information correctly";
                    }
                }
            }
        }

        if (viewModel.AddressInfoForm != null)
        {
            if (viewModel.AddressInfoForm.AddressLine_1 != null && viewModel.AddressInfoForm.PostalCode != null && viewModel.AddressInfoForm.City != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var address = await _accountService.GetAddressAsync(user.Id);
                    if (address != null)
                    {
                        address.AddressLine_1 = viewModel.AddressInfoForm.AddressLine_1!;
                        address.AddressLine_2 = viewModel.AddressInfoForm.AddressLine_2!;
                        address.PostalCode = viewModel.AddressInfoForm.PostalCode!;
                        address.City = viewModel.AddressInfoForm.City!;

                        var result = await _accountService.UpdateAddressAsync(address, user);
                        if (!result)
                        {
                            ModelState.AddModelError("ErrorUpdating", "Data did not update correctly");
                            ViewData["ErrorMessage"] = "Data did not update address information correctly";
                        }
                    }
                    else
                    {
                        address = new AddressEntity
                        {
                            UserId = user.Id,
                            AddressLine_1 = viewModel.AddressInfoForm.AddressLine_1!,
                            AddressLine_2 = viewModel.AddressInfoForm.AddressLine_2!,
                            PostalCode = viewModel.AddressInfoForm.PostalCode!,
                            City = viewModel.AddressInfoForm.City!,
                        };
                        var result = await _accountService.CreateAddressAsync(address, user);
                        if (!result)
                        {
                            ModelState.AddModelError("ErrorUpdating", "Data did not update correctly");
                            ViewData["ErrorMessage"] = "Data did not update address information correctly";
                        }
                    }
                }
            }
        }
        ////uppdaterar informationen

        viewModel.BasicInfoForm ??= await PopulateBasicInfoFormAsync();
        viewModel.AddressInfoForm ??= await PopulateAddressInfoAsync();
        viewModel.ProfileView ??= await PopulateProfileViewAsync();
    

        return View(viewModel);
    }


    private async Task<ProfileViewModel> PopulateProfileViewAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new ProfileViewModel
        {
            FirstName = user!.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
        };
    }

    private async Task<BasicInfoFormViewModel> PopulateBasicInfoFormAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new BasicInfoFormViewModel
        {
            userID = user!.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            Phone = user.PhoneNumber,
            Bio = user.Bio,
            IsExternalAccount = user.IsExternalAccount,
        };
    }

    private async Task<AddressInfoFormViewModel> PopulateAddressInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null && user.AddressId != null)
        {
            var address = await _accountService.GetAddressAsync(user.Id);
            return new AddressInfoFormViewModel
            {
                AddressLine_1 = address.AddressLine_1,
                AddressLine_2 = address.AddressLine_2,
                PostalCode = address.PostalCode,
                City = address.City,
            };
        }
        return new AddressInfoFormViewModel();
    }

    [HttpGet]
   public IActionResult AccountSecurity()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AccountSecurity()
    {
        return View();
    }
}

//[HttpPost]
//[Route("/account/details")]
//public async Task<IActionResult> BasicInfo(AccountDetailsViewModel viewModel)
//{

//    var updateUser = await _accountService.UpdateUserAsync(viewModel.User);
//    if (updateUser.StatusCode == Infrastructure.Models.StatusCode.ERROR)
//    {
//        ModelState.AddModelError("ErrorUpdating", "Data did not update correctly");
//        ViewData["ErrorMessage"] = "Data did not update correctly";

//    }
//    return RedirectToAction("Details", "Account", viewModel);
//}

//[HttpPost]
//public IActionResult SaveBasicInfo(AccountDetailsViewModel viewModel)
//{
//    if (TryValidateModel(viewModel.BasicInfoForm))
//    {
//        return RedirectToAction("Index", "Home");
//    }

//    //var updateUser = await _accountService.UpdateUserAsync(viewModel.User);
//    //if (updateUser.StatusCode == Infrastructure.Models.StatusCode.ERROR)
//    //{
//    //    ModelState.AddModelError("ErrorUpdating", "Data did not update correctly");
//    //    ViewData["ErrorMessage"] = "Data did not update correctly";

//    //}
//    ModelState.AddModelError("ErrorUpdating", "Data did not update correctly");
//    ViewData["ErrorMessage"] = "Data did not update correctly";
//    return View("Index", viewModel);
//}

//[HttpPost]
//public IActionResult SaveAddressInfo(AccountDetailsViewModel viewModel)
//{
//    if (TryValidateModel(viewModel.AddressInfoForm))
//    {
//        return RedirectToAction("Index", "Home");
//    }

//    //var updateUser = await _accountService.UpdateUserAsync(viewModel.User);
//    //if (updateUser.StatusCode == Infrastructure.Models.StatusCode.ERROR)
//    //{
//    //    ModelState.AddModelError("ErrorUpdating", "Data did not update correctly");
//    //    ViewData["ErrorMessage"] = "Data did not update correctly";

//    //}
//    return View("Index", viewModel);
//}


