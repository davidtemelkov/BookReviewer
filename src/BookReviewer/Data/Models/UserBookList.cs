namespace BookReviewer.Data.Models
{
    public class UserBookList
    {
        public int Id { get; init; }

        public int ListId { get; init; }

        public List List { get; init; }

        public int BookId { get; init; }

        public Book Book { get; init; }

        public int UserId { get; init; }

        public int User { get; init; }
    }
}
