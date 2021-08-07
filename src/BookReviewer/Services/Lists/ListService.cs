namespace BookReviewer.Services.Lists
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Lists;

    public class ListService : IListService
    {
        private readonly BookReviewerDbContext data;

        public ListService(BookReviewerDbContext data)
        {
            this.data = data;
        }

        public void Create(string userId, ListFormModel list)
        {
            var listData = new List 
            {
                Name = list.Name,
                Description = list.Description,
                UserId = userId
            };

            foreach (var book in list.Books)
            {
                listData.BookLists.Add(new BookList { ListId = listData.Id, BookId = book.Id });
            }

            this.data.Lists.Add(listData);
            this.data.SaveChanges();
        }
    }
}
