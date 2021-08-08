namespace BookReviewer.Services.Books
{
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Books;
    using System.Collections.Generic;

    public interface IBookService
    {
        IEnumerable<BookGridViewModel> GetAcceptedBooks();

        IEnumerable<BookGridViewModel> GetNonAcceptedBooks();

        void AdminCreate(string title,
            string author,
            string coverUrl,
            string description,
            int pages,
            string yearPublished,
            ICollection<string> bookGenres);

        void UserCreate(string title,
            User currentUser,
            string coverUrl,
            string description,
            int pages,
            string yearPublished,
            ICollection<string> bookGenres);

        void Edit(string id, BookFormModel editedBook);

        BookDetailsViewModel BookDetails(string id);
    }
}
