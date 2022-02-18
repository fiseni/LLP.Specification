using Ardalis.Specification;

namespace LLP.Specification.Domain.Blogs.Specs
{
    public abstract class BlogAggregateSpec : Specification<Blog>
    {
        public BlogAggregateSpec()
        {
            Query.Include(x => x.Posts);
        }
    }
}
