using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ChatApp.Data.Database;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDatabaseContext>
{
    public AppDatabaseContext CreateDbContext(string[] args)
    {
        var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../ChatApp.Presentation"));

        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

        Console.WriteLine(Directory.GetCurrentDirectory());

        var optionsBuilder = new DbContextOptionsBuilder<AppDatabaseContext>();
        var connectionString = config.GetConnectionString("LocalDatabase");

        optionsBuilder.UseNpgsql(connectionString);

        return new AppDatabaseContext(optionsBuilder.Options);
    }
}