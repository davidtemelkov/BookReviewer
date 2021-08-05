namespace BookReviewer.Controllers
{
    using BookReviewer.Data;
    using BookReviewer.Models.Users;
    using BookReviewer.Services.Users;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using BookReviewer.Infrastructure;
    using BookReviewer.Services.Books;

    public class UsersController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IUserService users;
        private readonly IBookService books;

        public UsersController(ApplicationDbContext data, IUserService users,
            IBookService books)
        {
            this.data = data;
            this.users = users;
            this.books = books;
        }

        public IActionResult Profile(string id) => View(users.Profile(id));

        public IActionResult BecomeAnAuthor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BecomeAnAuthor(BecomeAnAuthorFormModel author)
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

        public IActionResult AddBook() => View(new UserAddBookFormModel
        {
            Genres = books.GetGenres()
        });

        [HttpPost]
        public IActionResult AddBook(UserAddBookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Genres = books.GetGenres();

                return View(book);
            }

            var currentUser = this.data.Users
                .FirstOrDefault(u => u.Id == User.Id());

            users.AddBook(book.Title,
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
