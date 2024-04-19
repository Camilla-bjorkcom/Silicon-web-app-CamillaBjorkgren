using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Web_app_Camilla.Controllers;

public class ContactController(HttpClient http, IConfiguration configuration) : Controller
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Policy = "CIO")]
    [HttpGet]
    [Route("/Admin/ContactFormMessages")]
    public async Task<IActionResult> Messages()
    {
        var viewModel = new ContactViewModel();
        try
        {
            if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _http.GetAsync($"https://localhost:7138/api/Contact?key={_configuration["ApiKey:Secret"]}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    viewModel.Messages = JsonConvert.DeserializeObject<IEnumerable<ContactFormModel>>(json)!;
                    return View(viewModel);
                }
            }

        }
        catch { }

        ViewData["Error"] = "Failed at fetching courses from server.";
        return View(viewModel.Messages);
    }

    [Authorize(Policy = "CIO")]
    [HttpGet]
    [Route("/Admin/ContactFormMessageDetails")]
    public async Task<IActionResult> MessageDetails(string id)
    {
        var viewModel = new ContactViewModel();
        try
        {
            if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _http.GetAsync($"https://localhost:7138/api/Contact/{id}?key={_configuration["ApiKey:Secret"]}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var message = JsonConvert.DeserializeObject<ContactFormModel>(json)!;
                    return View(message);
                }
            }

        }
        catch { }

        ViewData["Error"] = "Failed at fetching courses from server.";
        return View(viewModel.Messages);
    }


    [HttpPost]
    public async Task<IActionResult> SendContactForm(ContactViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var json = JsonConvert.SerializeObject(viewModel.ContactForm);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _http.PostAsync($"https://localhost:7138/api/Contact?key={_configuration["ApiKey:Secret"]}", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Thank you for your message. We will get back at you as soon as possible.";
                    return RedirectToAction("Index");
                }
            }
            catch { TempData["Error"] = "Failed sending message. Please try again"; return RedirectToAction("Index"); }
        }
        TempData["Error"] = "Something went wrong, please try again.";
        return RedirectToAction("Index");
    }

}
