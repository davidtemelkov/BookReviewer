namespace BookReviewer.Services.Users
{
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Books;
    using BookReviewer.Models.Reviews;
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

        AllReviewsViewModel AllUserReviews(string id);

        void EditAuthor(string id, AuthorFormModel editedAuthor);

        void EditReview(string id, ReviewFormModel editedReview);
    }
}
