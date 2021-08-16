namespace BookReviewer.Services.Users
{
    using AutoMapper;
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Users;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly BookReviewerDbContext data;
        private readonly IMapper mapper;

        public UserService(BookReviewerDbContext data,
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public UserProfileViewModel Profile(string id)
        {
            var user = this.data.Users.FirstOrDefault(u => u.Id == id);

            var profile = this.mapper.Map<UserProfileViewModel>(user);

            return profile;
        }

        public void ChangeProfilePicture(string id, ChangeProfilePictureFormModel picture)
        {
            this.GetUserById(id).ProfilePicture = picture.PictureUrl;
            this.data.SaveChanges();
        }

        public User GetUserById(string id) 
            => this.data.Users.FirstOrDefault(u => u.Id == id);
    }
}
