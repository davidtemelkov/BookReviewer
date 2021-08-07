namespace BookReviewer.Services.Users
{
    using BookReviewer.Models.Users;

    public interface IUserService
    {
        UserProfileViewModel Profile(string id);
    }
}
