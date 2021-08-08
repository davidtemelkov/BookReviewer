namespace BookReviewer.Controllers
{
    using BookReviewer.Infrastructure;
    using BookReviewer.Models.Lists;
    using BookReviewer.Services.Lists;
    using Microsoft.AspNetCore.Mvc;

    public class ListsController : Controller
    {
        private readonly IListService lists;

        public ListsController(IListService lists)
        {
            this.lists = lists;
        }

        public IActionResult UserLists(string id)
        {
            return View();
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(ListFormModel form)
        {
            var currenUser = this.User.Id();

            this.lists.Create(currenUser, form);

            return View();
        }

        public IActionResult AddBookToList()
        {
            return View();
        }

        public IActionResult RemoveBookFromList()
        {
            return View();
        }
    }
}
