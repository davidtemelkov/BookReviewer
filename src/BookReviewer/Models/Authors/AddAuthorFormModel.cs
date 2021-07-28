namespace BookReviewer.Models.Authors
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddAuthorFormModel
    {
        [Required]
        [MaxLength(AuthorMaxName)]
        [MinLength(AuthorMinName)]
        public string Name { get; init; }

        [Required]
        public string DateOfBirth { get; init; }

        [Required]
        [MaxLength(AuthorMaxDetails)]
        public string Details { get; init; }
        
        [Required]
        [Url]
        public string PictureUrl { get; init; }
    }
}
