namespace BookReviewer.Test.Services
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Infrastructure;
    using BookReviewer.Services.Users;
    using BookReviewer.Models.Users;

    using AutoMapper;
    using Xunit;

    using static Data.DataConstants;

    public class UserServiceTest
    {
        private readonly BookReviewerDbContext data;

        public UserServiceTest()
        {
            data = TestStartup.GetContext();
        }

        [Fact]
        public void Profile()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(data,
                mapper);

            var user = new User { UserName = "TestUsername" };

            //Act
            this.data.Users.Add(user);
            this.data.SaveChanges();

            var profile = userService.Profile(user.Id);

            //Assert
            Assert.NotNull(profile);
        }

        [Fact]
        public void ChangeProfilePicture()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(data,
                mapper);

            var user = new User { UserName = "TestUsername", ProfilePicture = "TestProfilePicture" };

            var inputModel = new ChangeProfilePictureFormModel { PictureUrl = TestPictureUrl };

            //Act
            this.data.Users.Add(user);
            this.data.SaveChanges();

            userService.ChangeProfilePicture(user.Id, inputModel); 

            //Assert
            Assert.Equal(user.ProfilePicture, inputModel.PictureUrl);
        }

        [Fact]
        public void GetUserById()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(data,
                mapper);

            var user = new User { UserName = "TestUsername"};

            //Act
            this.data.Users.Add(user);
            this.data.SaveChanges();

            var getUser = userService.GetUserById(user.Id);

            //Assert
            Assert.NotNull(getUser);
        }
    }
}
