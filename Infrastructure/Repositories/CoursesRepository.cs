using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CoursesRepository(CoursesDbContext context) : Repo<CourseEntity, CoursesDbContext>(context)
{
    private readonly CoursesDbContext _context = context;
}
