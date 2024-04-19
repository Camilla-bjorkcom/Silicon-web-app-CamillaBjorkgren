using Infrastructure.Entities;
using Infrastructure.Models;

namespace Infrastructure.Factories;

public class CourseFactory
{
    public static CourseModel Create(CourseEntity entity)
    {
        try
        {
            return new CourseModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Price = entity.Price,
                DiscountPrice = entity.DiscountPrice,
                EstimatedHours = entity.EstimatedHours,
                IsBestSeller = entity.IsBestSeller,
                UserVotes = entity.UserVotes,
                LikeParameter = entity.LikeParameter,
                Category = entity.Category!.CategoryName,
                Creator = entity.Creator,
                ImageName = entity.ImageName,
                BigImageName  = entity.BigImageName,
                IsDigital = entity.IsDigital,              
            };
        }
        catch { }
        return null!;
    }

    public static IEnumerable<CourseModel> Create(IEnumerable<CourseEntity> entites)
    {
        List<CourseModel> courses = [];
        try
        {            
            foreach (var course in entites) 
            {
                courses.Add(Create(course));
            }
        }
        catch { }
        return courses;
    }
}
