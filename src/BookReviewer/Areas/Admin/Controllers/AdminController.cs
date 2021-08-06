namespace BookReviewer.Areas.Admin.Controllers
{
    using BookReviewer.Models.Authors;
    using BookReviewer.Models.Books;
    using BookReviewer.Services.Authors;
    using BookReviewer.Services.Books;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class AdminController : Controller
    {
        private readonly IAuthorService authors;
        private readonly IBookService books;

        public AdminController(IAuthorService authors,
            IBookService books)
        {
            this.authors = authors;
            this.books = books;
        }

        public IActionResult AddAuthor() => View();

        [HttpPost]
        public IActionResult AddAuthor(AddAuthorFormModel author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            authors.Create(author.Name,
                author.DateOfBirth,
                author.Details,
                author.PictureUrl);

            return Redirect("/");
        }

        public IActionResult AddBook() => View(new AddBookFormModel
        {
            Genres = books.GetGenres(),
            Authors = books.GetAuthors()
        });

        [HttpPost]
        public IActionResult AddBook(AddBookFormModel book)
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

            return Redirect("/");
        }
    }
}
