using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class CourseCreateDto
{

    [Required]
    [Display(Name = "Title")]
    public string Title { get; set; } = null!;

    [Display(Name = "Image name")]
    public string? ImageName { get; set; }

    [Display(Name = "Is bestseller")]
    public bool IsBestSeller { get; set; } = false;

    [Display(Name = "Is digital")]
    public bool IsDigital { get; set; } = false;

    [Display(Name = "Creator")]
    public string Creator { get; set; } = null!;

    [Display(Name = "Price")]
    public decimal? Price { get; set; }

    [Display(Name = "Discount price")]
    public decimal? DiscountPrice { get; set; }

    [Display(Name = "Hours")]
    public int? EstimatedHours { get; set; }

    [Display(Name = "Likes in procent (Like Parameter)")]
    public decimal? LikeParameter { get; set; }

    [Display(Name = "Likes in numbers (User votes)")]
    public decimal? UserVotes { get; set; }


    [Display(Name = "Big image name")]
    public string? BigImageName { get; set; }

    [Display(Name = "Category name")]
    public int CategoryId { get; set; }

}

