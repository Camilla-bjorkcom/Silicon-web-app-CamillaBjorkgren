using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;
public class UserCoursesEntity
{
    
    [ForeignKey(nameof(UserEntity))]
    public string UserId { get; set; } = null!;
    public UserEntity User { get; set; } = null!;

    
    [ForeignKey(nameof(CourseEntity))]
    public string CourseId { get; set; } = null!;
    public CourseEntity Course { get; set; } = null!;
}
