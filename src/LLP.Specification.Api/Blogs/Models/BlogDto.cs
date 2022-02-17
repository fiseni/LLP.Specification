namespace LLP.Specification.Api.Blogs.Models
{
    public record BlogDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<PostDto> Posts { get; set; } = new();
    }
}
