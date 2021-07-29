namespace BookReviewer.Models.Books
{
    using BookReviewer.Data.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddBookFormModel
    {
        [Required]
        public string Title { get; init; }

        [Required]
        public string Author { get; init; }

        [Required]
        [Display(Name = "Cover")]
        public string CoverUrl { get; init; }

        [Required]
        [Display(Name = "Year published")]
        public string ReleaseDate { get; init; }

        [Required]
        //[MaxLength(BookMaxPages)]
        public int Pages { get; init; }

        [Required]
        //[MaxLength(BookMaxDescription)]
        public string Description { get; init; }

        [Display(Name = "Genres")]
        public ICollection<string> BookGenres { get; init; } 
        public IEnumerable<BookGenresViewModel> Genres { get; set; } 
    }
}
