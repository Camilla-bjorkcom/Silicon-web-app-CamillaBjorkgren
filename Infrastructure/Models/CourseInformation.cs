using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;
public class CourseInformation
{
    public string Id { get; set; } = null!;

    [Required(ErrorMessage = "Enter a title")]
    [DataType(DataType.Text)]
    [Display(Name = "Title")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Enter an image name")]
    [DataType(DataType.Text)]
    [Display(Name = "Image name")]
    public string? ImageName { get; set; }

    [Display(Name = "Is bestseller")]
    public bool IsBestSeller { get; set; } = false;

    [Display(Name = "Is digital")]
    public bool IsDigital { get; set; } = false;

    [Required(ErrorMessage = "Enter a creator")]
    [Display(Name = "Creator")]
    public string Creator { get; set; } = null!;

    [Required(ErrorMessage = "Enter a price")]
    [Display(Name = "Price")]
    public decimal? Price { get; set; }

    [Display(Name = "Discount price")]
    public decimal? DiscountPrice { get; set; }

    [Required(ErrorMessage = "Enter estimated hours")]
    [Display(Name = "Hours")]
    public int? EstimatedHours { get; set; }

    [Required(ErrorMessage = "Enter a likes in procent")]
    [Display(Name = "Likes in procent (Like Parameter)")]
    public decimal? LikeParameter { get; set; }

    [Required(ErrorMessage = "Enter a likes in numbers")]
    [Display(Name = "Likes in numbers (User votes)")]
    public decimal? UserVotes { get; set; }

    [Display(Name = "Big image name")]
    public string? BigImageName { get; set; }
}
