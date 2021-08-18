namespace BookReviewer.Models.Users
{
    using BookReviewer.Data.Models;

    using System.Collections.Generic;

    public class AllReviewsViewModel
    {
        public ICollection<Review> Reviews { get; set; }
    }
}
