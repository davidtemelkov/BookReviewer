namespace BookReviewer.Models.Books
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BookQueryViewModel
    {
        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }

        public IEnumerable<string> Genres { get; set; }

        public string Genre { get; set; }

        public IEnumerable<BookGridViewModel> Books { get; init; }
    }
}
