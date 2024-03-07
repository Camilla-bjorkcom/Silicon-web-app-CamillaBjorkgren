using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web_app_Camilla.Controllers;

public class AuthController : Controller
{
    [HttpGet]
    [Route("/signin")]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpPost]
    [Route("/signin")]
    public IActionResult SignIn(SignInModel model)
    {
        if (ModelState.IsValid)
        {
        }
        ViewData["ErrorMessage"] = "Email and password is required";
            return View(model);
    }

    [HttpGet]
    [Route("/signup")]
    public IActionResult SignUp()
    {
        ViewData["Title"] = "Sign Up";
        return View();
    }

    [HttpPost]
    [Route("/signup")]
    public IActionResult SignUp(SignUpModel model)
    {
        ViewData["Title"] = "Sign Up";

        if (ModelState.IsValid)
        {
                return RedirectToAction("SignIn");
        }
        return View(model);
    }
}
