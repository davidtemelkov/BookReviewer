namespace BookReviewer.Test
{
    using BookReviewer.Data;

    using Microsoft.EntityFrameworkCore;
    using System;

    public class TestStartup
    {
        public static BookReviewerDbContext GetContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<BookReviewerDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            return new BookReviewerDbContext(dbContextOptions);
        }
    }
}
