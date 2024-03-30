using Microsoft.AspNetCore.Mvc;

namespace Web_app_Camilla.Controllers;

public class SiteSettings : Controller
{
    public IActionResult ChangeTheme(string mode)
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(60),
        };
        Response.Cookies.Append("ThemeMode", mode, option);
        return Ok();
    }


    [HttpPost]
    public IActionResult CookieConsent()
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddYears(1),
            HttpOnly = true,
            Secure = true,
        };
        Response.Cookies.Append("CookieConsent", "true", option);
        return Ok();
    }
}
