using System;

namespace Training.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Training.Domain.DataContext";
        }

        protected override void Seed(DataContext context)
        {
            var musicCategory = new Category { Name = "Music" };
            var movieCategory = new Category { Name = "Movie" };

            context.Categories.AddOrUpdate(category => category.Id,
                musicCategory,
                movieCategory,
                new Category { Name = "Sports" });

            context.Posts.AddOrUpdate(post => post.Id,
                new Post
                {
                    Title = "Music post 1",
                    Body = "This is a music post 1.",
                    CreationDateTimeUtc = new DateTime(2017, 2, 22, 12, 31, 00, DateTimeKind.Utc),
                    Category = musicCategory
                },
                new Post
                {
                    Title = "Music post 2",
                    Body = "This is a music post 2.",
                    CreationDateTimeUtc = new DateTime(2017, 3, 2, 22, 14, 00, DateTimeKind.Utc),
                    Category = musicCategory
                },
                new Post
                {
                    Title = "Movie post 1",
                    Body = "This is a movie post 1.",
                    CreationDateTimeUtc = new DateTime(2017, 1, 22, 2, 15, 00, DateTimeKind.Utc),
                    Category = movieCategory
                },
                new Post
                {
                    Title = "Movie post 2",
                    Body = "This is a movie post 2.",
                    CreationDateTimeUtc = new DateTime(2017, 5, 15, 22, 7, 00, DateTimeKind.Utc),
                    Category = movieCategory
                });
        }
    }
}
