using Azure.Core;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Text;
using Web_app_Camilla.ViewModels;
using static System.Net.WebRequestMethods;

namespace Web_app_Camilla.Controllers;

public class HomeController(HttpClient http, IConfiguration configuration) : Controller
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;

    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("/")]
    public async Task<IActionResult> Index(SubscriberViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                var response = await _http.PostAsync($"https://localhost:7138/api/subscribers?key={_configuration["ApiKey: Secret"]}", content);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Status"] = "Success";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    ViewData["Status"] = "AlreadyExists";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ViewData["Status"] = "Unauthorized";
                }
            }
            catch { ViewData["Status"] = "ConnectionFailed"; }
        }
        else
        {
            ViewData["Status"] = "Invalid";
        }
        return View(viewModel);
    }


    [Route("/error")]
    public IActionResult Error404(int statusCode) => View();

    [Route("/denied")]
    public IActionResult AccessDenied(int statusCode) => View();
}
