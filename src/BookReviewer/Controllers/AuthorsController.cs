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

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddAuthorFormModel author)
        {
            if(!ModelState.IsValid)
            {
                return View(author);
            }

            authors.Create(author.Name,
                author.DateOfBirth,
                author.Details,
                author.PictureUrl);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(string id) => View(authors.Details(id));
    }
}
