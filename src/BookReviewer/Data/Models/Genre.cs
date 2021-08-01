namespace BookReviewer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Genre
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }

        public ICollection<BookGenre> BookGenres { get; init; } 
    }
}
