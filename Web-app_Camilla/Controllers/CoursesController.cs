using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Web_app_Camilla.Controllers;

[Authorize]
public class CoursesController(HttpClient http, IConfiguration configuration, UserManager<UserEntity> userManager, CategoriesService categoriesService, CoursesService coursesService) : Controller
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly CategoriesService _categoriesService = categoriesService;
    private readonly CoursesService _coursesService = coursesService;


    [HttpGet]
    public async Task<IActionResult> Courses(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 6)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var courseResult = await _coursesService.GetCoursesAsync(category, searchQuery, pageNumber, pageSize);
              
                var viewModel = new CourseIndexViewModel
                {
                    Categories = await _categoriesService.GetCategoriesAsync(),
                    CoursesId = await _coursesService.GetCoursesIdAsync(user.Id),
                    Courses = courseResult.Courses!,
                    Pagination = new Pagination
                    {
                        PageSize = pageSize,
                        CurrentPage = pageNumber,
                        TotalPages = courseResult.TotalPages,
                        TotalItems = courseResult.TotalItems
                    }
                };
                return View(viewModel);
            }
        }
        catch { }
        ViewData["Error"] = "Failed at fetching courses from server.";
        return View();
    }


    public async Task<IActionResult> CourseDetails(string id)
    {
        try
        {
            var response = await _http.GetAsync($"{_configuration["ApiUris:Courses"]}/{id}?key={_configuration["ApiKey:Secret"]}");
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
