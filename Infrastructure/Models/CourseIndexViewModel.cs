namespace Infrastructure.Models;


public class CourseIndexViewModel
{
    public IEnumerable<CourseModel> Courses { get; set; } = new List<CourseModel>();
    public IEnumerable<CategoryModel>? Categories { get; set; }

    public CourseUpdateDto? UpdateDto { get; set; }

    public Course? Course { get; set; }

    public IEnumerable<CourseIdModel> CoursesId { get; set; } = new List<CourseIdModel>();

    public Pagination? Pagination { get; set; }

}
