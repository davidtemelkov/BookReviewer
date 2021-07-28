namespace BookReviewer.Controllers
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Authors;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Globalization;

    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext data;

        public AuthorsController(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddAuthorFormModel author)
        {
            if(!ModelState.IsValid)
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
