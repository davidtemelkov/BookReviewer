namespace BookReviewer.Models.Lists
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class ListFormModel
    {
        [Required]
        [StringLength(ListMaxName,
            MinimumLength = ListMinName,
            ErrorMessage = "The title of the book must be between {2} and {1} characters!")]
        public string Name { get; set; }

        [StringLength(ListMaxDescription,
            ErrorMessage = "The title of the book must be between less than {1} characters!")]
        public string Description { get; set; }
    }
}
