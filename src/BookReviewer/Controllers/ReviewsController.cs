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
        public IActionResult Add(string id, AddReviewFormModel review)
        {
            if (!ModelState.IsValid)
            {
                return View(review);
            }

            reviews.Create(review.Stars,
                review.Text,
                id,
                User.Id());

            return Redirect($"/Books/Details/{id}");
        }
    }
}
