
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_API_Camilla.Filters;

namespace Web_API_Camilla.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]

public class CoursesController(CoursesService courseService) : ControllerBase
{
    private readonly CoursesService _courseService = courseService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (ModelState.IsValid)
        {
            var result = await _courseService.GetAllAsync();
            if (result != null)
            {
                return Ok(result);
            }
        }
        return NotFound();
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        if (ModelState.IsValid)
        {
            var result = await _courseService.GetOneAsyncId(id);
            if (result != null)
            {
                return Ok(result);
            }
        }
        return NotFound();
    }

    [Authorize]
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
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateCourse(CourseIndexViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
                var entity = await _courseService.GetOneAsyncId(viewModel.Course!.Id);
                if (entity != null)
                {
                    CourseEntity course = new CourseEntity
                    {
                        Id = entity.Id,
                        Title = viewModel.UpdateDto.Title,
                        Price = viewModel.UpdateDto.Price,
                        DiscountPrice = viewModel.UpdateDto.DiscountPrice,
                        EstimatedHours = viewModel.UpdateDto.EstimatedHours,
                        BigImageName = viewModel.UpdateDto.BigImageName,
                        Creator = viewModel.UpdateDto.Creator,
                        ImageName = viewModel.UpdateDto.ImageName,
                        IsDigital = viewModel.UpdateDto.IsDigital,
                        LastUpdated = DateTime.Now,
                        Created = entity.Created,
                        IsBestSeller = viewModel.UpdateDto.IsBestSeller,
                        LikeParameter = viewModel.UpdateDto.LikeParameter,
                        UserVotes = viewModel.UpdateDto.UserVotes,
                    };
                    var result = await _courseService.UpdateCourseAsync(course);
                    if (result)
                    {
                        return Ok();
                    }              
                }            
            }
        return BadRequest();
    }


    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(string id)
    {
        if (ModelState.IsValid)
        {
            var entity = await _courseService.GetOneAsyncId(id);
            if (entity != null)
            {
                var result = await _courseService.DeleteCourseAsync(entity);
                return Ok(result);
            }
        }
        return BadRequest();
    }

}
