namespace Infrastructure.Models;
public class CourseResult
{
    public bool Success { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<CourseModel>? Courses { get; set; }
}
