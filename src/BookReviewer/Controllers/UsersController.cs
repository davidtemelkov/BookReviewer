namespace BookReviewer.Controllers
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Users;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Security.Claims;

    public class UsersController : Controller
    {
        private readonly ApplicationDbContext data;

        public UsersController(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IActionResult Profile(string id)
        {
            var profile = new UserProfileViewModel
            {
                Id = id,
                Username = User.FindFirstValue(ClaimTypes.Name),
                ProfilePictureUrl = this.data.Users
                .Where(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .FirstOrDefault()
                .ProfilePicture,
                AuthorId = this.data.Users
                .Where(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Select(u => u.AuthorId)
                .FirstOrDefault()
            };

            return View(profile);
        }

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

            var authorData = new Author
            {
                Name = author.Name,
                DateOfBirth = DateTime.ParseExact(author.DateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Details = author.Details,
                PictureUrl = author.PictureUrl
            };

            this.data.Authors.Add(authorData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
