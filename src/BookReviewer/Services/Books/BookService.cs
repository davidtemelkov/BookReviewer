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
        private readonly ApplicationDbContext data;

        public BookService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<BookGridViewModel> GetBooks() 
        {
            var books = this.data
               .Books
               .OrderByDescending(b => b.DateAdded)
               .Select(b => new BookGridViewModel
               {
                   Id = b.Id,
                   Title = b.Title,
                   Author = b.Author.Name,
                   CoverUrl = b.CoverUrl
               })
               .ToList();

            return books;
        }

        public void Create(string title,
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
                YearPublished = yearPublished
            };

            foreach (var genre in bookGenres)
            {
                bookData.BookGenres.Add(new BookGenre { Book = bookData, Genre = data.Genres.FirstOrDefault(g => g.Name == genre) });
            }

            data.Books.Add(bookData);
            data.SaveChanges();
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

        public IEnumerable<string> GetGenres()
        {
            return this.data
                    .Genres
                    .Select(g => g.Name)
                    .ToList();
        }

        public IEnumerable<string> GetAuthors()
        {
            return this.data
                    .Authors
                    .Select(a => a.Name)
                    .ToList();
        }

    }
}
