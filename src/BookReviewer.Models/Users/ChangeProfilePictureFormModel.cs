namespace BookReviewer.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class ChangeProfilePictureFormModel
    {
        [Required]
        [Url]
        public string PictureUrl { get; init; }
    }
}
