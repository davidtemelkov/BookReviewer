namespace BookReviewer.Services.Books
{
    using BookReviewer.Models.Books;
    using System.Collections.Generic;

    public interface IBookService
    {
        IEnumerable<BookGridViewModel> GetBooks();

        IEnumerable<string> GetGenres();

        IEnumerable<string> GetAuthors();

        void Create(string title,
            string author,
            string coverUrl,
            string description,
            int pages,
            string yearPublished,
            ICollection<string> bookGenres);

        BookDetailsViewModel BookDetails(string id);
    }
}
