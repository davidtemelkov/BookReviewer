namespace BookReviewer.Models.Users
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string ProfilePictureUrl { get; set; }

        public int? AuthorId { get; set; }
    }
}
