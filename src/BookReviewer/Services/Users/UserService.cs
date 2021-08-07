namespace BookReviewer.Services.Users
{
    using BookReviewer.Data;
    using BookReviewer.Models.Users;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly BookReviewerDbContext data;

        public UserService(BookReviewerDbContext data)
        {
            this.data = data;
        }

        public UserProfileViewModel Profile(string id)
        {
            var profile = new UserProfileViewModel
            {
                Id = id,
                Username = this.data.Users.FirstOrDefault(u => u.Id == id).UserName,
                ProfilePictureUrl = this.data.Users.FirstOrDefault(u => u.Id == id).ProfilePicture,
                AuthorId = this.data.Users.FirstOrDefault(u => u.Id == id).AuthorId
            };

            return profile;
        }
    }
}
