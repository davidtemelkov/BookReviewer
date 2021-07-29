namespace BookReviewer.Models.Books
{
    using BookReviewer.Data.Models;
    using System.Collections.Generic;

    public class BookDetailsViewModel
    {
        public int Id { get; init; }
        public string Title { get; init; }

        public string CoverUlr { get; init; }

        public string YearPublished { get; init; }

        public string AuthorName { get; init; }

        public int AuthorId { get; init; }

        public string Description { get; init; }

        public int Pages { get; init; }

        public ICollection<BookGenre> BookGenres { get; init; }
    }
}
