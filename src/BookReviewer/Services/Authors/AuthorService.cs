namespace BookReviewer.Services.Authors
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Authors;
    using BookReviewer.Models.Books;
    using System;
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
                Id = int.Parse(id),
                Name = a.Name,
                DateOfBirth = a.DateOfBirth.ToString("dd.MM.yyyy"),
                Details = a.Details,
                PictureUrl = a.PictureUrl,
                Books = this.data.Books.Where(b => b.AuthorId == int.Parse(id)).Select(b => new BookGridViewModel {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author.Name,
                    CoverUrl = b.CoverUrl
                }).ToList()
                
            })
                .FirstOrDefault();

            return authorDetails;
        }
    }
}
