namespace BookReviewer.Controllers
{
    using BookReviewer.Models.Reviews;
    using BookReviewer.Services.Reviews;
    using Microsoft.AspNetCore.Mvc;
    using BookReviewer.Infrastructure;

    public class ReviewsController : Controller
    {
        private readonly IReviewService reviews;

        public ReviewsController(IReviewService reviews)
        {
            this.reviews = reviews;
        }

        public IActionResult Add()
        {
            return View();
        }

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

        public IActionResult Edit(string id) => View(this.reviews.Details(id));

        [HttpPost]
        public IActionResult Edit(string id, ReviewFormModel editedReview)
        {
            var currentUserId = User.Id();
            this.reviews.Edit(id, editedReview);

            return Redirect($"/Users/Reviews/{currentUserId}");
        }

        public IActionResult Delete(string id)
        {
            var userId = User.Id();

            this.reviews.Delete(id);

            return Redirect($"/Users/Reviews/{userId}");
        }
    }
}
