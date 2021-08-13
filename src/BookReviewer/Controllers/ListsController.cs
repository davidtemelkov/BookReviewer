namespace BookReviewer.Controllers
{
    using BookReviewer.Infrastructure;
    using BookReviewer.Models.Books;
    using BookReviewer.Models.Lists;
    using BookReviewer.Services.Books;
    using BookReviewer.Services.Lists;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class ListsController : Controller
    {
        private readonly IListService lists;
        private readonly IBookService books;

        public ListsController(IListService lists,
            IBookService books)
        {
            this.lists = lists;
            this.books = books;
        }

        public IActionResult UserLists(string id)
        {
            return View();
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(ListFormModel form)
        {

            return RedirectToAction("AddBooks", "Lists", form);
        }

        public IActionResult AddBooks(ListFormModel form)
        {
            return View(new AddBooksViewModel
            {
                Name = form.Name,
                Description = form.Description,
                AvailableBooks = this.books.GetAcceptedBooks()
            });
        }

        public IActionResult AddToList(AddBooksViewModel list)
        {
            return RedirectToAction("AddBooks", "Lists", list);
        }

        public IActionResult RemoveFromList(AddBooksViewModel list)
        {
            return RedirectToAction("AddBooks", "Lists", list);
        }
    }
}
