using System;

namespace BlogApp.Data.Entities
{
    public class Post
    {
        public Guid Id { get; private set; }

        public string Title { get; private set; }

        public string Content { get; private set; }

        public Guid BlogId { get; private set; }

        private Blog? _blog;

        public Blog Blog
        {
            set => _blog = value;
            get => _blog
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Blog));
        }

        public Post(Guid id, string title, string content, Guid blogId)
        {
            Id = id;
            Title = title;
            Content = content;
            BlogId = blogId;
        }
    }
}
