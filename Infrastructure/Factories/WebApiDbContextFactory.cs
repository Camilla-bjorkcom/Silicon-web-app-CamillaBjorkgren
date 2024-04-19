using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Factories;

public class WebApiDbContextFactory : IDesignTimeDbContextFactory<WebApiDbContext>
{
    public WebApiDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WebApiDbContext>();
        optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\IT_kurser\\Kurser\\Webbutveckling-dotnet\\ASPNET\\Uppgifter_ASPNET\\Infrastructure\\Data\\Local_databaseWebApi.mdf;Integrated Security=True");

        return new WebApiDbContext(optionsBuilder.Options);
    }
}