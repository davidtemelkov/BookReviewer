namespace BookReviewer.Controllers
{
    using BookReviewer.Data;
    using BookReviewer.Models.Users;
    using BookReviewer.Services.Users;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using BookReviewer.Infrastructure;
    using BookReviewer.Services.Books;
    using BookReviewer.Services.Authors;
    using BookReviewer.Services.Reviews;
    using BookReviewer.Models.Reviews;
    using BookReviewer.Models.Books;

    public class UsersController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IUserService users;
        private readonly IBookService books;
        private readonly IAuthorService authors;
        private readonly IReviewService reviews;

        public UsersController(ApplicationDbContext data, IUserService users,
            IBookService books,
            IAuthorService authors,
            IReviewService reviews)
        {
            this.data = data;
            this.users = users;
            this.books = books;
            this.authors = authors;
            this.reviews = reviews;
        }

        public IActionResult Profile(string id) => View(users.Profile(id));

        public IActionResult BecomeAnAuthor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BecomeAnAuthor(AuthorFormModel author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            users.CreateAuthor(author.Name,
                author.DateOfBirth,
                author.Details,
                author.PictureUrl,
                User.Id());

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddBook() => View(new BookFormModel
        {
            Genres = books.GetGenres()
        });

        [HttpPost]
        public IActionResult AddBook(BookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Genres = books.GetGenres();

                return View(book);
            }

            var currentUser = this.data.Users
                .FirstOrDefault(u => u.Id == User.Id());

            books.UserCreate(book.Title,
                currentUser,
                book.CoverUrl,
                book.Description,
                book.Pages,
                book.YearPublished,
                book.BookGenres
                );

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Reviews(string id) => View(users.AllUserReviews(id));
        
        public IActionResult EditBookDetails(string id)
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
                Genres = books.GetGenres()
            };

            return View(editBookForm);
        }

        [HttpPost]
        public IActionResult EditBookDetails(string id, BookFormModel editedBook)
        {
            this.users.EditBook(id, editedBook);

            return Redirect($"/Books/Details/{id}");
        }

        public IActionResult EditAuthorDetails(string id)
        {
            var author = this.authors.Details(id);

            var editAuthorForm = new AuthorFormModel
            {
                Name = author.Name,
                DateOfBirth = author.DateOfBirth,
                Details = author.Details,
                PictureUrl = author.PictureUrl
            };

            return View(editAuthorForm);
        }

        [HttpPost]
        public IActionResult EditAuthorDetails(string id, AuthorFormModel editedAuthor)
        {
            users.EditAuthor(id, editedAuthor);

            return Redirect($"/Authors/Details/{id}");
        }

        public IActionResult EditReview(string id)
        {
            var review = this.data.Reviews.Find(int.Parse(id));

            var editReviewForm = new ReviewFormModel 
            {
                Stars = review.Stars,
                Text = review.Text
            };

            return View(editReviewForm);
        }

        [HttpPost]
        public IActionResult EditReview(string id, ReviewFormModel editedReview)
        {
            var currentUserId = User.Id();
            this.users.EditReview(id, editedReview);

            return Redirect($"/Users/Reviews/{currentUserId}");
        }

        public IActionResult DeleteReview(string id)
        {
            var userId = User.Id();

            var review = this.data.Reviews.Find(int.Parse(id));
            this.data.Reviews.Remove(review);
            this.data.SaveChanges();

            return Redirect($"/Users/Reviews/{userId}");
        }

        //public IActionResult Lists(string id)
        //{
        //    return View();
        //}

        //public IActionResult CreateList()
        //{ 
        //    return View();
        //}
    }
}
