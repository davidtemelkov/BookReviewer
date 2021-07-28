namespace BookReviewer.Infrastructure
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Globalization;
    using System.Linq;

    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ApplicationDbContext>();

            SeedGenres(data);
            SeedAuthors(data);
            SeedBooks(data);
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

        public static void SeedAuthors(ApplicationDbContext data)
        {
            if (data.Authors.Any())
            {
                return;
            }

            data.Authors.AddRange(new[]
            {
            new Author { Name = "Agatha Christie", DateOfBirth = DateTime.ParseExact("15.09.1890", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/c/cf/Agatha_Christie.png",
                Details = "Dame Agatha Mary Clarissa Christie, Lady Mallowan, DBE (née Miller; 15 September 1890 – 12 January 1976) was an English writer known for her 66 detective novels and 14 short story collections, particularly those revolving around fictional detectives Hercule Poirot and Miss Marple."},

            new Author { Name = "John Steinbeck", DateOfBirth = DateTime.ParseExact("27.02.1902", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d7/John_Steinbeck_1939_%28cropped%29.jpg/220px-John_Steinbeck_1939_%28cropped%29.jpg",
                Details = "John Ernst Steinbeck Jr. (/ˈstaɪnbɛk/; February 27, 1902 – December 20, 1968) was an American author and the 1962 Nobel Prize in Literature winner \"for his realistic and imaginative writings, combining as they do sympathetic humour and keen social perception.\"[2] He has been called \"a giant of American letters.\"" }
        });

            data.SaveChanges();
        }

        public static void SeedBooks(ApplicationDbContext data)
        {
            if (data.Books.Any())
            {
                return;
            }

            var book = new Book
            {
                Title = "And Then There Were None",
                AuthorId = 3,
                Pages = 264,
                CoverUrl = "https://agathachristie.imgix.net/hcuk-paperback/And-Then-There-Were-None.JPG?auto=compress,format&fit=clip&q=65&w=400",
                DateAdded = DateTime.ParseExact("03.03.2004", "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Description = "First, there were ten—a curious assortment of strangers summoned as weekend guests to a little private island off the coast of Devon."
            };

            var bookGenre = new BookGenre { Book = book, GenreId = 18 };
            var bookGenre2 = new BookGenre { Book = book, GenreId = 20 };

            book.BookGenres.Add(bookGenre);
            book.BookGenres.Add(bookGenre2);

            data.Books.Add(book);

            data.SaveChanges();
        }
    }
}
