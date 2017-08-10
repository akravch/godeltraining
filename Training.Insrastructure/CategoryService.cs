using System;
using System.Collections.Generic;
using System.Linq;
using Training.Domain;
using Training.Infrastructure.Interfaces;

namespace Training.Insrastructure
{
    public class CategoryService : ICategoryService
    {
        private readonly IDataContext context;

        public CategoryService(IDataContext context)
        {
            this.context = context;
        }

        public ICollection<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        public Category GetCategoryByName(string name)
        {
            return context.Categories.FirstOrDefault(category => category.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public Category GetCategoryById(Guid id)
        {
            return context.Categories.FirstOrDefault(category => category.Id == id);
        }
    }
}
