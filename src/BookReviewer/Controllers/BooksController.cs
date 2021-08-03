﻿namespace BookReviewer.Controllers
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Books;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class BooksController : Controller
    {
        private readonly ApplicationDbContext data;

        public BooksController(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IActionResult Add() => View(new AddBookFormModel
        {
            Genres = this.GetGenres(),
            Authors = this.GetAuthors()
        });

        [HttpPost]
        public IActionResult Add(AddBookFormModel book)
        {
            if (!ModelState.IsValid)
            {
                book.Genres = this.GetGenres();
                book.Authors = this.GetAuthors();

                return View(book);
            }

            var bookData = new Book
            {
                Title = book.Title,
                Author = data.Authors.FirstOrDefault(a => a.Name == book.Author),
                CoverUrl = book.CoverUrl,
                Description = book.Description,
                Pages = book.Pages,
                YearPublished = book.YearPublished
            };

            foreach (var genre in book.BookGenres)
            {
                bookData.BookGenres.Add(new BookGenre { Book = bookData, Genre = data.Genres.FirstOrDefault(g => g.Name == genre) });
            }

            data.Books.Add(bookData);
            data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(string id)
        {
            var book = this.data.Books
                .Where(b => b.Id == int.Parse(id))
                .Include(b => b.Reviews)
                .ThenInclude(r => r.User);

            var bookDetails = book.Select(b => new BookDetailsViewModel
            {
                Id = b.Id,
                Title = b.Title,
                AuthorName = b.Author.Name,
                AuthorId = b.Author.Id,
                Pages = b.Pages,
                CoverUlr = b.CoverUrl,
                Description = b.Description,
                YearPublished = b.YearPublished,
                Genres = string.Join(", ", b.BookGenres.Select(g => g.Genre.Name)),
                Reviews = b.Reviews
            })
                .FirstOrDefault();

            return View(bookDetails);
        }

        private IEnumerable<BookGenresViewModel> GetGenres()
        {
            return this.data
                    .Genres
                    .Select(g => new BookGenresViewModel  
                    { 
                        Id = g.Id, 
                        Name = g.Name
                    })
                    .ToList();
        }

        private IEnumerable<string> GetAuthors()
        {
            return this.data
                    .Authors
                    .Select(a => a.Name)
                    .ToList();
        }
    }
}
