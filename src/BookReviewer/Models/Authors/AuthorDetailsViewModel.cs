namespace BookReviewer.Models.Authors
{
    using BookReviewer.Data.Models;
    using System.Collections.Generic;

    public class AuthorDetailsViewModel
    {
        public string Name { get; init; }

        public string DateOfBirth { get; init; }

        public string Details { get; init; }

        public string PictureUrl { get; init; }

        public ICollection<Book> Books { get; init; }
    }
}
