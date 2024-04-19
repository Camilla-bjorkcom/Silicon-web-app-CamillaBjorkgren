
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_API_Camilla.Filters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Web_API_Camilla.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]

public class CoursesController(CoursesService courseService, WebApiDbContext context) : ControllerBase
{
    private readonly CoursesService _courseService = courseService;
    private readonly WebApiDbContext _context = context;

    [HttpGet]
    public async Task<IActionResult> GetAll(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 10)
    {
        if (ModelState.IsValid)
        {
            var query = _context.Courses.Include(i => i.Category).AsQueryable();

            if (!string.IsNullOrEmpty(category) && category != "all")
            {
                query = query.Where(x => x.Category!.CategoryName == category);
            }
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(x => x.Title.Contains(searchQuery) || x.Creator.Contains(searchQuery));
            }
            query = query.OrderByDescending(o => o.LastUpdated);

            var courses = await query.ToListAsync();
            if (courses != null)
            {
                var response = new CourseResult
                {
                    Success = true,
                    TotalItems = await query.CountAsync()
                };
                response.TotalPages = (int)Math.Ceiling(response.TotalItems / (double)pageSize);
                response.Courses = CourseFactory.Create(await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync());
                return Ok(response);
            }
            return NotFound();
        }
        return BadRequest();       
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
            return NotFound();
        }
        return BadRequest();
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
                    CategoryId = viewModel.UpdateDto.CategoryId,
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
            return NotFound();
        }
        return BadRequest();
    }

    [UseApiKey]
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserCourses(string userId)
    {
        if (ModelState.IsValid)
        {
            var userCourses = await _courseService.GetAllSavedCourses(userId);
            if (userCourses != null)
            {
                return Ok(userCourses);
            }
            return NotFound();
        }
        return BadRequest();
    }

    [UseApiKey]
    [HttpGet("course/{userId}")]
    public async Task<IActionResult> GetUserCoursesIds(string userId)
    {
        if (ModelState.IsValid)
        {
            var userCoursesId = await _courseService.GetAllSavedCoursesId(userId);
            if (userCoursesId != null)
            {
                return Ok(userCoursesId);
            }
            return NotFound();
        }
        return BadRequest();
    }

}
