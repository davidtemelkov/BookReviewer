namespace BookReviewer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Review
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(ReviewMaxStars)]
        public int Stars { get; set; }

        [Required]
        [MaxLength(ReviewTextMaxValue)]
        public string Text { get; set; }

        [Required]
        public string BookId { get; set; }

        public Book Book { get; set; }

        [Required]
        public string UserId { get; set; }

        public string User { get; set; }
    }
}
