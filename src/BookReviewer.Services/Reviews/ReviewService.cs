namespace BookReviewer.Services.Reviews
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Reviews;
    using BookReviewer.Models.Users;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class ReviewService : IReviewService
    {
        private readonly BookReviewerDbContext data;
        private readonly IMapper mapper;

        public ReviewService(BookReviewerDbContext data,
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Create(string bookId, string userId, ReviewFormModel review)
        {
            var reviewData = this.mapper.Map<Review>(review);
            reviewData.BookId = int.Parse(bookId);
            reviewData.UserId = userId;

            this.data.Reviews.Add(reviewData);
            this.data.SaveChanges();
        }

        public void Edit(string id, ReviewFormModel editedReview)
        {
            var reviewData = this.data.Reviews.Find(int.Parse(id));

            reviewData.Stars = int.Parse(editedReview.Stars);
            reviewData.Text = editedReview.Text;

            this.data.SaveChanges();
        }

        public ReviewFormModel Details(string id)
        {
            var review = this.data.Reviews.Find(int.Parse(id));

            var editReviewForm = this.mapper.Map<ReviewFormModel>(review);

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

        public bool UserOwnsReview(string userId, string reviewId)
            => userId == this.data.Reviews.Find(int.Parse(reviewId)).UserId;
    }
}
