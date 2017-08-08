using System.Linq;
using Training.Dto;

namespace Training.Infrastructure.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<CategoryDto> Categories { get; }
        
        void Add(CategoryDto categoryDto);

        void Update(CategoryDto categoryDto);

        void Remove(CategoryDto categoryDto);
    }
}
