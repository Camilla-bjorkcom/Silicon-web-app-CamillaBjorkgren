using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace Infrastructure.Services;

public class CoursesService(CoursesRepository coursesRepository, WebAppDbContext webAppDbContext, IConfiguration configuration, HttpClient http)
{
    private readonly CoursesRepository _coursesRepository = coursesRepository;
    private readonly WebAppDbContext _webAppDbContext = webAppDbContext;
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _http = http;

    public async Task<CourseResult> GetCoursesAsync(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var response = await _http.GetAsync($"{_configuration["ApiUris:Courses"]}?category={Uri.UnescapeDataString(category)}&searchQuery={Uri.UnescapeDataString(searchQuery)}&pageNumber={pageNumber}&pageSize={pageSize}&key={_configuration["ApiKey:Secret"]}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CourseResult>(json)!;
                if (result != null && result.Success)
                {
                    return result;
                }
            }
            return null!;
        }
        catch (Exception)
        {
            return null!;
        }
    }

    public async Task<IEnumerable<CourseIdModel>> GetCoursesIdAsync(string userId)
    {
        try
        {
            var responseCourseId = await _http.GetAsync($"{_configuration["ApiUris:Courses"]}/course/{userId}?key={_configuration["ApiKey:Secret"]}");
            if (responseCourseId.IsSuccessStatusCode)
            {
                var json = await responseCourseId.Content.ReadAsStringAsync();
                var courseId = JsonConvert.DeserializeObject<IEnumerable<CourseIdModel>>(json)!;
                return courseId ??= null!;
            }
        }
        catch { }
        return null!;
    }

    public async Task<CourseEntity> GetOneAsyncId(string id)
    {
        try
        {
            var course = await _coursesRepository.GetOneAsync(x => x.Id == id);
            if (course != null)
            {
                return course;
            }
            return null!;
        }
        catch (Exception) { return null!; }
    }
    public async Task<CourseEntity> GetOneAsyncTitle(string title)
    {
        try
        {
            var course = await _coursesRepository.GetOneAsync(x => x.Title == title);
            if (course != null)
            {
                return course;
            }
            return null!;
        }
        catch (Exception) { return null!; }
    }

    public async Task<IEnumerable<CourseEntity>> GetAllSavedCourses(string userId)
    {
        try
        {
            var userCourses = await _webAppDbContext.UserCourses
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.CourseId)
                .ToListAsync();
            if (userCourses.Count > 0)
            {
                var coursesList = new List<CourseEntity>();


                foreach (var courseId in userCourses)
                {
                    var course = await GetOneAsyncId(courseId);
                    if (course != null)
                    {
                        coursesList.Add(course);
                    }
                }
                return coursesList;
            }
            return null!;
        }
        catch { return null!; }
    }

    public async Task<IEnumerable<CourseIdModel>> GetAllSavedCoursesId(string userId)
    {
        try
        {
            var userCoursesId = await _webAppDbContext.UserCourses
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.CourseId)
                .ToListAsync();
            if (userCoursesId.Count > 0)
            {
                var courseIdModels = userCoursesId.Select(id => new CourseIdModel { Id = id });
                return courseIdModels;
            }
            return null!;
        }
        catch { return null!; }
    }

    public async Task<bool> ExistsCourseAsync(string title)
    {
        try
        {
            var courseExisting = await _coursesRepository.Exists(x => x.Title == title);
            return courseExisting;

        }
        catch (Exception)
        {

            return false!;
        }
    }

    public async Task<bool> CreateCourseAsync(CourseEntity course)
    {
        try
        {
            var result = await _coursesRepository.CreateAsync(course);
            if (result != null)
            {
                return true;
            }
            return false;

        }
        catch (Exception)
        {

            return false!;
        }
    }

    public async Task<bool> UpdateCourseAsync(CourseEntity course)
    {
        try
        {
            var courseExisting = await _coursesRepository.GetOneAsync(x => x.Id == course.Id);
            if (courseExisting != null)
            {
                var result = await _coursesRepository.UpdateAsync(course);
                if (result != null)
                    return true;
                return false;

            }
            return false;

        }
        catch (Exception)
        {
            return false!;
        }
    }

    public async Task<bool> DeleteCourseAsync(CourseEntity course)
    {
        try
        {
            var result = await _coursesRepository.DeleteAsync(x => x.Title == course.Title);
            if (result)
                return true;
            return false;
        }
        catch (Exception)
        {

            return false!;
        }
    }
}
