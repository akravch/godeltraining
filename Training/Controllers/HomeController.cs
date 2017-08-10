using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Training.Domain;
using Training.Dto;
using Training.Infrastructure.Interfaces;
using Training.ViewModels;

namespace Training.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService postService;
        private readonly ICategoryService categoryService;

        public HomeController(IPostService postService, ICategoryService categoryService)
        {
            this.postService = postService;
            this.categoryService = categoryService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Posts(string category = null)
        {
            var posts = !string.IsNullOrEmpty(category) ? postService.GetPostsByCategory(category) : postService.GetPosts();
            var postDtos = posts.Select(post => new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                CreationDateTimeUtc = post.CreationDateTimeUtc
            }).ToList();

            return View(postDtos);
        }

        public ActionResult NewPost()
        {
            var postViewModel = new NewPost
            {
                Categories = GetCategories()
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
                var selectedCategory = categoryService.GetCategoryById(postViewModel.SelectedCategoryId);

                if (selectedCategory != null)
                {
                    postService.AddNewPost(new Post
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
                    postViewModel.Categories = GetCategories();
                    result = View(postViewModel);
                }
            }
            else
            {
                postViewModel.Categories = GetCategories();
                result = View(postViewModel);
            }

            return result;
        }

        [ChildActionOnly]
        public PartialViewResult Categories() => PartialView("_CategoriesPartial", GetCategories());

        private IList<CategoryDto> GetCategories()
        {
            return categoryService.GetCategories().Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            }).ToList();
        }
    }
}