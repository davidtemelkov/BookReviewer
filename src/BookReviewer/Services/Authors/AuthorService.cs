namespace BookReviewer.Services.Authors
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Authors;
    using BookReviewer.Models.Books;
    using BookReviewer.Models.Users;
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

        public void AdminCreate(string name,
            string dateOfBirth,
            string details,
            string pictureUrl)
        {
            var authorData = new Author
            {
                Name = name,
                DateOfBirth = DateTime.ParseExact(dateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Details = details,
                PictureUrl = pictureUrl
            };

            this.data.Authors.Add(authorData);
            this.data.SaveChanges();
        }

        public void UserCreate(string name,
          string dateOfBirth,
          string details,
          string pictureUrl,
          string userId)
        {
            var authorData = new Author
            {
                Name = name,
                DateOfBirth = DateTime.ParseExact(dateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Details = details,
                PictureUrl = pictureUrl
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
            var author = this.data.Authors.Where(a => a.Id == int.Parse(id));

            var authorDetails = author.Select(a => new AuthorDetailsViewModel
            {
                Id = int.Parse(id),
                Name = a.Name,
                DateOfBirth = a.DateOfBirth.ToString("dd.MM.yyyy"),
                Details = a.Details,
                PictureUrl = a.PictureUrl,
                Books = this.data.Books.Where(b => b.AuthorId == int.Parse(id) && b.IsAccepted).Select(b => new BookGridViewModel {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author.Name,
                    CoverUrl = b.CoverUrl
                }).ToList()
            })
                .FirstOrDefault();

            return authorDetails;
        }

        public IEnumerable<string> GetAuthors()
        {
            return this.data
                    .Authors
                    .Select(a => a.Name)
                    .ToList();
        }
    }
}
