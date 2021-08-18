namespace BookReviewer.Services.Reviews
{
    using BookReviewer.Models.Reviews;
    using BookReviewer.Models.Users;

    public interface IReviewService
    {
        void Create(string bookId, string userId, ReviewFormModel review);

        void Edit(string id, ReviewFormModel editedReview);

        void Delete(string id);

        ReviewFormModel Details(string id);

        AllReviewsViewModel GetUserReviews(string id);

        bool UserOwnsReview(string userId, string reviewId);
    }
}
