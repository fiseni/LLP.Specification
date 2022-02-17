using AutoMapper;
using LLP.Specification.Api.Blogs.Models;
using LLP.Specification.Domain.Blogs;

namespace LLP.Specification.Api.Blogs
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<Blog, BlogDto>();
        }
    }
}
