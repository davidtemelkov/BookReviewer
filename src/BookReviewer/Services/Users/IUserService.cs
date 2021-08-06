namespace BookReviewer.Services.Users
{
    using BookReviewer.Models.Reviews;
    using BookReviewer.Models.Users;

    public interface IUserService
    {
        UserProfileViewModel Profile(string id);

        AllReviewsViewModel AllUserReviews(string id);

        void EditReview(string id, ReviewFormModel editedReview);
    }
}
