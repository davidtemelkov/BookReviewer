namespace BookReviewer.Test.Services
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Infrastructure;
    using BookReviewer.Models.Authors;
    using BookReviewer.Services.Authors;
    using BookReviewer.Services.Books;
    using BookReviewer.Services.Users;

    using AutoMapper;
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
                Details = TestDetails,
                PictureUrl = TestPictureUrl
            };

            //Act
            authorService.AdminCreate(inputModel);
            var createdAuthor = this.data.Authors.FirstOrDefault(a => a.Name == inputModel.Name);

            //Assert
            Assert.NotNull(createdAuthor);
            Assert.Equal(createdAuthor.DateOfBirth, DateTime.Parse("10.10.2010", CultureInfo.InvariantCulture));
        }

        [Fact]
        public void UserCreate()
        {
            //Arrange
            var bookService = new Mock<IBookService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(data,
                mapper);

            var authorService = new AuthorService(data,
                bookService,
                userService,
                mapper);

            var inputModel = new AuthorFormModel
            {
                Name = "TestName",
                DateOfBirth = "10.10.2010",
                Details = TestDetails,
                PictureUrl = TestPictureUrl
            };

            string userId = "TestId";

            var user = new User { Id = userId };

            //Act
            this.data.Users.Add(user);
            this.data.SaveChanges();

            authorService.UserCreate(inputModel, userId);
            var createdAuthor = this.data.Authors.FirstOrDefault(a => a.Name == inputModel.Name);

            //Assert
            Assert.NotNull(createdAuthor);
            Assert.Equal(createdAuthor.DateOfBirth, DateTime.Parse("10.10.2010", CultureInfo.InvariantCulture));
        }

        [Fact]
        public void Edit()
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

            var author = new Author
            {
                Name = "TestName",
                DateOfBirth = DateTime.Parse("10.10.2010", CultureInfo.InvariantCulture),
                Details = TestDetails,
                PictureUrl = TestPictureUrl
            };

            var inputModel = new AuthorFormModel
            {
                Name = "TestEditedName",
                DateOfBirth = "10.10.2010",
                Details = TestDetails,
                PictureUrl = TestPictureUrl
            };

            //Act
            this.data.Authors.Add(author);
            this.data.SaveChanges();

            authorService.Edit(author.Id.ToString(), inputModel);
            var editedAuthor = this.data.Authors.FirstOrDefault(a => a.Name == inputModel.Name);

            //Assert
            Assert.NotNull(editedAuthor);
            Assert.Equal("TestEditedName", editedAuthor.Name);
        }

        [Fact]
        public void Details()
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

            var author = new Author
            {
                Name = "TestName",
                DateOfBirth = DateTime.Parse("10.10.2010", CultureInfo.InvariantCulture),
                Details = TestDetails,
                PictureUrl = TestPictureUrl
            };

            //Act
            this.data.Authors.Add(author);
            this.data.SaveChanges();

            var details = authorService.Details(author.Id.ToString());

            //Assert
            Assert.NotNull(details);
            Assert.Equal(author.Name, details.Name);
        }

        [Fact]
        public void IsAuthor()
        {
            //Arrange
            var bookService = new Mock<IBookService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(data,
                mapper);

            var authorService = new AuthorService(data,
                bookService,
                userService,
                mapper);

            var userAuthor = new User
            {
                Id = "TestId",
                AuthorId = 1
            };

            var userNotAuthor = new User
            {
                Id = "TestId2"
            };

            //Act
            this.data.Users.Add(userAuthor);
            this.data.Users.Add(userNotAuthor);
            this.data.SaveChanges();

            var isAuthor = authorService.IsAuthor(userAuthor.Id);
            var isntAuthor = authorService.IsAuthor(userNotAuthor.Id);

            //Assert
            Assert.True(isAuthor);
            Assert.False(isntAuthor);
        }

        [Fact]
        public void IsCurrentAuthor()
        {
            //Arrange
            var bookService = new Mock<IBookService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(data,
                mapper);

            var authorService = new AuthorService(data,
                bookService,
                userService,
                mapper);

            var author = new Author{ };

            var user = new User
            {
                Id = "TestId"
            };

            //Act
            this.data.Authors.Add(author);
            this.data.Users.Add(user);
            this.data.SaveChanges();

            user.AuthorId = author.Id;

            var isCurrentAuthor = authorService.IsCurrentAuthor(user.Id, author.Id.ToString());

            //Assert
            Assert.True(isCurrentAuthor);
        }

        [Fact]
        public void IsAuthorOfBook()
        {
            //Arrange
            var bookService = new Mock<IBookService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(data,
                mapper);

            var authorService = new AuthorService(data,
                bookService,
                userService,
                mapper);

            var author = new Author { };

            var user = new User
            {
                Id = "TestId",
                AuthorId = author.Id
            };

            var book = new Book {AuthorId = author.Id };

            //Act
            this.data.Authors.Add(author); 
            this.data.Users.Add(user);
            this.data.Books.Add(book);
            this.data.SaveChanges();

            var isAuthorOfBook = authorService.IsAuthorOfBook(user.Id, book.Id.ToString());

            //Assert
            Assert.True(isAuthorOfBook);
        }

        [Fact]
        public void GetAuthors()
        {
            //Arrange
            var bookService = new Mock<IBookService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(data,
                mapper);

            var authorService = new AuthorService(data,
                bookService,
                userService,
                mapper);

            var author = new Author { };
            //Act
            this.data.Authors.Add(author);
            this.data.SaveChanges();

            var authors = authorService.GetAuthors();

            //Assert
            Assert.NotNull(authors);
        }
    }
}
