namespace BookReviewer.Controllers
{
    using BookReviewer.Data;
    using Microsoft.AspNetCore.Mvc;

    public class ListsController : Controller
    {
        private readonly ApplicationDbContext data;

        public ListsController(ApplicationDbContext data)
        {
            this.data = data;
        }
       
    }
}
