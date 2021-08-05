namespace BookReviewer.Services.Users
{
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Users;
    using System.Collections.Generic;

    public interface IUserService
    {
        UserProfileViewModel Profile(string id);

        void CreateAuthor(string name,
            string dateOfBirth,
            string details,
            string pictureUrl,
            string userId);

        void AddBook(string title,
            User currentUser,
            string coverUrl,
            string description, 
            int pages,
            string yearPublished,
            ICollection<string> bookGenres);

        AllReviewsViewModel AllUserReviews(string id);
    }
}
