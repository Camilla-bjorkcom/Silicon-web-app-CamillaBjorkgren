namespace Infrastructure.Models;

public class CourseModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? ImageName { get; set; }
    public string? BigImageName { get; set; }
    public bool IsBestSeller { get; set; }
    public bool IsDigital { get; set; }
    public string? Category { get; set; }
    public string? Creator { get; set; }
    public decimal? Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int? EstimatedHours { get; set; }
    public decimal? LikeParameter { get; set; }
    public decimal? UserVotes { get; set; }
}
