using AutoMapper;
using LLP.Specification.Api.Blogs.Models;
using LLP.Specification.Domain.Blogs;

namespace LLP.Specification.Api.Blogs
{
    public class BlogReverseProfile : Profile
    {
        public BlogReverseProfile()
        {
            CreateMap<BlogCreateDto, Blog>()
                .ConvertUsing((src, dest, context) =>
                {
                    var blog = new Blog(src.Name);

                    foreach (var postCreateDto in src.Posts)
                    {
                        blog.AddPost(new Post(postCreateDto.Title));
                    }

                    return blog;
                });

            CreateMap<BlogUpdateDto, Blog>()
                .ConvertUsing((src, dest, context) =>
                {
                    if (dest is null) throw new ArgumentNullException(nameof(dest), "The object to be updated must be provided!");

                    dest.Update(src.Name);

                    return dest;
                });
        }
    }
}
