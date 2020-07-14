using BlogApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace BlogApp.Api.Tests
{
    public class BlogWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            builder.ConfigureServices(services =>
            {
                // Remove the app's ApplicationDbContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<BlogDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContext<BlogDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");

                    // print EF debug logs during tests
                    var fac = LoggerFactory.Create(builder => { builder.AddDebug(); });
                    options.UseLoggerFactory(fac);
                });

                using (var scope = services.BuildServiceProvider().CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<BlogDbContext>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();
                }
            })
            .ConfigureAppConfiguration((context, config) =>
            {
                //config.AddInMemoryCollection(new[]
                //{
                //    // TODO: Add later the docker connection string
                //});
            })
            .ConfigureLogging(builder =>
            {
                builder.AddDebug();
                builder.SetMinimumLevel(LogLevel.Information);
            });
        }
    }
}
