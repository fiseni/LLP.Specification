using LLP.Specification.Domain.Contracts;

namespace LLP.Specification.Domain.Blogs
{
    public class Blog : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        private readonly List<Post> _posts = new();
        public IEnumerable<Post> Posts => _posts.AsEnumerable();

        public Blog(string name)
        {
            Update(name);
        }

        public void Update(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public Post AddPost(Post post)
        {
            if (post is null) throw new ArgumentNullException(nameof(post));

            var existingPost = Posts.FirstOrDefault(x => x.Title == post.Title);
            if (existingPost is not null)
            {
                throw new ArgumentException("The provided post already exists!");
            }

            _posts.Add(post);
            return post;
        }

        public void DeletePost(Guid postId)
        {
            var post = Posts.FirstOrDefault(x => x.Id == postId);

            if (post is null) throw new ArgumentNullException(nameof(post));

            _posts.Remove(post);
        }
    }
}
