using System;
using System.Collections.Generic;
using Training.Domain;

namespace Training.Infrastructure.Interfaces
{
    public interface IPostService
    {
        ICollection<Post> GetPosts();

        ICollection<Post> GetPostsByCategory(string categoryName);

        Post GetPostById(Guid id);

        void AddNewPost(Post post);

        void UpdatePost(Post post);
    }
}
