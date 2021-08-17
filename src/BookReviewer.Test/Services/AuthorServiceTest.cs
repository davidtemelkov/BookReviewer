namespace BookReviewer.Test.Services
{
    using AutoMapper;
    using BookReviewer.Data;
    using BookReviewer.Infrastructure;
    using BookReviewer.Models.Authors;
    using BookReviewer.Services.Authors;
    using BookReviewer.Services.Books;
    using BookReviewer.Services.Users;
    using Moq;
    using System;
    using System.Globalization;
    using System.Linq;
    using Xunit;

    using static Data.DataConstants;

    public class AuthorServiceTest
    {
        private readonly BookReviewerDbContext data;

        public AuthorServiceTest()
        {
            data = TestStartup.GetContext();
        }

        [Fact]
        public void AdminCreate()
        {
            //Arrange
            var bookService = new Mock<IBookService>().Object;
            var userService = new Mock<IUserService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var authorService = new AuthorService(data,
                bookService,
                userService,
                mapper);

            var inputModel = new AuthorFormModel
            {
                Name = "TestName",
                DateOfBirth = "10.10.2010",
                Details = AuthorTestDetails,
                PictureUrl = AuthorTestPictureUrl
            };

            //Act
            authorService.AdminCreate(inputModel);
            var createdAuthor = this.data.Authors.FirstOrDefault(a => a.Name == inputModel.Name);

            //Assert
            Assert.NotNull(createdAuthor);
            Assert.Equal(createdAuthor.DateOfBirth, DateTime.Parse("10.10.2010", CultureInfo.InvariantCulture));
        }
    }
}
