namespace BookReviewer.Areas.Admin.Controllers
{
    using BookReviewer.Data;
    using BookReviewer.Models.Authors;
    using BookReviewer.Models.Books;
    using BookReviewer.Services.Authors;
    using BookReviewer.Services.Books;
    using BookReviewer.Services.Genres;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class AdminController : Controller
    {
        private readonly IAuthorService authors;
        private readonly IBookService books;
        private readonly IGenreService genres;
        private readonly BookReviewerDbContext data;

        public AdminController(IAuthorService authors,
            IBookService books,
            IGenreService genres,
            BookReviewerDbContext data)
        {
            this.authors = authors;
            this.books = books;
            this.genres = genres;
            this.data = data;
        }

        public IActionResult AddAuthor() => View();

        [HttpPost]
        public IActionResult AddAuthor(AuthorFormModel author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            this.authors.AdminCreate(author.Name,
                author.DateOfBirth,
                author.Details,
                author.PictureUrl);

            return Redirect("/");
        }

        public IActionResult AddBook() => View(new BookFormModel
        {
            Genres = this.genres.GetGenres(),
            Authors = this.authors.GetAuthors()
        });

        [HttpPost]
        public IActionResult AddBook(BookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Genres = this.genres.GetGenres();
                book.Authors = this.authors.GetAuthors();

                return View(book);
            }

            this.books.AdminCreate(book.Title,
                book.Author,
                book.CoverUrl,
                book.Description,
                book.Pages,
                book.YearPublished,
                book.BookGenres);

            return Redirect("/");
        }

        public IActionResult AcceptNewBooks() => View(this.books.GetNonAcceptedBooks());

        public IActionResult AcceptBook(string id)
        {
            var book = this.data.Books.Find(int.Parse(id));

            book.IsAccepted = true;
            this.data.SaveChanges();

            return RedirectToAction("AcceptNewBooks", "Admin");
        }

        public IActionResult DenyBook(string id)
        {
            return RedirectToAction("AcceptNewBooks", "Admin");
        }
    }
}
