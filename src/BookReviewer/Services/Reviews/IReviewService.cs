namespace BookReviewer.Services.Reviews
{
    public interface IReviewService
    {
        void Create(int stars,
            string text,
            string id,
            string userId);
    }
}
