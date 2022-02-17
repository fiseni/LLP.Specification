using LLP.Specification.Domain.Blogs;
using LLP.Specification.Infrastructure.Blogs;
using Microsoft.EntityFrameworkCore;

namespace LLP.Specification.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Blog> Blogs => Set<Blog>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogConfiguration).Assembly);
        }
    }
}
