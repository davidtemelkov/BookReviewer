namespace BookReviewer.Services.Users
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Users;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public UserProfileViewModel Profile(string id)
        {
            var profile = new UserProfileViewModel
            {
                Id = id,
                Username = this.data.Users.FirstOrDefault(u => u.Id == id).UserName,
                ProfilePictureUrl = this.data.Users.FirstOrDefault(u => u.Id == id).ProfilePicture,
                AuthorId = this.data.Users.FirstOrDefault(u => u.Id == id).AuthorId
            };

            return profile;
        }

        public void CreateAuthor(string name,
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

        public void AddBook(string title,
            User currentUser,
            string coverUrl,
            string description,
            int pages,
            string yearPublished,
            ICollection<string> bookGenres)
        {
            var bookData = new Book
            {
                Title = title,
                Author = data.Authors.FirstOrDefault(a => a.Id == currentUser.AuthorId),
                CoverUrl = coverUrl,
                Description = description,
                Pages = pages,
                YearPublished = yearPublished
            };

            foreach (var genre in bookGenres)
            {
                bookData.BookGenres.Add(new BookGenre { Book = bookData, Genre = data.Genres.FirstOrDefault(g => g.Name == genre) });
            }

            data.Books.Add(bookData);
            data.SaveChanges();
        }

        public void EditBook(string id, UserBookFormModel editedBook)
        {
            var bookData = this.data.Books.Find(int.Parse(id));

            bookData.Title = editedBook.Title;
            bookData.CoverUrl = editedBook.CoverUrl;
            bookData.Description = editedBook.Description;
            bookData.Pages = editedBook.Pages;
            bookData.YearPublished = editedBook.YearPublished;

            this.data.SaveChanges();
        }

        public void EditAuthor(string id, AuthorFormModel editedAuthor)
        {
            var authorData = this.data.Authors.Find(int.Parse(id));

            authorData.Name = editedAuthor.Name;
            authorData.DateOfBirth = DateTime.ParseExact(editedAuthor.DateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            authorData.Details = editedAuthor.Details;
            authorData.PictureUrl = editedAuthor.PictureUrl;

            this.data.SaveChanges();
        }

        public AllReviewsViewModel AllUserReviews(string id)
        {
            var reviews = new AllReviewsViewModel
            {
                Reviews = this.data.Reviews
               .Where(r => r.UserId == id)
               .Include(r => r.Book)
               .ThenInclude(b => b.Author)
               .Include(r => r.User)
               .ToList()
            };

            return reviews;
        }
    }
}
