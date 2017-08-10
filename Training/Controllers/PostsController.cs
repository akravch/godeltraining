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
    public class PostsController : Controller
    {
        private readonly IPostService postService;
        private readonly ICategoryService categoryService;

        public PostsController(IPostService postService, ICategoryService categoryService)
        {
            this.postService = postService;
            this.categoryService = categoryService;
        }

        public ActionResult Index(string category = null)
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

        public ActionResult New()
        {
            var postViewModel = new EditPostViewModel
            {
                Categories = GetCategories()
            };

            return View(postViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(EditPostViewModel postViewModelViewModel)
        {
            ActionResult result;

            var isModelValid = ModelState.IsValid;

            if (isModelValid)
            {
                var selectedCategory = categoryService.GetCategoryById(postViewModelViewModel.SelectedCategoryId);

                if (selectedCategory != null)
                {
                    postService.AddNewPost(new Post
                    {
                        Title = postViewModelViewModel.Title,
                        Body = postViewModelViewModel.Body,
                        Category = selectedCategory,
                        CreationDateTimeUtc = DateTime.UtcNow
                    });
                    result = RedirectToAction("Index", new { category = selectedCategory.Name });
                }
                else
                {
                    ModelState.AddModelError(nameof(postViewModelViewModel.SelectedCategoryId), "Selected category does not exist");
                    postViewModelViewModel.Categories = GetCategories();
                    result = View(postViewModelViewModel);
                }
            }
            else
            {
                postViewModelViewModel.Categories = GetCategories();
                result = View(postViewModelViewModel);
            }

            return result;
        }

        public ActionResult Edit(string id)
        {
            Guid guid;

            if (Guid.TryParse(id, out guid))
            {
                var post = postService.GetPostById(guid);

                if (post != null)
                {
                    var postViewModel = new EditPostViewModel
                    {
                        Title = post.Title,
                        Body = post.Body,
                        Categories = GetCategories(),
                        SelectedCategoryId = post.Category.Id
                    };

                    return View(postViewModel);
                }
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(EditPostViewModel postViewModelViewModel)
        {
            ActionResult result;

            if (ModelState.IsValid)
            {
                var selectedCategory = categoryService.GetCategoryById(postViewModelViewModel.SelectedCategoryId);

                if (selectedCategory != null)
                {
                    var post = new Post
                    {
                        Id = postViewModelViewModel.Id,
                        Title = postViewModelViewModel.Title,
                        Body = postViewModelViewModel.Body,
                        Category = selectedCategory
                    };

                    postService.UpdatePost(post);
                    result = RedirectToAction("Index", new { category = selectedCategory.Name });
                }
                else
                {
                    ModelState.AddModelError(nameof(EditPostViewModel.SelectedCategoryId), "Selected category does not exist");
                    postViewModelViewModel.Categories = GetCategories();
                    result = View(postViewModelViewModel);
                }
            }
            else
            {
                postViewModelViewModel.Categories = GetCategories();
                result = View(postViewModelViewModel);
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