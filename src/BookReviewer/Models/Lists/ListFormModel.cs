namespace BookReviewer.Models.Lists
{
    using BookReviewer.Data.Models;
    using System.Collections.Generic;

    public class ListFormModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Book> Books { get; init; }
    }
}
