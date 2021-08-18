namespace BookReviewer.Services.Lists
{
    using BookReviewer.Models.Lists;

    public interface IListService
    {
        AllListsViewModel GetUserLists(string id);

        int Create(string userId, ListFormModel list);

        void Delete(string id);

        ListDetailsViewModel GetListDetails(string id);

        void AddBook(string bookId, string listId);

        void RemoveBook(string bookId, string listId);

        bool UserOwnsList(string userId, string listId);
    }
}
