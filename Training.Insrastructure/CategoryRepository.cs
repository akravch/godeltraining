using System;
using System.Linq;
using Training.Domain;
using Training.Dto;
using Training.Infrastructure.Interfaces;

namespace Training.Insrastructure
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDataContext context;

        public CategoryRepository(IDataContext context)
        {
            this.context = context;
        }

        public IQueryable<CategoryDto> Categories => context.Categories.Select(category => new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Posts = category.Posts.Select(post => new PostDto
            {
                Body = post.Body,
                CreationDateTimeUtc = post.CreationDateTimeUtc
            }).ToList()
        });

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
