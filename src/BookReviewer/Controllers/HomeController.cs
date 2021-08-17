namespace BookReviewer.Controllers
{
    using BookReviewer.Models;
    using BookReviewer.Models.Books;
    using BookReviewer.Services.Books;
    using BookReviewer.Services.Emails;
    using BookReviewer.Services.Genres;

    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly IEmailService emails;
        private readonly IBookService books;
        private readonly IGenreService genres;

        public HomeController(IEmailService emails,
            IBookService books,
            IGenreService genres)
        {
            this.emails = emails;
            this.books = books;
            this.genres = genres;
        }

        public IActionResult Index() => View(new BookQueryViewModel
        {
            Genres = this.genres.GetGenres(),
            Books = this.books.GetAcceptedBooks()
        });

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
}
