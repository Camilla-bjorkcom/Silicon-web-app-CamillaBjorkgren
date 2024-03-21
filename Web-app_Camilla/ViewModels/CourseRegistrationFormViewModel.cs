using System.ComponentModel.DataAnnotations;

namespace Web_app_Camilla.ViewModels;

public class CourseRegistrationFormViewModel
{
    [Required]
    [Display(Name = "Title")]
    public string Title { get; set; } = null!;

    [Display(Name = "ImageUrl")]
    public string? imageURL { get; set; }

    [Display(Name = "Is Bestseller")]
    public bool IsBestSeller { get; set; } = false;

    [Display(Name = "Category")]
    public string? Category { get; set; }

    [Display(Name = "Creator")]
    public string? Creator { get; set; }

    [Display(Name = "Price")]
    public string? Price { get; set; }

    [Display(Name = "Discount Price")]
    public string? DiscountPrice { get; set; }

    [Display(Name = "Hours")]
    public string? EstimatedHours { get; set; }

    [Display(Name = "Likes in procent (Like Parameter)")]
    public decimal? LikeParameter { get; set; }

    [Display(Name = "Likes in Numbers (User votes)")]
    public decimal? UserVotes { get; set; }

}
