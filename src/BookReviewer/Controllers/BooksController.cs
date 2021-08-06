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

        public IActionResult Details(string id) => View(books.BookDetails(id));
    }
}
