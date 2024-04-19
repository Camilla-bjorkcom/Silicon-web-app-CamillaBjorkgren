using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;
public class UserCoursesEntity
{
    public string UserId { get; set; } = null!;
    public UserEntity UserEntity { get; set; }
    public string CourseId { get; set; } = null!;
}
