namespace BookReviewer.Services.Authors
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Authors;
    using BookReviewer.Services.Books;
    using BookReviewer.Services.Users;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class AuthorService : IAuthorService
    {
        private readonly BookReviewerDbContext data;
        private readonly IBookService books;
        private readonly IUserService users;
        private readonly IMapper mapper;

        public AuthorService(BookReviewerDbContext data,
            IBookService books,
            IUserService users,
            IMapper mapper)
        {
            this.data = data;
            this.books = books;
            this.users = users;
            this.mapper = mapper;
        }

        public void AdminCreate(AuthorFormModel author)
        {
            var authorData = this.mapper.Map<Author>(author);

            this.data.Authors.Add(authorData);
            this.data.SaveChanges();
        }

        public void UserCreate(AuthorFormModel author, string userId)
        {
            var authorData = this.mapper.Map<Author>(author);

            this.data.Authors.Add(authorData);
            this.data.SaveChanges();

            this.users.GetUserById(userId)
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
            var authorData = this.data.Authors.Where(a => a.Id == int.Parse(id))
                .ProjectTo<AuthorDetailsViewModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

            authorData.Books = this.books.GetAcceptedBooks().Where(b => b.AuthorId == int.Parse(id));

            return authorData;
        }

        public bool IsAuthor(string id) 
            => this.users.GetUserById(id).AuthorId.HasValue;


        public bool IsAuthorOfBook(string userId, string bookId)
            => this.users.GetUserById(userId).AuthorId == this.data.Books.Find(int.Parse(bookId)).AuthorId;

        public bool IsCurrentAuthor(string userId, string authorId) 
            => this.users.GetUserById(userId).AuthorId == int.Parse(authorId);

        public IEnumerable<string> GetAuthors()
        {
            return this.data
                    .Authors
                    .Select(a => a.Name)
                    .ToList();
        }
    }
}
