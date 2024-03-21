using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Web_app_Camilla.ViewModels;

namespace Web_app_Camilla.Controllers;

public class CoursesController : Controller
{
    public async Task<IActionResult> Courses()
    {
        using var http = new HttpClient();
        var response = await http.GetAsync("https://localhost:7138/api/courses");
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<IEnumerable<CourseEntity>>(json);

        return View(data);
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
