using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Web_app_Camilla.ViewModels;

namespace Web_app_Camilla.Controllers;

[Authorize]
public class CoursesController(HttpClient http, IConfiguration configuration) : Controller
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;

    [HttpGet]
    [Route("/Courses")]
    public async Task<IActionResult> Courses()
    {
        var viewModel = new CourseIndexViewModel();
        try
        {         
            if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _http.GetAsync($"https://localhost:7138/api/Courses?key={_configuration["ApiKey:Secret"]}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    viewModel.Courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(json)!;
                    return View(viewModel);
                }
            }
        }
        catch (Exception ex) { }

        ViewData["Error"] = "Failed at fetching courses from server.";
        return View(viewModel);
    }

    public async Task<IActionResult> CourseDetails(string id)
    {

        if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.GetAsync($"https://localhost:7138/api/Courses/{id}?key={_configuration["ApiKey:Secret"]}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var course = JsonConvert.DeserializeObject<CourseModel>(json)!;
                return View(course);
            }
        }
        ViewData["Error"] = "Failed at fetching course from server.";
        return View();
    }
}
