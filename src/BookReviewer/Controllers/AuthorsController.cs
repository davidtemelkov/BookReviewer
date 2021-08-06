namespace BookReviewer.Controllers
{
    using BookReviewer.Models.Authors;
    using BookReviewer.Services.Authors;
    using Microsoft.AspNetCore.Mvc;

    public class AuthorsController : Controller
    {
        private readonly IAuthorService authors;

        public AuthorsController(IAuthorService authors)
        {
            this.authors = authors;
        }

        public IActionResult Details(string id) => View(authors.Details(id));
    }
}
