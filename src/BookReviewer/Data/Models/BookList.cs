namespace BookReviewer.Data.Models
{
    public class BookList
    {
        public int Id { get; init; }

        public int ListId { get; init; }

        public List List { get; init; }

        public int BookId { get; init; }

        public Book Book { get; init; }
    }
}
