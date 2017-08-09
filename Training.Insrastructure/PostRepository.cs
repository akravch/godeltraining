using System;
using System.Linq;
using Training.Domain;
using Training.Dto;
using Training.Infrastructure.Interfaces;

namespace Training.Insrastructure
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext context;

        public PostRepository(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<PostDto> Posts => context.Posts.Select(post => new PostDto
        {
            Id = post.Id,
            Body = post.Body,
            CreationDateTimeUtc = post.CreationDateTimeUtc,
            Category = new CategoryDto
            {
                Id = post.Category.Id,
                Name = post.Category.Name
            }
        });

        public void Add(PostDto postDto)
        {
            var categoryId = postDto.Category.Id;
            var postCategory = context.Categories.FirstOrDefault(category => category.Id == categoryId);

            if (postCategory != null)
            {
                context.Posts.Add(new Post
                {
                    Body = postDto.Body,
                    CreationDateTimeUtc = postDto.CreationDateTimeUtc,
                    Category = postCategory
                });
                context.SaveChanges();
            }
        }

        public void Update(PostDto postDto)
        {
            var existingPost = postDto.Id != default(Guid) ? context.Posts.FirstOrDefault(post => post.Id == postDto.Id) : null;

            if (existingPost != null)
            {
                existingPost.Body = postDto.Body;
                existingPost.CreationDateTimeUtc = postDto.CreationDateTimeUtc;

                if (existingPost.Category.Id != postDto.Category.Id)
                {
                    var newCategory = context.Categories.FirstOrDefault(category => category.Id == postDto.Category.Id);

                    if (newCategory != null)
                    {
                        existingPost.Category = newCategory;
                    }
                }

                context.SaveChanges();
            }
        }

        public void Remove(PostDto postDto)
        {
            var postToRemove = postDto.Id != default(Guid) ? context.Posts.FirstOrDefault(post => post.Id == postDto.Id) : null;

            if (postToRemove != null)
            {
                context.Posts.Remove(postToRemove);
                context.SaveChanges();
            }
        }
    }
}
