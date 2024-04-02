using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Factories
{
    public class WebAppDbContextFactory : IDesignTimeDbContextFactory<WebAppDbContext>
    {
        public WebAppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WebAppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\IT_kurser\\Kurser\\Webbutveckling-dotnet\\ASPNET\\Uppgifter_ASPNET\\Infrastructure\\Data\\Local_databaseWebApp.mdf;Integrated Security=True");

            return new WebAppDbContext(optionsBuilder.Options);
        }
    }
}
