namespace BookReviewer.Controllers
{
    using BookReviewer.Data;
    using BookReviewer.Models.Books;
    using BookReviewer.Services.Books;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using BookReviewer.Infrastructure;
    using BookReviewer.Services.Genres;

    public class BooksController : Controller
    {
        private readonly IBookService books;
        private readonly BookReviewerDbContext data;
        private readonly IGenreService genres;

        public BooksController(IBookService books,
            BookReviewerDbContext data,
            IGenreService genres)
        {
            this.books = books;
            this.data = data;
            this.genres = genres;
        }

        public IActionResult Add() => View(new BookFormModel
        {
            Genres = this.genres.GetGenres()
        });

        [HttpPost]
        public IActionResult Add(BookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Genres = this.genres.GetGenres();

                return View(book);
            }

            var currentUser = this.data.Users
                .FirstOrDefault(u => u.Id == User.Id());

            this.books.UserCreate(book.Title,
                currentUser,
                book.CoverUrl,
                book.Description,
                book.Pages,
                book.YearPublished,
                book.BookGenres
                );

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(string id)
        {
            var book = this.books.BookDetails(id);

            var editBookForm = new BookFormModel
            {
                Title = book.Title,
                Author = book.AuthorName,
                CoverUrl = book.CoverUlr,
                Pages = book.Pages,
                YearPublished = book.YearPublished,
                Description = book.Description,
                Genres = this.genres.GetGenres()
            };

            return View(editBookForm);
        }

        [HttpPost]
        public IActionResult Edit(string id, BookFormModel editedBook)
        {
            this.books.Edit(id, editedBook);

            return Redirect($"/Books/Details/{id}");
        }

        public IActionResult All() => View(books.GetBooks());

        public IActionResult Details(string id) => View(books.BookDetails(id));
    }
}
