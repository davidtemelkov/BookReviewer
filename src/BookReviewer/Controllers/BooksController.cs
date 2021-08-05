namespace BookReviewer.Controllers
{
    using BookReviewer.Models.Books;
    using BookReviewer.Services.Books;
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : Controller
    {
        private readonly IBookService books;

        public BooksController(IBookService books)
        {
            this.books = books;
        }

        public IActionResult All() => View(books.GetBooks());

        public IActionResult Add() => View(new AddBookFormModel
        {
            Genres = books.GetGenres(),
            Authors = books.GetAuthors()
        });

        [HttpPost]
        public IActionResult Add(AddBookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Genres = books.GetGenres();
                book.Authors = books.GetAuthors();

                return View(book);
            }

            books.Create(book.Title,
                book.Author,
                book.CoverUrl,
                book.Description,
                book.Pages,
                book.YearPublished,
                book.BookGenres);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(string id) => View(books.BookDetails(id));
    }
}
