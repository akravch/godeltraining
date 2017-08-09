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
        // TODO: This also gets all the posts even though we don't need it here
        public PartialViewResult Categories() => PartialView("_CategoriesPartial", categoryRepository.Categories);
    }
}