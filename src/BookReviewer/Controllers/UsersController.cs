namespace BookReviewer.Controllers
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Books;
    using BookReviewer.Models.Users;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Claims;

    public class UsersController : Controller
    {
        private readonly ApplicationDbContext data;

        public UsersController(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IActionResult Profile(string id)
        {
            var profile = new UserProfileViewModel
            {
                Id = id,
                Username = User.FindFirstValue(ClaimTypes.Name),
                ProfilePictureUrl = this.data.Users
                .Where(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefault()
                .ProfilePicture,
                AuthorId = this.data.Users
                .Where(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefault()
                .AuthorId
            };

            return View(profile);
        }

        public IActionResult BecomeAnAuthor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BecomeAnAuthor(BecomeAnAuthorFormModel author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            var authorData = new Author
            {
                Name = author.Name,
                DateOfBirth = DateTime.ParseExact(author.DateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Details = author.Details,
                PictureUrl = author.PictureUrl
            };

            this.data.Authors.Add(authorData);
            this.data.SaveChanges();

            this.data.Users
                .FirstOrDefault(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .AuthorId = authorData.Id;
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddBook() => View(new UserAddBookFormModel
        {
            Genres = this.GetGenres()
        });

        [HttpPost]
        public IActionResult AddBook(UserAddBookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Genres = this.GetGenres();

                return View(book);
            }

            var currentUser = this.data.Users
                .FirstOrDefault(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            var bookData = new Book
            {
                Title = book.Title,
                Author = data.Authors.FirstOrDefault(a => a.Id == currentUser.AuthorId),
                CoverUrl = book.CoverUrl,
                Description = book.Description,
                Pages = book.Pages,
                YearPublished = book.YearPublished
            };

            foreach (var genre in book.BookGenres)
            {
                bookData.BookGenres.Add(new BookGenre { Book = bookData, Genre = data.Genres.FirstOrDefault(g => g.Name == genre) });
            }

            data.Books.Add(bookData);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<BookGenresViewModel> GetGenres()
        {
            return this.data
                    .Genres
                    .Select(g => new BookGenresViewModel
                    {
                        Id = g.Id,
                        Name = g.Name
                    })
                    .ToList();
        }
    }
}
