namespace BookReviewer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Book
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(BookMaxTitle)]
        public string Title { get; set; }

        [Required]
        public string CoverUrl { get; set; }

        [Required]
        public string YearPublished { get; set; }

        public ICollection<BookGenre> BookGenres { get; init; } = new List<BookGenre>();

        public ICollection<Review> Reviews { get; init; } = new List<Review>();

        public ICollection<BookList> BookLists { get; init; } = new List<BookList>();

        [Required]
        [MaxLength(BookMaxPages)]
        public int Pages { get; set; }

        [Required]
        [MaxLength(BookMaxDescription)]
        public string Description { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public bool IsAccepted { get; set; }

        public DateTime DateAdded { get; init; } = DateTime.UtcNow;
    }
}
