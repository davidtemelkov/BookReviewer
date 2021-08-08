namespace BookReviewer.Controllers
{
    using BookReviewer.Services.Authors;
    using Microsoft.AspNetCore.Mvc;
    using BookReviewer.Infrastructure;
    using BookReviewer.Models.Authors;
    using Microsoft.AspNetCore.Authorization;

    public class AuthorsController : Controller
    {
        private readonly IAuthorService authors;

        public AuthorsController(IAuthorService authors)
        {
            this.authors = authors;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AuthorFormModel author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            this.authors.UserCreate(author.Name,
                author.DateOfBirth,
                author.Details,
                author.PictureUrl,
                User.Id());

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Edit(string id)
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

        [Authorize]
        [HttpPost]
        public IActionResult Edit(string id, AuthorFormModel editedAuthor)
        {
            this.authors.Edit(id, editedAuthor);

            return Redirect($"/Authors/Details/{id}");
        }
        public IActionResult Details(string id) => View(authors.Details(id));
    }
}
