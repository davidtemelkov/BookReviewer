namespace BookReviewer.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public string ProfilePicture { get; set; }

        public ICollection<Review> Reviews { get; init; } = new List<Review>();

        public ICollection<List> Lists { get; init; } = new List<List>();
    }
}
