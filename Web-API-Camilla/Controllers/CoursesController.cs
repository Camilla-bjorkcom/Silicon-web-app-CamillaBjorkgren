using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using Web_API_Camilla.Filters;

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
                var courseEntity = new CourseEntity
                {
                    Title = dto.Title,
                    Price = dto.Price,
                    DiscountPrice = dto.DiscountPrice,
                    EstimatedHours = dto.EstimatedHours,
                    IsBestSeller = dto.IsBestSeller,
                    IsDigital = dto.IsDigital,
                    UserVotes = dto.UserVotes,
                    LikeParameter = dto.LikeParameter,
                    Category = dto.Category,
                    Creator = dto.Creator,
                    ImageName = dto.ImageName,
                };
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
                        Category = courseEntity.Category,
                        Creator = courseEntity.Creator,
                    };
                    return Created("", courseModel);
                }
                return Problem();
            }
            return Conflict();
        }
        return BadRequest();

    }

    [HttpPut]
    public async Task<IActionResult> UpdateCourse(CourseCreateDto dto)
    {
        if (ModelState.IsValid)
        {
            var existingCourse = await _courseService.ExistsCourseAsync(dto.Title);
            if (!existingCourse)
            {
                var courseEntity = new CourseEntity
                {
                    Title = dto.Title,
                    Price = dto.Price,
                    DiscountPrice = dto.DiscountPrice,
                    EstimatedHours = dto.EstimatedHours,
                    IsBestSeller = dto.IsBestSeller,
                    IsDigital = dto.IsDigital,
                    UserVotes = dto.UserVotes,
                    LikeParameter = dto.LikeParameter,
                    Category = dto.Category,
                    Creator = dto.Creator,
                    ImageName = dto.ImageName,
                };

                var result = await _courseService.UpdateCourseAsync(courseEntity);
                if (result)
                    return Ok();
                return Problem();
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
