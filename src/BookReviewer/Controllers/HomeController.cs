using BookReviewer.Data;
using BookReviewer.Models;
using BookReviewer.Models.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace BookReviewer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext data;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext data)
        {
            _logger = logger;
            this.data = data;
        }

        public IActionResult Index()
        {
            var books = this.data
                .Books
                .OrderByDescending(b => b.DateAdded)
                .Select(b => new BookGridViewModel 
            { 
                Id = b.Id,
                Title = b.Title,
                Author = b.Author.Name,
                CoverUrl = b.CoverUrl
            })
                .ToList();

            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
