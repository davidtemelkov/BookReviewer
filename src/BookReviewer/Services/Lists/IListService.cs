namespace BookReviewer.Services.Lists
{
    using BookReviewer.Models.Lists;
    using System.Collections.Generic;

    public interface IListService
    {
        IEnumerable<AllListsViewModel> GetUserLists(string id);

        int Create(string userId, ListFormModel list);

        ListDetailsViewModel GetListDetails(string id);

        void AddBook(string bookId, string listId);
    }
}
