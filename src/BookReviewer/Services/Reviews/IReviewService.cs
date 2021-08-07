namespace BookReviewer.Services.Reviews
{
    using BookReviewer.Models.Reviews;
    using BookReviewer.Models.Users;

    public interface IReviewService
    {
        void Create(int stars,
            string text,
            string id,
            string userId);

        void Edit(string id, ReviewFormModel editedReview);

        void Delete(string id);

        ReviewFormModel Details(string id);

        AllReviewsViewModel GetUserReviews(string id);
    }
}
