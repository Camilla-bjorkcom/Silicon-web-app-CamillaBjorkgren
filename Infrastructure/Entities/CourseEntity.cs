namespace Infrastructure.Entities;

public class CourseEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? imageURL { get; set; } 
    public bool IsBestSeller { get; set; }
    public string? Category { get; set; } 
    public string? Creator { get; set; } 
    public string? Price { get; set; }
    public string? DiscountPrice { get; set; }
    public string? EstimatedHours { get; set; }
    public decimal? LikeParameter { get; set; }
    public decimal? UserVotes { get; set; }
    public bool? SaveCourse { get; set; }
}
