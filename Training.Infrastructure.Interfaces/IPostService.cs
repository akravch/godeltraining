using System;
using System.Collections.Generic;
using Training.Domain;

namespace Training.Infrastructure.Interfaces
{
    public interface IPostService
    {
        int Count();

        int Count(string categoryName);

        ICollection<Post> GetPosts();

        ICollection<Post> GetPosts(int offset, int count);

        ICollection<Post> GetPostsByCategory(string categoryName);

        ICollection<Post> GetPostsByCategory(string categoryName, int offset, int count);

        Post GetPostById(Guid id);

        void AddNewPost(Post post);

        void UpdatePost(Post post);

        void DeletePost(Post post);
    }
}
