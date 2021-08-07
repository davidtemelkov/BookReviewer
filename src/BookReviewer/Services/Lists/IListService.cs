namespace BookReviewer.Services.Lists
{
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Lists;
    using System.Collections.Generic;

    public interface IListService
    {
        void Create(string userId, ListFormModel list);
    }
}
