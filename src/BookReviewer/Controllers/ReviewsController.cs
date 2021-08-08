namespace BookReviewer.Controllers
{
    using BookReviewer.Models.Reviews;
    using BookReviewer.Services.Reviews;
    using Microsoft.AspNetCore.Mvc;
    using BookReviewer.Infrastructure;
    using Microsoft.AspNetCore.Authorization;

    public class ReviewsController : Controller
    {
        private readonly IReviewService reviews;

        public ReviewsController(IReviewService reviews)
        {
            this.reviews = reviews;
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(string id, ReviewFormModel review)
        {
            if (!ModelState.IsValid)
            {
                return View(review);
            }

            this.reviews.Create(review.Stars,
                review.Text,
                id,
                User.Id());

            return Redirect($"/Books/Details/{id}");
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            if (!this.reviews.OwnsReview(User.Id(), id) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(this.reviews.Details(id));
        }


        [Authorize]
        [HttpPost]
        public IActionResult Edit(string id, ReviewFormModel editedReview)
        {
            var currentUserId = User.Id();
            this.reviews.Edit(id, editedReview);

            return Redirect($"/Reviews/UserReviews/{currentUserId}");
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            if (!this.reviews.OwnsReview(User.Id(), id) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var userId = User.Id();

            this.reviews.Delete(id);

            return Redirect($"/Reviews/UserReviews/{userId}");
        }

        public IActionResult UserReviews(string id) => View(this.reviews.GetUserReviews(id));
    }
}
