namespace BookReviewer.Models.Reviews
{
    public class AddReviewFormModel
    {
        public int Stars { get; init; }

        public string Text { get; init; }

        public string UserId { get; init; }

        public int BookId {get;init;}
    }
}
