using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Web_app_Camilla.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    #region Individual Account | Sign In
    [HttpGet]
    [Route("/signin")]
    public IActionResult SignIn(string returnUrl)
    {
        if (_signInManager.IsSignedIn(User))
            return RedirectToAction("Details", "Account");

        ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/");
        return View();
    }

    [HttpPost]
    [Route("/signin")]
    public async Task<IActionResult> SignIn(SignInModel model, string returnUrl)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                return RedirectToAction("Index", "Account");
            }
        }
        ModelState.AddModelError("IncorrectValues", "Incorrect email or password");
        ViewData["ErrorMessage"] = "Incorrect email or password";
            return View(model);
    }
    #endregion

    #region Individual Account | Sign Up
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
                Created = DateTime.Now,
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
    #endregion

    #region Individual Account | Sign Out
    [HttpGet]
    [Route("/signout")]
    public new async Task<IActionResult> SignOut() 
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home"); 
    }
    #endregion


    #region External Account | Facebook
    [HttpGet]
    public IActionResult Facebook()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("FacebookCallback"));
        return new ChallengeResult("Facebook", authProps);
    }

    [HttpGet]
    public async Task<IActionResult> FacebookCallback()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info != null)
        {
            var userEntity = new UserEntity
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                IsExternalAccount = true,
                Created = DateTime.Now
            };
            var user = await _userManager.FindByEmailAsync(userEntity.Email);
            if (user == null)
            {
                var result = await _userManager.CreateAsync(userEntity);
                if (result.Succeeded)
                {
                    user = await _userManager.FindByEmailAsync(userEntity.Email);
                }
            }
            if (user != null)
            {
                if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                {
                    user.FirstName = userEntity.FirstName;
                    user.LastName = userEntity.LastName;
                    user.Email = userEntity.Email;
                    user.IsExternalAccount = true;
                    await _userManager.UpdateAsync(user);
                }
                await _signInManager.SignInAsync(user, isPersistent: false);
                if (HttpContext.User != null)
                {
                    return RedirectToAction("Index", "Account");
                }
            }
        }
        ModelState.AddModelError("InvalidFacebookAuthentication", "danger | Failed to authenticate with Facebook");
        ViewData["StatusMessage"] = "danger|Failed to authenticate with Facebook";
        return RedirectToAction("SignIn", "Auth");
    }
    #endregion

    #region External Account | Google
    [HttpGet]
    public IActionResult Google()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action("GoogleCallback"));
        return new ChallengeResult("Google", authProps);
    }

    [HttpGet]
    public async Task<IActionResult> GoogleCallback()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info != null)
        {
            var userEntity = new UserEntity
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                IsExternalAccount = true,
                Created = DateTime.Now
            };
            var user = await _userManager.FindByEmailAsync(userEntity.Email);
            if (user == null)
            {
                var result = await _userManager.CreateAsync(userEntity);
                if (result.Succeeded)
                {
                    user = await _userManager.FindByEmailAsync(userEntity.Email);
                }
            }
            if (user != null)
            {
                if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                {
                    user.FirstName = userEntity.FirstName;
                    user.LastName = userEntity.LastName;
                    user.Email = userEntity.Email;
                    user.IsExternalAccount = true;
                    await _userManager.UpdateAsync(user);
                }
                await _signInManager.SignInAsync(user, isPersistent: false);
                if (HttpContext.User != null)
                {
                    return RedirectToAction("Index", "Account");
                }
            }
        }
        ModelState.AddModelError("InvalidGoogleAuthentication", "danger | Failed to authenticate with Google");
        ViewData["StatusMessage"] = "danger|Failed to authenticate with Google";
        return RedirectToAction("SignIn", "Auth");
    }
    #endregion
}
