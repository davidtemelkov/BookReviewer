namespace BookReviewer.Infrastructure
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;

    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ApplicationDbContext>();

            SeedGenres(data);
            data.Database.Migrate();

            return app;
        }

        public static void SeedGenres(ApplicationDbContext data)
        {
            if (data.Genres.Any())
            {
                return;
            }

            data.Genres.AddRange(new[]
            {
            new Genre { Name = "Romance"},
            new Genre { Name = "Mystery"},
            new Genre { Name = "Fantasy"},
            new Genre { Name = "Fiction"},
            new Genre { Name = "Thriller"},
            new Genre { Name = "Horror"},
            new Genre { Name = "Young adult"},
            new Genre { Name = "Adult"},
            new Genre { Name = "Children"},
            new Genre { Name = "Self-help"},
            new Genre { Name = "Religious"},
            new Genre { Name = "Autobiography"},
            new Genre { Name = "Historical"},
            new Genre { Name = "Non-fiction"},
            new Genre { Name = "Classics"},
            new Genre { Name = "Politics"}
        });

            data.SaveChanges();
        }
    }
}
