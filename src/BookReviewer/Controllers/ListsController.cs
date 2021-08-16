namespace BookReviewer.Controllers
{
    using BookReviewer.Infrastructure;
    using BookReviewer.Models.Lists;
    using BookReviewer.Services.Books;
    using BookReviewer.Services.Lists;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ListsController : Controller
    {
        private readonly IListService lists;

        public ListsController(IListService lists)
        {
            this.lists = lists;
        }

        public IActionResult UserLists(string id) => View(this.lists.GetUserLists(id));

        [Authorize]
        public IActionResult Create() => View();

        [Authorize]
        [HttpPost]
        public IActionResult Create(ListFormModel form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var createdListId = this.lists.Create(User.Id(), form);

            return Redirect($"/Lists/Edit/{createdListId}");
        }

        public IActionResult Details(string id)
        {
            var details = this.lists.GetListDetails(id);

            return View(details);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var details = this.lists.GetListDetails(id);

            return View(details);
        }

        [Authorize]
        public IActionResult AddToList(string id)
        {
            var ids = id.Split("%2F");
            var bookId = ids[0];
            var listId = ids[1];

            this.lists.AddBook(bookId, listId);

            return Redirect($"/Lists/Edit/{listId}");
        }

        [Authorize]
        public IActionResult RemoveFromList(string id)
        {
            var ids = id.Split("%2F");
            var bookId = ids[0];
            var listId = ids[1];

            this.lists.RemoveBook(bookId, listId);

            return Redirect($"/Lists/Edit/{listId}");
        }
    }
}
