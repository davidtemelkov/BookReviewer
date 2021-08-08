namespace BookReviewer.Services.Authors
{
    using BookReviewer.Models.Authors;
    using BookReviewer.Models.Users;
    using System.Collections.Generic;

    public interface IAuthorService
    {
        void AdminCreate(string name,
            string dateOfBirth,
            string details,
            string pictureUrl);

        void UserCreate(string name,
            string dateOfBirth,
            string details,
            string pictureUrl,
            string userId);

        void Edit(string id, AuthorFormModel editedAuthor);

        AuthorDetailsViewModel Details(string id);

        IEnumerable<string> GetAuthors();

        bool IsAuthorOfBook(string userId, string bookId);

        bool IsAuthor(string id);

        bool IsCurrentAuthor(string userId, string authorId);
    }
}
