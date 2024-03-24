using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class CoursesService(CoursesRepository coursesRepository)
{
    private readonly CoursesRepository _coursesRepository = coursesRepository;


}
