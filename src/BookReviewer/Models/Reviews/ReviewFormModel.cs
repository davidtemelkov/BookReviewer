namespace BookReviewer.Models.Reviews
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class ReviewFormModel
    {
        [Required(ErrorMessage = "This field is required!")]
        [Range(ReviewMinStars,
            ReviewMaxStars,
            ErrorMessage = "Rating must be between {1} and {2}")]
        public int Stars { get; init; }

        public string Text { get; init; }
       
        public string UserId { get; init; }
       
        public int BookId {get;init;}
    }
}
