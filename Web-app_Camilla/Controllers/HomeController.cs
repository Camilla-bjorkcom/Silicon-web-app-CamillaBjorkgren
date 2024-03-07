using Microsoft.AspNetCore.Mvc;

namespace Web_app_Camilla.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Route("/error")]
    public IActionResult Error404(int statusCode) => View();
}
