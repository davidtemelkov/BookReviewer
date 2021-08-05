namespace BookReviewer.Services.Reviews
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;

    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext data;

        public ReviewService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void Create(int stars,
            string text,
            string id,
            string userId)
        {
            var reviewData = new Review
            {
                Stars = stars,
                Text = text,
                BookId = int.Parse(id),
                UserId = userId
            };

            this.data.Reviews.Add(reviewData);
            this.data.SaveChanges();
        }
    }
}
