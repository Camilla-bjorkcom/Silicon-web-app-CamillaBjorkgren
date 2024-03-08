using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Web_app_Camilla.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    [HttpGet]
    [Route("/signin")]
    public IActionResult SignIn()
    {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Details", "Account");
        return View();
    }

    [HttpPost]
    [Route("/signin")]
    public async Task<IActionResult> SignIn(SignInModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Account");
            }
        }
        ModelState.AddModelError("IncorrectValues", "Incorrect email or password");
        ViewData["ErrorMessage"] = "Incorrect email or password";
            return View(model);
    }

    [HttpGet]
    [Route("/signup")]
    public IActionResult SignUp()
    {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Details", "Account");
            
        return View();
    }

    [HttpPost]
    [Route("/signup")]
    public async Task<ActionResult> SignUp(SignUpModel model)
    {
        if (ModelState.IsValid)
        {
            var exists = await _userManager.Users.AnyAsync(x => x.Email == model.Email);
                if (exists)
            {
                ModelState.AddModelError("AlreadyExists", "User with the same email address already exists");
                return View();
            }
            var userEntity = new UserEntity
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
            };
           var result = await _userManager.CreateAsync(userEntity, model.Password); 
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn");
            }
      
        }
        ViewData["ErrorMessage"] = "Email and password is required";
        return View(model);
    }

    [HttpGet]
    [Route("/signout")]
    public new async Task<IActionResult> SignOut() 
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home"); 
    }
}
