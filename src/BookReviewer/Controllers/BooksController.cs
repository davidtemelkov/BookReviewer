namespace BookReviewer.Controllers
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Books;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
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
                return View(book);
            }

            //var bookData = new Book
            //{
            //    Title = book.Title,
            //    Author = data.Authors.FirstOrDefault(a=> a.Name == book.Author),

            //};

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
