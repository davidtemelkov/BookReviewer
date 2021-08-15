namespace BookReviewer.Services.Lists
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Books;
    using BookReviewer.Models.Lists;
    using BookReviewer.Services.Books;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class ListService : IListService
    {
        private readonly BookReviewerDbContext data;
        private readonly IBookService books;

        public ListService(BookReviewerDbContext data,
            IBookService books)
        {
            this.data = data;
            this.books = books;
        }

        public AllListsViewModel GetUserLists(string id)
        {
            var listData = new AllListsViewModel
            {
                UserId = id,
                Lists = this.data.Users.Include(x => x.Lists).FirstOrDefault(u => u.Id == id).Lists.ToList()
            };

            return listData;
        }

        public int Create(string userId, ListFormModel list)
        {
            var listData = new List
            {
                Name = list.Name,
                Description = list.Description,
                UserId = userId
            };

            this.data.Lists.Add(listData);
            this.data.SaveChanges();
            this.data.Entry(listData).GetDatabaseValues();

            return listData.Id;
        }

        public ListDetailsViewModel GetListDetails(string id)
        {
            var list = this.data.Lists.Where(l => l.Id == int.Parse(id))
                .ToList();

            var addedBooks = this.data.BookLists.Where(l => l.ListId == int.Parse(id))
                .Select(b => b.Book).Select(b => new BookGridViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author.Name,
                    CoverUrl = b.CoverUrl
                })
                .ToList();

            var availableBooks = this.books.GetAcceptedBooks()
                .Where(b => addedBooks.FirstOrDefault(x => x.Id == b.Id) == null)
                .ToList();

            var bookDetails = list.Select(l => new ListDetailsViewModel
            {
                Id = l.Id,
                UserId = l.UserId,
                Name = l.Name,
                Description = l.Description,
                AddedBooks = addedBooks,
                AvailableBooks = availableBooks
            })
                .FirstOrDefault();

            return bookDetails;
        }

        public void AddBook(string bookId, string listId)
        {
            this.data.BookLists.Add(new BookList { BookId = int.Parse(bookId), ListId = int.Parse(listId) });
            this.data.SaveChanges();
        }

        public void RemoveBook(string bookId, string listId)
        {
            var book = this.data.BookLists.FirstOrDefault(b => b.BookId == int.Parse(bookId) && b.ListId == int.Parse(listId));
            this.data.BookLists.Remove(book);
            this.data.SaveChanges();
        }
    }
}
