using System.Linq;
using System.Web.Mvc;
using Training.Dto;
using Training.Infrastructure.Interfaces;

namespace Training.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [ChildActionOnly]
        public PartialViewResult Categories()
        {
            var categories = categoryService.GetCategories().Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            });

            return PartialView("_CategoriesPartial", categories);
        }
    }
}