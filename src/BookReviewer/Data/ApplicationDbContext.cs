namespace BookReviewer.Data
{
    using BookReviewer.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Book> Books { get; init; }

        public DbSet<Author> Authors { get; init; }

        public DbSet<Genre> Genres { get; init; }

        public DbSet<Review> Reviews { get; init; }

        public DbSet<List> Lists { get; init; }

        public DbSet<BookGenre> BookGenres { get; init; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
