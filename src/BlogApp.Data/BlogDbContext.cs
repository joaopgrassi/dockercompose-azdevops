using BlogApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; } = null!;
        
        public DbSet<Post> Posts { get; set; } = null!;

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Applies all the configurations for entities.	See	the	Configuration folder
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
