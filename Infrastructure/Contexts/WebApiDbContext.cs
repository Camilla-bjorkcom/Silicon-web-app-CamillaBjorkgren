using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class WebApiDbContext(DbContextOptions<WebApiDbContext> options) : DbContext(options)
{
    public virtual DbSet<CourseEntity> Courses { get; set; }
    public DbSet<UserApiEntity> UserApi { get; set; }
    public virtual DbSet<SubscriberEntity> Subscribers { get; set; }
    public virtual DbSet<CategoryEntity> Categories { get; set; }

    public DbSet<ContactMessageEntity> ContactMessages { get; set; }



}
