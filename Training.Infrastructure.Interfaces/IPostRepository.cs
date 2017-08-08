using System.Linq;
using Training.Dto;

namespace Training.Infrastructure.Interfaces
{
    public interface IPostRepository
    {
        IQueryable<PostDto> Posts { get; }

        void Add(PostDto postDto);

        void Update(PostDto postDto);

        void Remove(PostDto postDto);
    }
}
