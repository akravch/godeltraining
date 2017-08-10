using System;
using System.Collections.Generic;
using Training.Domain;

namespace Training.Infrastructure.Interfaces
{
    public interface ICategoryService
    {
        ICollection<Category> GetCategories();

        Category GetCategoryByName(string name);

        Category GetCategoryById(Guid id);
    }
}
