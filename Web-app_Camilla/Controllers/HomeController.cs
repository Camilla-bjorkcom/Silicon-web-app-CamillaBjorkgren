using Microsoft.AspNetCore.Mvc;

namespace Web_app_Camilla.Controllers;

public class HomeController : Controller
{
    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("/error")]
    public IActionResult Error404(int statusCode) => View();
}
