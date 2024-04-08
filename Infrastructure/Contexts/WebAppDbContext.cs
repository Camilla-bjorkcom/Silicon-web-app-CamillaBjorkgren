using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Contexts;

public class WebAppDbContext(DbContextOptions<WebAppDbContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<AddressEntity> Address { get; set; }

    public virtual DbSet<UserCoursesEntity> UserCourses { get; set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<UserCoursesEntity>()
           .HasKey(x => new { x.UserId, x.CourseId });
        

        builder.Entity<UserCoursesEntity>()
            .HasOne(uc => uc.UserEntity) // Anger att UserCoursesEntity har en relation till UserEntity
            .WithMany() // UserEntity kan ha flera UserCoursesEntity
            .HasForeignKey(uc => uc.UserId); // ForeignKey för UserId
    }
}
