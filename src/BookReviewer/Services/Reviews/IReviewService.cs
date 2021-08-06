namespace BookReviewer.Services.Reviews
{
    using BookReviewer.Models.Reviews;

    public interface IReviewService
    {
        void Create(int stars,
            string text,
            string id,
            string userId);

        void Edit(string id, ReviewFormModel editedReview);

        void Delete(string id);

        ReviewFormModel Details(string id);
    }
}
