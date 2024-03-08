using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_app_Camilla.ViewModels;

namespace Web_app_Camilla.Controllers;

public class AccountController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, AccountService accountService) : Controller
{
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AccountService _accountService = accountService;

    [HttpGet]
    [Route("/account/details")]
    public async Task<IActionResult> Index()
    {
        if (!_signInManager.IsSignedIn(User))
            return RedirectToAction("SignIn", "Auth");

        var userEntity = await _userManager.GetUserAsync(User);

       var viewModel = new AccountDetailsViewModel
        {
            User = userEntity!
        };
        return View(viewModel);
    }

    [HttpPost]
    [Route("/account/details")]
    public async Task<IActionResult> BasicInfo(AccountDetailsViewModel viewModel)
    {

        var updateUser = await _accountService.UpdateUserAsync(viewModel.User);
        if (updateUser.StatusCode == Infrastructure.Models.StatusCode.ERROR)
        {
            ModelState.AddModelError("ErrorUpdating", "Data did not update correctly");
            ViewData["ErrorMessage"] = "Data did not update correctly";

        }
        return RedirectToAction("Details", "Account", viewModel);
    }
}
