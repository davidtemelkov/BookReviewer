namespace BookReviewer.Services.Books
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Books;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class BookService : IBookService
    {
        private readonly BookReviewerDbContext data;

        public BookService(BookReviewerDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<BookGridViewModel> GetAcceptedBooks()
        {
            var books = this.data
               .Books
               .OrderByDescending(b => b.Reviews.Select(r => r.Stars).Average())
               .Where(b => b.IsAccepted)
               .Select(b => new BookGridViewModel
               {
                   Id = b.Id,
                   Title = b.Title,
                   Author = b.Author.Name,
                   AuthorId = b.AuthorId,
                   CoverUrl = b.CoverUrl,
                   IsAccepted = b.IsAccepted,
                   Genres = string.Join("", b.BookGenres.Select(g => g.Genre.Name))
               })
               .ToList();

            return books;
        }

        public IEnumerable<BookGridViewModel> GetNonAcceptedBooks()
        {
            var books = this.data
               .Books
               .Where(b => !b.IsAccepted)
               .OrderByDescending(b => b.DateAdded)
               .Select(b => new BookGridViewModel
               {
                   Id = b.Id,
                   Title = b.Title,
                   Author = b.Author.Name,
                   AuthorId = b.AuthorId,
                   CoverUrl = b.CoverUrl,
                   IsAccepted = b.IsAccepted
               })
               .ToList();

            return books;
        }

        public void AdminCreate(string title,
            string author,
            string coverUrl,
            string description,
            int pages,
            string yearPublished,
            ICollection<string> bookGenres)
        {
            var bookData = new Book
            {
                Title = title,
                Author = data.Authors.FirstOrDefault(a => a.Name == author),
                CoverUrl = coverUrl,
                Description = description,
                Pages = pages,
                YearPublished = yearPublished,
                IsAccepted = true
            };

            foreach (var genre in bookGenres)
            {
                bookData.BookGenres.Add(new BookGenre { Book = bookData, Genre = this.data.Genres.FirstOrDefault(g => g.Name == genre) });
            }

            this.data.Books.Add(bookData);
            this.data.SaveChanges();
        }

        public void UserCreate(string title,
            User currentUser,
            string coverUrl,
            string description,
            int pages,
            string yearPublished,
            ICollection<string> bookGenres)
        {
            var bookData = new Book
            {
                Title = title,
                Author = data.Authors.FirstOrDefault(a => a.Id == currentUser.AuthorId),
                CoverUrl = coverUrl,
                Description = description,
                Pages = pages,
                YearPublished = yearPublished
            };

            foreach (var genre in bookGenres)
            {
                bookData.BookGenres.Add(new BookGenre { Book = bookData, Genre = data.Genres.FirstOrDefault(g => g.Name == genre) });
            }

            this.data.Books.Add(bookData);
            this.data.SaveChanges();
        }

        public void Edit(string id, BookFormModel editedBook)
        {
            var bookData = this.data.Books.Find(int.Parse(id));

            bookData.Title = editedBook.Title;
            bookData.CoverUrl = editedBook.CoverUrl;
            bookData.Description = editedBook.Description;
            bookData.Pages = editedBook.Pages;
            bookData.YearPublished = editedBook.YearPublished;

            this.data.SaveChanges();
        }

        public BookDetailsViewModel BookDetails(string id)
        {
            var book = this.data.Books
                .Where(b => b.Id == int.Parse(id))
                .Include(b => b.Reviews)
                .ThenInclude(r => r.User);

            var bookDetails = book.Select(b => new BookDetailsViewModel
            {
                Id = b.Id,
                Title = b.Title,
                AuthorName = b.Author.Name,
                AuthorId = b.Author.Id,
                Pages = b.Pages,
                CoverUlr = b.CoverUrl,
                Description = b.Description,
                YearPublished = b.YearPublished,
                Genres = string.Join(", ", b.BookGenres.Select(g => g.Genre.Name)),
                Reviews = b.Reviews
            })
                .FirstOrDefault();

            return bookDetails;
        }
    }
}
