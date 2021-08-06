namespace BookReviewer.Models.Books
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class BookFormModel
    {
        [Required]
        [StringLength(BookMaxTitle,
            MinimumLength = BookMinTitle,
            ErrorMessage = "The title of the book must be between {2} and {1} characters!")]
        public string Title { get; init; }

        [Required]
        [StringLength(AuthorMaxName,
            MinimumLength = AuthorMinName)]
        public string Author { get; init; }

        [Required]
        [Url]
        [Display(Name = "Cover")]
        public string CoverUrl { get; init; }

        [Required]
        [StringLength(BookYearMaxChars, MinimumLength = BookYearMinChars,
            ErrorMessage = "The year must be {1} characters!")]
        [Display(Name = "Year published")]
        public string YearPublished { get; init; }

        [Required]
        [Range(BookMinPages,BookMaxPages,
            ErrorMessage = "The pages of the book must be between {1} and {2}!")]
        public int Pages { get; init; }

        [Required]
        [StringLength(BookMaxDescription, MinimumLength = BookMinDescription,
            ErrorMessage = "The description of the book must be between {2} and {1} characters!")]
        public string Description { get; init; }

        [Display(Name = "Genres")]
        public ICollection<string> BookGenres { get; init; } 

        public IEnumerable<string> Genres { get; set; } 

        public IEnumerable<string> Authors { get; set; }
    }
}
