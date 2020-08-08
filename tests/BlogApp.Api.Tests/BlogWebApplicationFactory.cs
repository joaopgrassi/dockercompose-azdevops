using BlogApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using Xunit;

namespace BlogApp.Api.Tests
{
    [Collection("Database")]
    public class BlogWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private readonly DbFixture _dbFixture;

        public BlogWebApplicationFactory(DbFixture dbFixture)
        {
            _dbFixture = dbFixture;
        }

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

                services.AddDbContext<BlogDbContext>(options =>
                {
                    // uses the connection string from the fixture
                    options.UseSqlServer(_dbFixture.ConnString);

                    // print EF debug logs during tests
                    var fac = LoggerFactory.Create(builder => { builder.AddDebug(); });
                    options.UseLoggerFactory(fac);
                });
            })
            .ConfigureLogging(builder =>
            {
                builder.AddDebug();
                builder.SetMinimumLevel(LogLevel.Information);
            });
        }
    }
}
