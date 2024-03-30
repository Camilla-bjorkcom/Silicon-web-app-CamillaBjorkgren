using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using Web_API_Camilla.Filters;
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

        return View(viewModel);
    }

    public async Task<IActionResult> CourseDetails(string id)
    {
        if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _http.GetAsync($"https://localhost:7138/api/courses/{id}?key={_configuration["ApiKey:Secret"]}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var course = JsonConvert.DeserializeObject<CourseModel>(json)!;
                return View(course);
            }
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(CourseRegistrationFormViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            using var http = new HttpClient();

            var json = JsonConvert.SerializeObject(viewModel);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await http.PostAsync($"http://localhost:7238/api/courses", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Courses", "Courses");
            }
        }
        return View(viewModel);
    }


}
