namespace BookReviewer.Controllers
{
    using BookReviewer.Data;
    using Microsoft.AspNetCore.Mvc;

    public class ListsController : Controller
    {
        private readonly BookReviewerDbContext data;

        public ListsController(BookReviewerDbContext data)
        {
            this.data = data;
        }
       
    }
}
