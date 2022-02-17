namespace LLP.Specification.Api.Blogs.Models
{
    public class BlogCreateDto
    {
        public string Name { get; set; }
        public List<PostCreateDto> Posts { get; set; } = new();
    }
}
