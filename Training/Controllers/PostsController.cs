using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Training.Domain;
using Training.Dto;
using Training.Extensions;
using Training.Infrastructure.Interfaces;
using Training.Models;
using Training.ViewModels;

namespace Training.Controllers
{
    public class PostsController : Controller
    {
        private const int PageSize = 3;
        private readonly IPostService postService;
        private readonly ICategoryService categoryService;

        public PostsController(IPostService postService, ICategoryService categoryService)
        {
            this.postService = postService;
            this.categoryService = categoryService;
        }

        public ActionResult Index(string category = null, int page = 1)
        {
            var pageOffset = (page - 1) * PageSize;
            int postCount;
            ICollection<Post> posts;

            if (!string.IsNullOrEmpty(category))
            {
                posts = postService.GetPostsByCategory(category, pageOffset, PageSize);
                postCount = postService.Count(category);
            }
            else
            {
                posts = postService.GetPosts(pageOffset, PageSize);
                postCount = postService.Count();
            }

            var postDtos = posts.Select(post => new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                CreationDateTimeUtc = post.CreationDateTimeUtc
            }).ToList();
            var totalPages = (int)Math.Ceiling(postCount / (double)PageSize);
            var pageViewModel = new PostPageViewModel
            {
                CurrentPage = page,
                TotalPages = totalPages,
                CategoryName = category,
                Posts = postDtos
            };

            return View(pageViewModel);
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
            var changedPost = ChangePost(postViewModelViewModel, post => postService.AddNewPost(post));

            if (changedPost != null)
            {
                TempData[TempDataConstants.PostChanged] = "The post has been succesfully published";
                result = RedirectToAction("Index", new { category = changedPost.Category.Name });
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
            var post = GetPostByStringId(id);

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

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(EditPostViewModel postViewModelViewModel)
        {
            ActionResult result;
            var changedPost = ChangePost(postViewModelViewModel, post => postService.UpdatePost(post));

            if (changedPost != null)
            {
                TempData[TempDataConstants.PostChanged] = "The post has been succesfully saved";
                result = RedirectToAction("Index", new { category = changedPost.Category.Name });
            }
            else
            {
                postViewModelViewModel.Categories = GetCategories();
                result = View(postViewModelViewModel);
            }

            return result;
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            var post = GetPostByStringId(id);

            if (post != null)
            {
                postService.DeletePost(post);
                TempData[TempDataConstants.PostChanged] = "The post has been succesfully deleted";

                return Redirect(Request.UrlReferrer.AbsolutePath);
            }

            return HttpNotFound();
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

        private Post GetPostByStringId(string id)
        {
            Guid guid;

            return Guid.TryParse(id, out guid) ? postService.GetPostById(guid) : null;
        }

        private void AddMissingCategoryError(string propertyName)
        {
            ModelState.AddModelError(propertyName, "Selected category does not exist");
        }

        private Post ChangePost(EditPostViewModel viewModel, Action<Post> changeAction)
        {
            Post changedPost = null;

            if (ModelState.IsValid)
            {
                var selectedCategory = categoryService.GetCategoryById(viewModel.SelectedCategoryId);

                if (selectedCategory != null)
                {
                    changedPost = viewModel.ToPost(selectedCategory);
                    changeAction(changedPost);
                }
                else
                {
                    AddMissingCategoryError(nameof(viewModel.SelectedCategoryId));
                }
            }

            return changedPost;
        }
    }
}