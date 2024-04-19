using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Web_app_Camilla.ViewModels;


namespace Web_app_Camilla.Controllers;

public class SubscribeController(HttpClient http, IConfiguration configuration) : Controller
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost]
    public async Task<IActionResult> Subscribe(SubscriberModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await _http.PostAsync($"https://localhost:7138/api/Subscribers?key={_configuration["ApiKey:Secret"]}", content);
                if (response.IsSuccessStatusCode)
                {             
                    TempData["Success"] = "Successfully subscribed to newsletter!";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    TempData["Error"] = "You are already subscribed";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    TempData["Error"] = "Failed at subscribing to newsletter! Contact the web admin";
                }
            }
            catch { TempData["Status"] = "ConnectionFailed"; }
        }
        else
        {
            TempData["Error"] = "You must enter an valid email address";
        }

        return RedirectToAction("Index", "Home", "newsletter");
    }

    [HttpGet]
    public IActionResult EndSubscription()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EndSubscription(EndSubscriptionViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var response = await _http.DeleteAsync($"https://localhost:7138/api/Subscribers/{viewModel.Email}?key={_configuration["ApiKey:Secret"]}");
                if (response.IsSuccessStatusCode)
                { ViewData["Success"] = "Successfully deleted your subscription to our newsletter!";
                    return View();
                }
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) 
                {
                    ViewData["Error"] = "Failed. Could not delete your email because it was not found.";
                    return View();
                }
                ViewData["Error"] = "Failed, please try again later or contact the web admin.";
            }
            catch { ViewData["Error"] = "Failed, please try again later or contact the web admin."; }
        }
        else
        {
            ViewData["Error"] = "Failed, please try again later or contact the web admin.";
        }
        return View();
    }
}
