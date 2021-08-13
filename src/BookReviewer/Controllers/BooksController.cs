namespace BookReviewer.Controllers
{
    using BookReviewer.Data;
    using BookReviewer.Models.Books;
    using BookReviewer.Services.Books;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using BookReviewer.Infrastructure;
    using BookReviewer.Services.Genres;
    using Microsoft.AspNetCore.Authorization;
    using BookReviewer.Services.Authors;

    public class BooksController : Controller
    {
        private readonly IBookService books;
        private readonly BookReviewerDbContext data;
        private readonly IGenreService genres;
        private readonly IAuthorService authors;

        public BooksController(IBookService books,
            BookReviewerDbContext data,
            IGenreService genres,
            IAuthorService authors)
        {
            this.books = books;
            this.data = data;
            this.genres = genres;
            this.authors = authors;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.authors.IsAuthor(User.Id()))
            {
                return RedirectToAction("Add", "Authors");
            }

            return View(new BookFormModel
            {
                Genres = this.genres.GetGenres()
            });
        }

        [Authorize]
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

        [Authorize]
        public IActionResult Edit(string id)
        {
            if (!this.authors.IsAuthorOfBook(User.Id(), id) && !User.IsAdmin())
            {
                return Unauthorized();
            }

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

        public IActionResult All() => View(new BookQueryViewModel
        {
            Genres = this.genres.GetGenres(),
            Books = this.books.GetAcceptedBooks()
        });

        public IActionResult Details(string id) => View(books.BookDetails(id));

        public IActionResult Search(string searchTerm, string genre)
        {
            var books = this.books.GetAcceptedBooks();

            if (searchTerm == null && genre == "All")
            {
                return Redirect("/Books/All");
            }
            else if (searchTerm == null && genre != "All")
            {
                return View(books.Where(b => b.Genres.ToLower().Contains(genre.ToLower())));
            }
            else if(searchTerm != null && genre == "All")
            {
                return View(books.Where(b => b.Title.ToLower().Contains(searchTerm.ToLower())));
            }
            else
            {
                return View(books.Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()) && b.Genres.ToLower().Contains(genre.ToLower())));
            }
        }
    }
}

