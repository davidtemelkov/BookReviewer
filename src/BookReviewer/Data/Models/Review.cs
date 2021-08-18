namespace BookReviewer.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Review
    {
        public int Id { get; init; }

        [Required]
        public int Stars { get; set; }

        [MaxLength(ReviewTextMaxValue)]
        public string Text { get; set; }

        public DateTime DateAdded { get; init; } = DateTime.UtcNow;

        [Required]
        public int BookId { get; set; }

        public Book Book { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
