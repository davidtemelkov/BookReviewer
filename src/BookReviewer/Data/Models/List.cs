namespace BookReviewer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class List
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(ListMaxName)]
        public string Name { get; set; }

        [MaxLength(ListMaxDescription)]
        public string Description { get; set; }

        public string UserId { get; init; }

        public User User { get; init; }

        public ICollection<BookList> BookLists { get; init; } = new List<BookList>();
    }
}
