using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Training.Domain;
using Training.Infrastructure.Interfaces;

namespace Training.Insrastructure
{
    public class PostService : IPostService
    {
        private readonly IDataContext context;

        public PostService(IDataContext context)
        {
            this.context = context;
        }

        public ICollection<Post> GetPosts()
        {
            return context.Posts.ToList();
        }

        public ICollection<Post> GetPostsByCategory(string categoryName)
        {
            return context.Posts.Include(post => post.Category).Where(post => post.Category.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void AddNewPost(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
        }
    }
}
