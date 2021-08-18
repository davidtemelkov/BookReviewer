namespace BookReviewer.Controllers
{
    using BookReviewer.Data;
    using BookReviewer.Models.Books;
    using BookReviewer.Services.Books;
    using BookReviewer.Infrastructure;
    using BookReviewer.Services.Genres;
    using BookReviewer.Services.Authors;

    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    public class BooksController : Controller
    {
        private readonly IBookService books;
        private readonly BookReviewerDbContext data;
        private readonly IGenreService genres;
        private readonly IAuthorService authors;
        private readonly IMapper mapper;

        public BooksController(IBookService books,
            BookReviewerDbContext data,
            IGenreService genres,
            IAuthorService authors,
            IMapper mapper)
        {
            this.books = books;
            this.data = data;
            this.genres = genres;
            this.authors = authors;
            this.mapper = mapper;
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

            this.books.UserCreate(currentUser, book);

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

            var editBookForm = this.mapper.Map<BookFormModel>(book);
            editBookForm.Genres = this.genres.GetGenres();

            return View(editBookForm);
        }

        [HttpPost]
        public IActionResult Edit(string id, BookFormModel editedBook)
        {
            if (!ModelState.IsValid)
            {
                editedBook.Genres = this.genres.GetGenres();

                return View(editedBook);
            }

            this.books.Edit(id, editedBook);

            return Redirect($"/Books/Details/{id}");
        }

        public IActionResult Details(string id) => View(books.BookDetails(id));

        public IActionResult Search(string searchTerm, string genre)
        {
            if (this.books.SearchBooks(searchTerm, genre) == null)
            {
                return Redirect("/");
            }

            return View((this.books.SearchBooks(searchTerm, genre)));
        }
    }
}

