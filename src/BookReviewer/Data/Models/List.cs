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

        [Required]
        [MaxLength(ListMaxDescription)]
        public string Description { get; set; }

        public ICollection<UserBookList> UserBookLists { get; init; } = new List<UserBookList>();
    }
}
