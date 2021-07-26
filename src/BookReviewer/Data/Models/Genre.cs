namespace BookReviewer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Genre
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }
    }
}
