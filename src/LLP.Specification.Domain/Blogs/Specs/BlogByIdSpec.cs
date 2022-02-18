using Ardalis.Specification;
using System.Linq.Expressions;

namespace LLP.Specification.Domain.Blogs.Specs
{
    public class BlogByIdSpec : BlogAggregateSpec
    {
        public BlogByIdSpec(Guid id)
        {
            Query.Where(x => x.Id == id);
        }
    }
}
