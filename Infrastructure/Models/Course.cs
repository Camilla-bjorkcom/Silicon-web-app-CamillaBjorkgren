﻿namespace Infrastructure.Models;

public class Course
{
    public string Id { get; set; } = null!;
    public string? Title { get; set; }

    public string? ImageName { get; set; }

    public bool IsBestSeller { get; set; }

    public bool IsDigital { get; set; }

    public string? Creator { get; set; }

    public decimal? Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public int? EstimatedHours { get; set; }

    public decimal? LikeParameter { get; set; }

    public decimal? UserVotes { get; set; }
    public string? BigImageName { get; set; }

    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }
}
