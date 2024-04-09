using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Web_app_Camilla.Controllers;

[Authorize]
public class CoursesController(HttpClient http, IConfiguration configuration, UserManager<UserEntity> userManager) : Controller
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;
    private readonly UserManager<UserEntity> _userManager = userManager;


    [HttpGet]
    [Route("/Courses")]
    public async Task<IActionResult> Courses()
    {
        var viewModel = new CourseIndexViewModel();
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var responseCourseId = await _http.GetAsync($"https://localhost:7138/api/courses/user/{user.Id}?key={_configuration["ApiKey:Secret"]}");
                if (responseCourseId.IsSuccessStatusCode)
                {
                    var courseIds = await responseCourseId.Content.ReadAsStringAsync();
                    viewModel.CoursesId = JsonConvert.DeserializeObject<IEnumerable<CourseIdModel>>(courseIds)!;
                }

                var response = await _http.GetAsync($"https://localhost:7138/api/Courses?key={_configuration["ApiKey:Secret"]}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    viewModel.Courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(json)!;
                    return View(viewModel);
                }

            }
        }
        catch { }

        ViewData["Error"] = "Failed at fetching courses from server.";
        return View(viewModel);
    }


    public async Task<IActionResult> CourseDetails(string id)
    {
        try
        {
            var response = await _http.GetAsync($"https://localhost:7138/api/Courses/{id}?key={_configuration["ApiKey:Secret"]}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var course = JsonConvert.DeserializeObject<CourseModel>(json)!;
                return View(course);
            }


        }
        catch { }
        ViewData["Error"] = "Failed at fetching course from server.";
        return View();
    }
}
