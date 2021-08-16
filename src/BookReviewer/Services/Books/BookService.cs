namespace BookReviewer.Services.Books
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Books;
    using BookReviewer.Services.Genres;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class BookService : IBookService
    {
        private readonly BookReviewerDbContext data;
        private readonly IGenreService genres;
        private readonly IMapper mapper;

        public BookService(BookReviewerDbContext data,
            IGenreService genres,
            IMapper mapper)
        {
            this.data = data;
            this.genres = genres;
            this.mapper = mapper;
        }

        public IEnumerable<BookGridViewModel> GetAcceptedBooks()
        {
            var books = this.data.Books
               .OrderByDescending(b => b.Reviews.Select(r => r.Stars).Average())
               .Where(b => b.IsAccepted)
               .ProjectTo<BookGridViewModel>(this.mapper.ConfigurationProvider)
               .ToList();

            return books;
        }

        public IEnumerable<BookGridViewModel> GetNonAcceptedBooks()
        {
            var books = this.data
               .Books
               .Where(b => !b.IsAccepted)
               .OrderByDescending(b => b.DateAdded)
               .ProjectTo<BookGridViewModel>(this.mapper.ConfigurationProvider)
               .ToList();

            return books;
        }

        public void AdminCreate(BookFormModel book)
        {
            var bookData = this.mapper.Map<Book>(book);
            bookData.Author = this.data.Authors.FirstOrDefault(a => a.Name == book.Author);

            foreach (var genre in book.BookGenres)
            {
                bookData.BookGenres.Add(new BookGenre { Book = bookData, Genre = this.data.Genres.FirstOrDefault(g => g.Name == genre) });
            }

            this.data.Books.Add(bookData);
            this.data.SaveChanges();
        }

        public void UserCreate(User currentUser, BookFormModel book)
        {
            var bookData = this.mapper.Map<Book>(book);
            bookData.Author = this.data.Authors.FirstOrDefault(a => a.Id == currentUser.AuthorId);

            foreach (var genre in book.BookGenres)
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

        public BookQueryViewModel SearchBooks(string searchTerm, string genre)
        {
            var books = this.GetAcceptedBooks();

            if (searchTerm == null && genre == "All")
            {
                return null;
            }
            else if (searchTerm == null && genre != "All")
            {
                return new BookQueryViewModel
                {
                    Genres = this.genres.GetGenres(),
                    Books = books.Where(b => b.Genres.ToLower().Split(",").Any(g => g == genre.ToLower()))
                };
            }
            else if (searchTerm != null && genre == "All")
            {
                return new BookQueryViewModel
                {
                    Genres = this.genres.GetGenres(),
                    Books = books.Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()))
                };
            }
            else
            {
                return new BookQueryViewModel
                {
                    Genres = this.genres.GetGenres(),
                    Books = books.Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()) && b.Genres.ToLower().Split(",").Any(g => g == genre.ToLower()))
                };
            }
        }
    }
}
