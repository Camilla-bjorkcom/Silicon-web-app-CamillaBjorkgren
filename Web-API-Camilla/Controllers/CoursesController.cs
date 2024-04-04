
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Reflection;
using Web_API_Camilla.Filters;
using static System.Net.WebRequestMethods;

namespace Web_API_Camilla.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
[Authorize]
public class CoursesController(CoursesService courseService) : ControllerBase
{
    private readonly CoursesService _courseService = courseService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _courseService.GetAllAsync();
        if (result != null)
        {
            return Ok(result);
        }
        return NotFound();
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        var result = await _courseService.GetOneAsyncId(id);
        if (result != null)
        {
            return Ok(result);
        }
        return NotFound();
    }


    [HttpPost]
    public async Task<IActionResult> CreateOne(CourseCreateDto dto)
    {
        if (ModelState.IsValid)
        {
            var existingCourse = await _courseService.ExistsCourseAsync(dto.Title);
            if (!existingCourse)
            {
                CourseEntity courseEntity = dto;
                var result = await _courseService.CreateCourseAsync(courseEntity);
                if (result)
                {
                    var courseModel = new CourseModel
                    {
                        Id = courseEntity.Id,
                        Title = courseEntity.Title,
                        Price = courseEntity.Price,
                        DiscountPrice = courseEntity.DiscountPrice,
                        EstimatedHours = courseEntity.EstimatedHours,
                        IsBestSeller = courseEntity.IsBestSeller,
                        UserVotes = courseEntity.UserVotes,
                        LikeParameter = courseEntity.LikeParameter,
                        Creator = courseEntity.Creator,
                    };
                    return Created("", courseModel);
                }
                return BadRequest();
            }
            return Conflict();
        }
        return BadRequest(ModelState);

    }

    [HttpPut]
    public async Task<IActionResult> UpdateCourse(CourseUpdateDto dto)
    {
        if (ModelState.IsValid)
        {
            //Ändra till ID? Hur få in Id från sidan här?
            var entity = await _courseService.GetOneAsyncTitle(dto.OldTitle);
            if (entity != null)
            {
                CourseEntity course = new CourseEntity
                {
                    Id= entity.Id,
                    Title = dto.Title,
                    Price = dto.Price,
                    DiscountPrice = dto.DiscountPrice,
                    EstimatedHours = dto.EstimatedHours,
                    BigImageName = dto.BigImageName,
                    Creator = dto.Creator,
                    ImageName = dto.ImageName,
                    IsDigital = dto.IsDigital,
                    LastUpdated = DateTime.Now,
                    Created = entity.Created,
                    IsBestSeller = dto.IsBestSeller,
                    LikeParameter = dto.LikeParameter,
                    UserVotes = dto.UserVotes,
                };
                var result = await _courseService.UpdateCourseAsync(course);
                if (result)
                    return Ok();
                return BadRequest();
            }
        }
        return BadRequest();
    }





    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(string title)
    {
        var entity = await _courseService.GetOneAsyncTitle(title);
        if (entity != null)
        {
            var result = await _courseService.DeleteCourseAsync(entity);
            return Ok(result);
        }
        return BadRequest();
    }

}
