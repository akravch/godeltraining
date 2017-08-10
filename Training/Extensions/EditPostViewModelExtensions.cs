using Training.Domain;
using Training.ViewModels;

namespace Training.Extensions
{
    public static class EditPostViewModelExtensions
    {
        public static Post ToPost(this EditPostViewModel viewModel, Category category)
        {
            return new Post
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Body = viewModel.Body,
                Category = category
            };
        }
    }
}