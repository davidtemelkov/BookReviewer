namespace BookReviewer.Services.Authors
{
    using BookReviewer.Models.Authors;
    using System.Collections.Generic;

    public interface IAuthorService
    {
        void AdminCreate(AuthorFormModel author);

        void UserCreate(AuthorFormModel author, string userId);

        void Edit(string id, AuthorFormModel editedAuthor);

        AuthorDetailsViewModel Details(string id);

        IEnumerable<string> GetAuthors();

        bool IsAuthorOfBook(string userId, string bookId);

        bool IsAuthor(string id);

        bool IsCurrentAuthor(string userId, string authorId);
    }
}
