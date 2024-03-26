//using Infrastructure.Entities;
//using Infrastructure.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore.Storage;
//using System.Reflection;

//namespace Web_API_Camilla.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//[UseApiKey]
//[Authorize]
//public class CoursesController : ControllerBase
//{
//    // private readonly repository / Service

//    [HttpGet]
//    public async Task<IActionResult> GetAll()
//    {
//        var courses = await _context.Courses.ToListAsync();
//          return OK(courses);
//        return NotFound();
//    }
//   

//    [HttpGet("{id}")]
//    public async Task<IActionResult> GetOne(string id)
//    {
//        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
//        if(course != null)
//        {
//            return Ok(course);
//        }
//        return NotFound();    
//    }

//    [HttpPost]
//    public async Task<IActionResult> CreateOne(CourseCreateDto dto)
//    {
//        if (ModelState.IsValid)
//        {
//              kontrollera att det inte finns en course med samma titel med AnyAsync
//            var courseEntity = new CourseEntity
//            {
//                Title = dto.Title,
//                Price = dto.Price,
//                DiscountPrice = dto.DiscountPrice,
//                EstimatedHours = dto.EstimatedHours,
//                IsBestSeller = dto.IsBestSeller,
//                UserVotes = dto.UserVotes,
//                LikeParameter = dto.LikeParameter,
//                Category = dto.Category,
//                Creator = dto.Creator,
//                ImageName = dto.ImageName,
//            };

//            _context.Courses.Add(courseEntity);
//            await _context.SaveChangesAsync();

//            var courseModel = new CourseModel
//            {
//                Id = courseEntity.Id,
//                Title = courseEntity.Title,
//                Price = courseEntity.Price,
//                DiscountPrice = courseEntity.DiscountPrice,
//                EstimatedHours = courseEntity.EstimatedHours,
//                IsBestSeller = courseEntity.IsBestSeller,
//                UserVotes = courseEntity.UserVotes,
//                LikeParameter = courseEntity.LikeParameter,
//                Category = courseEntity.Category,
//                Creator = courseEntity.Creator,
//            };




//            return Created("", courseModel);
//        }
//        return BadRequest();
//    }


//}
