using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Web_app_Camilla.ViewModels;
using static System.Net.WebRequestMethods;

namespace Web_app_Camilla.Controllers;

[Authorize]
public class AccountController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, AccountService accountService, HttpClient http, IConfiguration configuration) : Controller
{
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AccountService _accountService = accountService;
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;

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
    public async Task<IActionResult> AccountSecurity()
    {
        if (!_signInManager.IsSignedIn(User))
            return RedirectToAction("SignIn", "Auth");

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var viewModel = new AccountSecurityViewModel
            {
                IsExternalAccount = user.IsExternalAccount
            };
            viewModel.ProfileView ??= await PopulateProfileViewAsync();
            return View(viewModel);
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> AccountSecurity(AccountSecurityViewModel viewModel)
    {
        if (viewModel.PasswordForm.CurrentPassword != null && viewModel.PasswordForm.NewPassword != null && viewModel.PasswordForm.ConfirmPassword != null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var passwordResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash!, viewModel.PasswordForm.CurrentPassword);
                if (passwordResult == PasswordVerificationResult.Success)
                {
                    var result = await _accountService.UpdatePasswordAsync(user, viewModel.PasswordForm.NewPassword);
                    if (!result)
                    {
                        ModelState.AddModelError("ErrorPassword", "Failed to update password");
                        ViewData["ErrorMessage"] = "Failed to update password";
                    }
                    viewModel.ProfileView ??= await PopulateProfileViewAsync();
                    return View(viewModel);
                }
            }
        }
        ModelState.AddModelError("ErrorPassword", "Password is not correct");
        ViewData["ErrorMessage"] = "Your current password is not correct";
        viewModel.ProfileView ??= await PopulateProfileViewAsync();
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> AccountSavedItems()
    {
        var viewModel = new AccountSavedItemsViewModel();
        viewModel.ProfileView ??= await PopulateProfileViewAsync();

        if (!_signInManager.IsSignedIn(User))
            return RedirectToAction("SignIn", "Auth");

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            try
            {
                var responseCourseId = await _http.GetAsync($"https://localhost:7138/api/courses/course/{user.Id}?key={_configuration["ApiKey:Secret"]}");
                if (responseCourseId.IsSuccessStatusCode)
                {
                    var courseIds = await responseCourseId.Content.ReadAsStringAsync();
                    viewModel.CoursesId = JsonConvert.DeserializeObject<IEnumerable<CourseIdModel>>(courseIds)!;
                }

                var response = await _http.GetAsync($"https://localhost:7138/api/courses/user/{user.Id}?key={_configuration["ApiKey:Secret"]}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    viewModel.Courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(json)!;

                    return View(viewModel);
                }

            }
            catch { }

            ViewData["Error"] = "Failed at fetching courses from server.";

            return View(viewModel);


        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> JoinCourse(string userId, string courseId)
    {
        if (userId != null && courseId != null)
        {
            var result = await _accountService.JoinCourseAsync(userId, courseId);
            if (result)
            {
                return RedirectToAction("Courses", "Courses");
                //om denna är true, ändra färg till "bokmärkt" på courses-sida?
            }
            
        }
        return RedirectToAction("Courses", "Courses");
    }


    [HttpPost]
    public async Task<IActionResult> DeleteAccount(AccountSecurityViewModel viewModel)
    {
        if (viewModel.DeleteAccount.CheckDeleteAccount)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var deleteUser = await _accountService.DeleteUserAsync(user, viewModel.DeleteAccount.CheckDeleteAccount);
                if (deleteUser)
                {
                    return RedirectToAction("Deleted", "Auth");
                }
                ModelState.AddModelError("DeleteError", "Something went wrong trying to delete the account.");
                ViewData["ErrorMessage"] = "Something went wrong trying to delete the account.";
                viewModel.ProfileView ??= await PopulateProfileViewAsync();
                return View(viewModel);
            }

        }
        ModelState.AddModelError("DeleteError", "You must check the box before deleting you account");
        ViewData["ErrorMessage"] = "You must check the box before deleting you account.";
        viewModel.ProfileView ??= await PopulateProfileViewAsync();
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        var result = await _accountService.UploadUserProfileImageAsync(User, file);
        if (result)
        {
            ViewData["ImageUpload"] = "Sucessfully updated profile image";
            return RedirectToAction("Index", "Account");
        }
        else
        {
            ViewData["ImageUpload"] = "Could not update profile image";
            return RedirectToAction("Index", "Account");
        }
    }

    
}
