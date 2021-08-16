namespace BookReviewer.Services.Users
{
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Users;

    public interface IUserService
    {
        UserProfileViewModel Profile(string id);

        void ChangeProfilePicture(string id, ChangeProfilePictureFormModel picture);

        User GetUserById(string id);
    }
}
