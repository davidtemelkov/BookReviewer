namespace BookReviewer.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        public string ProfilePicture { get; set; }

        [Required]
        public bool IsAuthor { get; set; }

        public ICollection<Review> Reviews { get; init; } = new List<Review>();

        public ICollection<List> Lists { get; init; } = new List<List>();
    }
}
