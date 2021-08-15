namespace BookReviewer.Models.Lists
{
    using BookReviewer.Models.Books;
    using System.Collections.Generic;


    public class ListDetailsViewModel
    {
        public int Id { get; init; }

        public string UserId { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public IEnumerable<BookGridViewModel> AddedBooks { get; init; } 

        public IEnumerable<BookGridViewModel> AvailableBooks { get; set; } 
    }
}
