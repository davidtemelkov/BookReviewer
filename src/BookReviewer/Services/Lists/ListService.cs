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
          
        }
    }
}
