using Infrastructure.Models;

namespace Infrastructure.Models;


public class CourseIndexViewModel
{
    public IEnumerable<CourseModel> Courses { get; set; } = new List<CourseModel>();

    public CourseCreateDto? CreateDto {  get; set; } 

    public CourseUpdateDto? UpdateDto { get; set; }

    public Course? Course { get; set; }

    public CourseModel? CourseModel { get; set; }

}
