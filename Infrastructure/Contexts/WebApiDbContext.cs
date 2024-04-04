using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class WebApiDbContext(DbContextOptions<WebApiDbContext> options) : DbContext(options)
{
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<UserApiEntity> UserApi { get; set; }
    public DbSet<SubscriberEntity> Subscribers { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
}
