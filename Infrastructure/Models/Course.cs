namespace Infrastructure.Models;

public class Course
{
    public string Title { get; set; } = null!;

    public string? ImageName { get; set; }

    public bool IsBestSeller { get; set; } = false;

    public bool IsDigital { get; set; } = false;

    public string Creator { get; set; } = null!;

    public decimal? Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public int? EstimatedHours { get; set; }

    public decimal? LikeParameter { get; set; }

    public decimal? UserVotes { get; set; }
    public string? BigImageName { get; set; }
}
