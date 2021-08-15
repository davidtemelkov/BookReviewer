namespace BookReviewer.Services.Lists
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Books;
    using BookReviewer.Models.Lists;
    using BookReviewer.Services.Books;
    using System.Collections.Generic;
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

        public IEnumerable<AllListsViewModel> GetUserLists(string id)
        {
            return this.data.Lists.Where(l => l.UserId == id).Select(l => new AllListsViewModel
            {
                Id = l.Id,
                Name = l.Name
            }).ToList();

            //return this.data.Users.Find(id).Lists.Select(l => new AllListsViewModel
            //{
            //    Id = l.Id,
            //    Name = l.Name
            //}).ToList();
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
            var list = this.data.Lists.Where(l => l.Id == int.Parse(id));

            var addedBooks2 = this.data.BookLists.Where(l => l.ListId == int.Parse(id)).Select(b => b.Book).Select(b => new BookGridViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author.Name,
                CoverUrl = b.CoverUrl
            })
                .ToList();

            //var addedBooks = list.FirstOrDefault().BookLists
            //    .Select(b => b.Book)
            //    .Select(b => new BookGridViewModel
            //    {
            //        Id = b.Id,
            //        Title = b.Title,
            //        Author = b.Author.Name,
            //        CoverUrl = b.CoverUrl
            //    })
            //    .ToList();

            var availableBooks = this.data.Books.Where(b => b.IsAccepted)
                .Select(b => new BookGridViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author.Name,
                    CoverUrl = b.CoverUrl
                });

          

            var bookDetails = list.Select(l => new ListDetailsViewModel
            {
                Id = l.Id,
                UserId = l.UserId,
                Name = l.Name,
                Description = l.Description,
                AddedBooks = addedBooks2,
                AvailableBooks = availableBooks.ToList()
            })
                .FirstOrDefault();

            return bookDetails;
        }

        public void AddBook(string bookId, string listId)
        {
            this.data.BookLists.Add(new BookList { BookId = int.Parse(bookId), ListId = int.Parse(listId) });
            this.data.SaveChanges();
        }
    }
}
