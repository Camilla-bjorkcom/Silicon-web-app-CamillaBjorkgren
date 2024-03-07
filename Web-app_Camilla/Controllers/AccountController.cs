using Microsoft.AspNetCore.Mvc;

namespace Web_app_Camilla.Controllers;

public class AccountController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
