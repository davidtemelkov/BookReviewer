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
        public DateTime ReleaseDate { get; set; }

        [Required]
        public ICollection<Genre> Genres { get; init; } = new List<Genre>();

        [Required]
        [MaxLength(BookMaxPages)]
        public int Pages { get; set; }

        [Required]
        [MaxLength(BookMaxDescription)]
        public string Description { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public bool IsAccepted { get; set; }
    }
}
