using Infrastructure.Models;

namespace Web_app_Camilla.ViewModels;

public class CourseIndexViewModel
{
    public IEnumerable<CourseModel> Courses { get; set; } = new List<CourseModel>();

    //public 

}
