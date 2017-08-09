using System;
using System.Linq;
using System.Web.Mvc;
using Training.Dto;
using Training.Infrastructure.Interfaces;
using Training.ViewModels;

namespace Training.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostRepository postRepository;
        private readonly ICategoryRepository categoryRepository;

        // TODO: Should I inject services instead?
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

        public ActionResult NewPost()
        {
            var categories = categoryRepository.Categories.ToList();
            var postViewModel = new NewPost
            {
                Categories = categories
            };

            return View(postViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPost(NewPost postViewModel)
        {
            ActionResult result;

            var isModelValid = ModelState.IsValid;

            if (isModelValid)
            {
                var selectedCategory = categoryRepository.Categories.FirstOrDefault(category => category.Id == postViewModel.SelectedCategoryId);

                if (selectedCategory != null)
                {
                    postRepository.Add(new PostDto
                    {
                        Body = postViewModel.Body,
                        Category = selectedCategory,
                        CreationDateTimeUtc = DateTime.UtcNow
                    });
                    result = RedirectToAction("Posts", new { category = selectedCategory.Name });
                }
                else
                {
                    ModelState.AddModelError(nameof(postViewModel.SelectedCategoryId), "Selected category does not exist");

                    // TODO: Is there a way to preserve the categories without requesting them from the db again?
                    postViewModel.Categories = categoryRepository.Categories.ToList();
                    result = View(postViewModel);
                }
            }
            else
            {
                postViewModel.Categories = categoryRepository.Categories.ToList();
                result = View(postViewModel);
            }

            return result;
        }
    }
}