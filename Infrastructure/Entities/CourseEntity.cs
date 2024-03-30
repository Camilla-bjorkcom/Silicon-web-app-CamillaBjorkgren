using Infrastructure.Models;

namespace Infrastructure.Entities;

public class CourseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = null!;
    public string? ImageName { get; set; } 
    public bool IsBestSeller { get; set; }
    public bool IsDigital { get; set; }
    public string? Category { get; set; } 
    public string? Creator { get; set; } 
    public decimal? Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int? EstimatedHours { get; set; }
    public decimal? LikeParameter { get; set; }
    public decimal? UserVotes { get; set; }

    public static implicit operator CourseEntity(CourseModel model)
    {
        return new CourseEntity
        {
           Id = model.Id,
           Title = model.Title,
           ImageName = model.ImageName,
           IsBestSeller = model.IsBestSeller,
           IsDigital = model.IsDigital,
           Category = model.Category,
           Creator = model.Creator,
           Price = model.Price,
           DiscountPrice = model.DiscountPrice,
           EstimatedHours = model.EstimatedHours,
           LikeParameter = model.LikeParameter,
           UserVotes = model.UserVotes,
        };

    }
}
