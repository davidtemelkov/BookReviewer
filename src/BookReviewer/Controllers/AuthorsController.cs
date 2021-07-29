namespace BookReviewer.Controllers
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Authors;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

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

        public IActionResult Details(string id)
        {
            var author = this.data.Authors.Where(a => a.Id == int.Parse(id));

            var authorDetails = author.Select(a => new AuthorDetailsViewModel
            {
                Name = a.Name,
                DateOfBirth = a.DateOfBirth.ToString("dd.MM.yyyy"),
                Details = a.Details,
                PictureUrl = a.PictureUrl,
                Books = new List<Book>()
            })
                .FirstOrDefault();

            foreach (var book in this.data.Books.Where(b => b.AuthorId == int.Parse(id)))
            {
                authorDetails.Books.Add(book);
            }

            return View(authorDetails);
        }
    }
}
