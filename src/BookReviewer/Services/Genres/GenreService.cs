namespace BookReviewer.Services.Genres
{
    using BookReviewer.Data;
    using System.Collections.Generic;
    using System.Linq;

    public class GenreService : IGenreService
    {
        private readonly BookReviewerDbContext data;

        public GenreService(BookReviewerDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<string> GetGenres()
        {
            return this.data
                    .Genres
                    .Select(g => g.Name)
                    .ToList();
        }
    }
}
