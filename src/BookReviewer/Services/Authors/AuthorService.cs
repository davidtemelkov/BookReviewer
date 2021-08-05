namespace BookReviewer.Services.Authors
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Authors;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext data;

        public AuthorService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void Create(string name,
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

        public AuthorDetailsViewModel Details(string id)
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

            return authorDetails;
        }
    }
}
