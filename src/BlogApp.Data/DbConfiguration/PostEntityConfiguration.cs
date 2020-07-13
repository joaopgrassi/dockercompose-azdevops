using BlogApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Data.DbConfiguration
{
    public sealed class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title).HasMaxLength(255);

            // Relationship - Need to set instruct EF to access the field of the nav prop
            // because nullable ref types is enabled:
            // See more here: https://docs.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types#non-nullable-properties-and-initialization
            builder
                .HasOne(p => p.Blog)
                .WithMany(b => b.Posts)
                .HasForeignKey(p => p.BlogId)
                .Metadata.DependentToPrincipal.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
