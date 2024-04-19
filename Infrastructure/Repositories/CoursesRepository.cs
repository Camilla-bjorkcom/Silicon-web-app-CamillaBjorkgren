using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CoursesRepository(WebApiDbContext context) : Repo<CourseEntity, WebApiDbContext>(context)
{
    private readonly WebApiDbContext _context = context;

    public override async Task<IEnumerable<CourseEntity>> GetAllAsync(string category = "")
    {
        {
            try
            {
                var query = _context.Courses
                    .Include(i => i.Category)
                    .AsQueryable();
                if (!string.IsNullOrEmpty(category) && category != "all")
                    query = query.Where(x => x.Category!.CategoryName == category);

                query = query.OrderByDescending(o => o.LastUpdated);
                var courses = await query.ToListAsync();
                return courses;
            }
            catch (Exception ex) { Debug.WriteLine("Error :: " + ex.Message); }

            return null!;
        }
    }

    public override Task<CourseEntity> GetOneAsync(Expression<Func<CourseEntity, bool>> predicate)
    {
        return base.GetOneAsync(predicate);
    }
}
