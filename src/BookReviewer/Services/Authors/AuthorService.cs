﻿namespace BookReviewer.Services.Authors
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Authors;
    using BookReviewer.Services.Books;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class AuthorService : IAuthorService
    {
        private readonly BookReviewerDbContext data;
        private readonly IBookService books;

        public AuthorService(BookReviewerDbContext data,
            IBookService books)
        {
            this.data = data;
            this.books = books;
        }

        public void AdminCreate(AuthorFormModel author)
        {
            var authorData = new Author
            {
                Name = author.Name,
                DateOfBirth = DateTime.ParseExact(author.DateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Details = author.Details,
                PictureUrl = author.PictureUrl
            };

            this.data.Authors.Add(authorData);
            this.data.SaveChanges();
        }

        public void UserCreate(AuthorFormModel author, string userId)
        {
            var authorData = new Author
            {
                Name = author.Name,
                DateOfBirth = DateTime.ParseExact(author.DateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Details = author.Details,
                PictureUrl = author.PictureUrl
            };

            this.data.Authors.Add(authorData);
            this.data.SaveChanges();

            this.data.Users
                .FirstOrDefault(u => u.Id == userId)
                .AuthorId = authorData.Id;
            this.data.SaveChanges();
        }

        public void Edit(string id, AuthorFormModel editedAuthor)
        {
            var authorData = this.data.Authors.Find(int.Parse(id));

            authorData.Name = editedAuthor.Name;
            authorData.DateOfBirth = DateTime.ParseExact(editedAuthor.DateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            authorData.Details = editedAuthor.Details;
            authorData.PictureUrl = editedAuthor.PictureUrl;

            this.data.SaveChanges();
        }

        public AuthorDetailsViewModel Details(string id)
        {
            var author = this.data.Authors.Where(a => a.Id == int.Parse(id))
                .ToList();

            var authorDetails = author.Select(a => new AuthorDetailsViewModel
            {
                Id = int.Parse(id),
                Name = a.Name,
                DateOfBirth = a.DateOfBirth.ToString("dd.MM.yyyy"),
                Details = a.Details,
                PictureUrl = a.PictureUrl,
                Books = this.books.GetAcceptedBooks().Where(b => b.AuthorId == int.Parse(id))
            })
                .FirstOrDefault();

            return authorDetails;
        }

        public bool IsAuthor(string id) 
            => this.data.Users.Find(id).AuthorId.HasValue;


        public bool IsAuthorOfBook(string userId, string bookId)
            => this.data.Users.Find(userId).AuthorId == this.data.Books.Find(int.Parse(bookId)).AuthorId;

        public bool IsCurrentAuthor(string userId, string authorId) 
            => this.data.Users.Find(userId).AuthorId == int.Parse(authorId);

        public IEnumerable<string> GetAuthors()
        {
            return this.data
                    .Authors
                    .Select(a => a.Name)
                    .ToList();
        }
    }
}
