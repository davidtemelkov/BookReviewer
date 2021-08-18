namespace BookReviewer.Test
{
    using BookReviewer.Data;

    using Microsoft.Extensions.Configuration;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
           : base(configuration)
        {
        }

        public static BookReviewerDbContext GetContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<BookReviewerDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            return new BookReviewerDbContext(dbContextOptions);
        }
    }
}
