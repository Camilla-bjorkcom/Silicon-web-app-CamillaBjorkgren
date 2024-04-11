using Infrastructure.Models;

namespace Web_app_Camilla.ViewModels;

public class AccountSavedItemsViewModel
{
    public ProfileViewModel ProfileView { get; set; } = null!;
    public IEnumerable<CourseModel> Courses { get; set; } = new List<CourseModel>();
    public IEnumerable<CourseIdModel?> CoursesId { get; set; } = new List<CourseIdModel>();
}
