﻿using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Web_app_Camilla.ViewModels;
using static System.Net.WebRequestMethods;

namespace Web_app_Camilla.Controllers;

[Authorize(Policy = "Admins")]
public class AdminController(IConfiguration configuration, HttpClient http) : Controller
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
    public IActionResult Settings()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(CourseCreateDto dto)
    {
        if (ModelState.IsValid)
        {
            if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var json = JsonConvert.SerializeObject(dto);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _http.PostAsync($"https://localhost:7138/api/Courses?key={_configuration["ApiKey:Secret"]}", content);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Success"] = "Successfully created a new course";
                    return RedirectToAction("Settings");
                }
            }
            ViewData["Error"] = "Failed at creating a new course";
        }
        else
        {
            ViewData["Error"] = "Failed at creating a new course";
        }
        return RedirectToAction("Settings");
    }

    [Authorize(Policy = "CIO")]
    [HttpGet]
    public IActionResult CreateCourse()
    {
        return View();
    }

    [Authorize(Policy = "CIO")]
    [HttpGet]
    public async Task<IActionResult> UpdateCourse(string id)
    {
        if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.GetAsync($"https://localhost:7138/api/Courses/{id}?key={_configuration["ApiKey:Secret"]}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var course = JsonConvert.DeserializeObject<Course>(json)!;
              
                var courseModel = new CourseIndexViewModel
                {
                    Course = new Course
                    {
                        Id = course.Id,
                        Title = course.Title,
                        ImageName = course.ImageName,
                        BigImageName = course.BigImageName,
                        IsBestSeller = course.IsBestSeller,
                        IsDigital = course.IsDigital,
                        EstimatedHours = course.EstimatedHours,
                        UserVotes = course.UserVotes,
                        LikeParameter = course.LikeParameter,
                        Creator = course.Creator,
                        DiscountPrice = course.DiscountPrice,
                        Price = course.Price,
                    }

                };
                return View(courseModel);
            }
        }
        ViewData["Error"] = "Failed at fetching course from server.";
        return View();
    }



    [Authorize(Policy = "CIO")]
    [HttpPost]
    public async Task<IActionResult> UpdateCourse(CourseIndexViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                var json = JsonConvert.SerializeObject(viewModel);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _http.PutAsync($"https://localhost:7138/api/Courses?key={_configuration["ApiKey:Secret"]}", content);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Success"] = "Successfully updated course";
                    return RedirectToAction("Courses", "Courses");
                }
            }
            ViewData["Error"] = "Failed at creating a new course";
        }
        else
        {
            ViewData["Error"] = "Failed at creating a new course";
        }
        return View(viewModel);
    }


 

    [Authorize(Policy = "CIO")]
    [HttpGet]
    public async Task<IActionResult> DeleteCourse(string id)
    {
        if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _http.DeleteAsync($"https://localhost:7138/api/Courses/{id}?key={_configuration["ApiKey:Secret"]}");
            if (response.IsSuccessStatusCode)
            {
                ViewData["Success"] = "Successfully deleted course";
                return RedirectToAction("Courses", "Courses");
            }
        }
        else
        {
            ViewData["Error"] = "Failed at deleting a new course";
        }
        return View();
    }
}

