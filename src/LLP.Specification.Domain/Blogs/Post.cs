using System.Diagnostics.CodeAnalysis;

namespace LLP.Specification.Domain.Blogs
{
    public class Post
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public Guid BlogId { get; private set; }

        public Post(string title)
        {
            Update(title);
        }

        [MemberNotNull(nameof(Title))]
        public void Update(string title)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));

            Title = title;
        }
    }
}
