using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_app_Camilla.ViewModels;

namespace Web_app_Camilla.Controllers;

public class AccountController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager) : Controller
{
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserManager<UserEntity> _userManager = userManager;

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
        var userEntity = await _userManager.GetUserAsync(User);
        var result = await _userManager.UpdateAsync(userEntity!);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("ErrorUpdating", "Data did not update correctly");
            ViewData["ErrorMessage"] = "Data did not update correctly";
        }
        return RedirectToAction("Details", "Account", viewModel);
    }
}
