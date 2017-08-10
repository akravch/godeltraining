using System.Collections.Generic;
using Training.Domain;

namespace Training.Infrastructure.Interfaces
{
    public interface IPostService
    {
        ICollection<Post> GetPosts();

        ICollection<Post> GetPostsByCategory(string categoryName);

        void AddNewPost(Post post);
    }
}
