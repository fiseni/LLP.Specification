using LLP.Specification.Domain.Blogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LLP.Specification.Infrastructure.Blogs
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable(nameof(Blog));

            builder.Metadata.FindNavigation(nameof(Blog.Posts))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(b => b.Id);
        }
    }
}
