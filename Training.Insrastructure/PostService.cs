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

        public Post GetPostById(Guid id)
        {
            return context.Posts.Include(post => post.Category).FirstOrDefault(post => post.Id == id);
        }

        public void AddNewPost(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            var existingPost = context.Posts.FirstOrDefault(p => p.Id == post.Id);

            if (existingPost != null)
            {
                existingPost.Title = post.Title;
                existingPost.Body = post.Body;
                existingPost.Category = post.Category;
                context.SaveChanges();
            }
        }

        public void DeletePost(Post post)
        {
            var existingPost = context.Posts.FirstOrDefault(p => p.Id == post.Id);

            if (existingPost != null)
            {
                context.Posts.Remove(existingPost);
                context.SaveChanges();
            }
        }
    }
}
