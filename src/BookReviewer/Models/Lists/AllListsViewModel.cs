namespace BookReviewer.Models.Lists
{
    using BookReviewer.Data.Models;

    using System.Collections.Generic;

    public class AllListsViewModel
    {
       public string UserId { get; init; }

       public IEnumerable<List> Lists { get; init; }
    }
}
