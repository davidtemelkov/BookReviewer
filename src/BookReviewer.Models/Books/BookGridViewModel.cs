namespace BookReviewer.Models.Books
{
    public class BookGridViewModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public string Author { get; init; }

        public int AuthorId { get; init; }

        public string CoverUrl { get; init; }

        public bool IsAccepted { get; init; }

        public string Genres { get; init; }
    }
}
