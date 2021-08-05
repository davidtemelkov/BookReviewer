namespace BookReviewer.Models.Users
{
    using BookReviewer.Models.Books;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserAddBookFormModel
    {
        public string Title { get; init; }

        public string Author { get; init; }

        public string CoverUrl { get; init; }

        public string YearPublished { get; init; }

        public int Pages { get; init; }

        public string Description { get; init; }

        [Display(Name = "Genres")]
        public ICollection<string> BookGenres { get; init; }
        public IEnumerable<string> Genres { get; set; }
    }
}
