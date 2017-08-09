using System;
using System.Linq;
using System.Web.Mvc;
using Training.Infrastructure.Interfaces;

namespace Training.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostRepository postRepository;
        private readonly ICategoryRepository categoryRepository;

        public HomeController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            this.postRepository = postRepository;
            this.categoryRepository = categoryRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Posts(string category = null)
        {
            ActionResult result;

            if (!string.IsNullOrEmpty(category))
            {
                var foundCategory = categoryRepository.Categories.FirstOrDefault(existingCategory => existingCategory.Name.Equals(category, StringComparison.OrdinalIgnoreCase));

                result = foundCategory != null ? View(foundCategory.Posts) : (ActionResult)HttpNotFound();
            }
            else
            {
                result = View(postRepository.Posts.ToList());
            }

            return result;
        }
    }
}