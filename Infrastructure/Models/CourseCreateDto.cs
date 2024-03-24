using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class CourseCreateDto
{
    [Required]
    public string Title { get; set; } = null!;
    public string? ImageURL { get; set; }
    public bool IsBestSeller { get; set; } = false;
    public string? Category { get; set; }
    public string? Creator { get; set; } 
    public decimal? Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int? EstimatedHours { get; set; }
    public decimal? LikeParameter { get; set; }
    public decimal? UserVotes { get; set; }
}

