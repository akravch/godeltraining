using System.Web.Mvc;
using Training.Infrastructure.Interfaces;

namespace Training.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [ChildActionOnly]
        public PartialViewResult Categories() => PartialView("_CategoriesPartial", categoryRepository.Categories);
    }
}