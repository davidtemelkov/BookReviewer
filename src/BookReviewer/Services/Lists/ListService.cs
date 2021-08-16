namespace BookReviewer.Services.Lists
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
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
        private readonly IMapper mapper;

        public ListService(BookReviewerDbContext data,
            IBookService books,
            IMapper mapper)
        {
            this.data = data;
            this.books = books;
            this.mapper = mapper;
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
            var listData = this.mapper.Map<List>(list);
            listData.UserId = userId;

            this.data.Lists.Add(listData);
            this.data.SaveChanges();
            this.data.Entry(listData).GetDatabaseValues();

            return listData.Id;
        }

        public ListDetailsViewModel GetListDetails(string id)
        {
            var list = this.data.Lists.Where(l => l.Id == int.Parse(id));

            var bookDetails = list.ProjectTo<ListDetailsViewModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

            var addedBooks = this.data.BookLists.Where(l => l.ListId == int.Parse(id))
                .Select(b => b.Book).ProjectTo<BookGridViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

            var availableBooks = this.books.GetAcceptedBooks()
               .Where(b => addedBooks.FirstOrDefault(x => x.Id == b.Id) == null)
               .ToList();

            bookDetails.AddedBooks = addedBooks;
            bookDetails.AvailableBooks = availableBooks;

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
