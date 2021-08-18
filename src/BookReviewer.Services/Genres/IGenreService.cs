namespace BookReviewer.Services.Genres
{
    using System.Collections.Generic;

    public interface IGenreService
    {
        IEnumerable<string> GetGenres();
    }
}
