using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Web_app_Camilla.ViewModels;

namespace Web_app_Camilla.Controllers;

[Authorize]
public class CoursesController : Controller
{
    //private readonly HttpClient _http;


    public async Task<IActionResult> Courses()
    {
        var viewModel = new CourseIndexViewModel();

        using var http = new HttpClient();
        var response = await http.GetAsync("https://localhost:7138/api/courses");
        var json = await response.Content.ReadAsStringAsync();
        viewModel.Courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(json)!;

        return View(viewModel);
    }

    public async Task<IActionResult> CourseDetails()
    {
        using var http = new HttpClient();
        var response = await http.GetAsync("https://localhost:7138/api/courses");
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<CourseEntity>(json);

        return View(data);
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
