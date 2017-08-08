using System.Linq;
using Training.Domain;

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
