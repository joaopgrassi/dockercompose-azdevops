using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogApp.Data.Entities
{
    public class Blog
    {
        public Guid Id { get; private set; }

        public string Url { get; private set; }

        public List<Post> Posts { get; set; } = Enumerable.Empty<Post>().ToList();

        public Blog(Guid id, string url)
        {
            Id = id;
            Url = url;
        }

        public void UpdateUrl(string url)
        {
            Url = url;
        }
    }
}
