using Infrastructure.Entities;
using Infrastructure.Models;

namespace Infrastructure.Factories;
public class CategoryFactory
{
    public static CategoryModel Create(CategoryEntity entity)
    {
        try
        {
            return new CategoryModel
            {
                Id = entity.Id,
                CategoryName = entity.CategoryName,
            };
        }
        catch { }
        return null!;
    }
    public static IEnumerable<CategoryModel> Create(IEnumerable<CategoryEntity> entities)
    {
        List<CategoryModel> categories = new List<CategoryModel>();
        try
        {
            foreach(var entity in entities)
                categories.Add(Create(entity));
            return categories;
        }
        catch { }
        return null!;
    }
}
