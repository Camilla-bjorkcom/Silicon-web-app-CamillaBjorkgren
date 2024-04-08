using Infrastructure.Models;

namespace Infrastructure.Entities;

public class CourseEntity
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Creator { get; set; } = null!;
    public string? ImageName { get; set; } 
    public string? BigImageName { get; set; }
    public bool IsBestSeller { get; set; }
    public bool IsDigital { get; set; }
    public decimal? Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public int? EstimatedHours { get; set; }
    public decimal? LikeParameter { get; set; }
    public decimal? UserVotes { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastUpdated { get; set; }
    public int? CategoryId { get; set; }
    public CategoryEntity? Category { get; set; }


    public static implicit operator CourseEntity(CourseCreateDto dto)
    {
        return new CourseEntity
        {
            Id = Guid.NewGuid().ToString(),
            Title = dto.Title,
            ImageName = dto.ImageName,
            BigImageName = dto.BigImageName,
            IsBestSeller = dto.IsBestSeller,
            IsDigital = dto.IsDigital,
            Creator = dto.Creator!,
            Price = dto.Price,
            DiscountPrice = dto.DiscountPrice,
            EstimatedHours = dto.EstimatedHours,
            LikeParameter = dto.LikeParameter,
            UserVotes = dto.UserVotes,
            Created = DateTime.Now,
            LastUpdated = DateTime.Now,
        };
    }

   
}
