namespace BookReviewer.Controllers
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Reviews;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Security.Claims;

    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext data;

        public ReviewsController(ApplicationDbContext data)
        {
            this.data = data;
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

            var reviewData = new Review 
            {
                Stars = review.Stars,
                Text = review.Text,
                BookId = int.Parse(id),
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            };

            this.data.Reviews.Add(reviewData);
            this.data.SaveChanges();

            return Redirect($"/Books/Details/{id}");
        }
    }
}
