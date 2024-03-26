using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CoursesRepository(WebApiDbContext context) : Repo<CourseEntity, WebApiDbContext>(context)
{
    private readonly WebApiDbContext _context = context;
}
