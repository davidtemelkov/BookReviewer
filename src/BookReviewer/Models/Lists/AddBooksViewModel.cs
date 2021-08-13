namespace BookReviewer.Models.Lists
{
    using BookReviewer.Models.Books;
    using System.Collections.Generic;


    public class AddBooksViewModel
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public IEnumerable<BookGridViewModel> AddedBooks { get; init; } = new List<BookGridViewModel>();

        public IEnumerable<BookGridViewModel> AvailableBooks { get; init; }
    }
}
