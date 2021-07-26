namespace BookReviewer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Author
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(AuthorMaxName)]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; } 

        [Required]
        [MaxLength(AuthorMaxDetails)]
        public string Details { get; set; }

        public ICollection<Book> Books { get; init; } = new List<Book>();
    }
}
