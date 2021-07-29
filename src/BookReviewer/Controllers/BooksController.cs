namespace BookReviewer.Controllers
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Books;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class BooksController : Controller
    {
        private readonly ApplicationDbContext data;

        public BooksController(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IActionResult Add() => View(new AddBookFormModel
        {
            Genres = this.GetGenres()
        });

        [HttpPost]
        public IActionResult Add(AddBookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Genres = this.GetGenres();

                return View(book);
            }

            var bookData = new Book
            {
                Title = book.Title,
                Author = data.Authors.FirstOrDefault(a => a.Name == book.Author),
                CoverUrl = book.CoverUrl,
                Description = book.Description,
                Pages = book.Pages,
                ReleaseDate = DateTime.ParseExact(book.ReleaseDate, "yyyy", CultureInfo.InvariantCulture)
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
