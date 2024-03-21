using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace Web_API_Camilla.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    // private readonly repository / Service

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _context.Courses.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if(course != null)
        {
            return Ok(course);
        }
        return NotFound();    
    }

    [HttpPost]
    public async Task<IActionResult> CreateOne(CourseCreateDto model)
    {
        if (ModelState.IsValid)
        {
            var courseEntity = new CourseEntity
            {
                Title = model.Title,
                Price = model.Price,
                DiscountPrice = model.DiscountPrice,
                EstimatedHours = model.EstimatedHours,
                IsBestSeller = model.IsBestSeller,
                UserVotes = model.UserVotes,
                LikeParameter = model.LikeParameter,
                Category = model.Category,
                SaveCourse = model.SaveCourse,
                Creator = model.Creator,
            };

            _context.Courses.Add(courseEntity);
            await _context.SaveChangesAsync();

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
                SaveCourse = courseEntity.SaveCourse,
                Creator = courseEntity.Creator,
            };

            


            return Created("", courseModel);
        }
        return BadRequest();
    }


}
