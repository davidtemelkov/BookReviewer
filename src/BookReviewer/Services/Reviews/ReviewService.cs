namespace BookReviewer.Services.Reviews
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Reviews;
    using BookReviewer.Models.Users;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class ReviewService : IReviewService
    {
        private readonly BookReviewerDbContext data;

        public ReviewService(BookReviewerDbContext data)
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

        public void Edit(string id, ReviewFormModel editedReview)
        {
            var reviewData = this.data.Reviews.Find(int.Parse(id));

            reviewData.Stars = editedReview.Stars;
            reviewData.Text = editedReview.Text;

            this.data.SaveChanges();
        }

        public ReviewFormModel Details(string id)
        {
            var review = this.data.Reviews.Find(int.Parse(id));

            var editReviewForm = new ReviewFormModel
            {
                Stars = review.Stars,
                Text = review.Text
            };

            return editReviewForm;
        }

        public void Delete(string id)
        {
            var review = this.data.Reviews.Find(int.Parse(id));
            this.data.Reviews.Remove(review);
            this.data.SaveChanges();
        }

        public AllReviewsViewModel GetUserReviews(string id)
        {
            var reviews = new AllReviewsViewModel
            {
                Reviews = this.data.Reviews
               .Where(r => r.UserId == id)
               .Include(r => r.Book)
               .ThenInclude(b => b.Author)
               .Include(r => r.User)
               .ToList()
            };

            return reviews;
        }
    }
}
