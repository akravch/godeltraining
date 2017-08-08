using System;
using System.Linq;
using Training.Domain;
using Training.Infrastructure.Interfaces;

namespace Training.Insrastructure
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext context;

        public CategoryRepository(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<CategoryDto> Categories => context.Categories.Select(category => new CategoryDto { Id = category.Id, Name = category.Name });

        public void Add(CategoryDto categoryDto)
        {
            context.Categories.Add(new Category { Name = categoryDto.Name });
            context.SaveChanges();
        }

        public void Update(CategoryDto categoryDto)
        {
            var existingCategory = categoryDto.Id != default(Guid) ? context.Categories.FirstOrDefault(category => category.Id == categoryDto.Id) : null;

            if (existingCategory != null)
            {
                existingCategory.Name = categoryDto.Name;
                context.SaveChanges();
            }
        }

        public void Remove(CategoryDto categoryDto)
        {
            var categoryToRemove = categoryDto.Id != default(Guid) ? context.Categories.FirstOrDefault(category => category.Id == categoryDto.Id) : null;

            if (categoryToRemove != null)
            {
                context.Categories.Remove(categoryToRemove);
                context.SaveChanges();
            }
        }
    }
}
