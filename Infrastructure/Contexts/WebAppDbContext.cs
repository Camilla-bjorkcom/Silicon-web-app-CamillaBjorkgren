using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class WebAppDbContext(DbContextOptions<WebAppDbContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<AddressEntity> Address { get; set; }

    public DbSet<SubscriberEntity> Subscribers { get; set; }
}
