namespace BookReviewer.Models.Authors
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AuthorFormModel
    {
        [Required]
        [StringLength(AuthorMaxName,
            MinimumLength = AuthorMinName)]
        public string Name { get; init; }

        [StringLength(AuthorDateOfBirthChars,
            MinimumLength = AuthorDateOfBirthChars,
            ErrorMessage = "Format must be dd.mm.yyyy")]
        public string DateOfBirth { get; init; }

        [Required]
        [StringLength(AuthorMaxDetails,
            MinimumLength = AuthorMinName,
            ErrorMessage = "Author details must be between {2} and {1} characters!")]
        public string Details { get; init; }

        [Required]
        [Url]
        public string PictureUrl { get; init; }
    }
}
