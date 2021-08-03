﻿namespace BookReviewer.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    using static Data.DataConstants;

    public class User : IdentityUser
    {
        public string ProfilePicture { get; set; } = UserDefaultProfilePicture;

        public bool isAuthor { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public ICollection<Review> Reviews { get; init; } = new List<Review>();

        public ICollection<List> Lists { get; init; } = new List<List>();
    }
}
