namespace BookReviewer.Test.Services
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Infrastructure;
    using BookReviewer.Models.Books;
    using BookReviewer.Services.Books;
    using BookReviewer.Services.Genres;

    using AutoMapper;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Xunit;

    using static Data.DataConstants;

    public class BookServiceTest
    {
        private readonly BookReviewerDbContext data;

        public BookServiceTest()
        {
            data = TestStartup.GetContext();
        }

        [Fact]
        public void AdminCreate()
        {
            //Arrange
            var genreService = new Mock<IGenreService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var bookService = new BookService(data,
                genreService,
                mapper);

            var inputModel = new BookFormModel
            {
                Title = "TestTitle",
                YearPublished = "2010",
                Description = TestDetails,
                CoverUrl = TestPictureUrl,
                Pages = 200,
                Author = "TestAuthor",
                BookGenres = new List<string> { "Romance", "Fiction" }
            };

            //Act
            bookService.AdminCreate(inputModel);
            var createdBook = this.data.Books.FirstOrDefault(a => a.Title == inputModel.Title);

            //Assert
            Assert.NotNull(createdBook);
            Assert.Equal(createdBook.Title, inputModel.Title);
        }

        [Fact]
        public void UserCreate()
        {
            //Arrange
            var genreService = new Mock<IGenreService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var bookService = new BookService(data,
                genreService,
                mapper);

            var inputModel = new BookFormModel
            {
                Title = "TestTitle",
                YearPublished = "2010",
                Description = TestDetails,
                CoverUrl = TestPictureUrl,
                Pages = 200,
                Author = "TestAuthor",
                BookGenres = new List<string> { "Romance", "Fiction" }
            };

            var user = new User
            {
                Id = "UserTestId",
                AuthorId = 1
            };

            //Act
            bookService.UserCreate(user, inputModel);
            var createdBook = this.data.Books.FirstOrDefault(a => a.Title == inputModel.Title);

            //Assert
            Assert.NotNull(createdBook);
            Assert.Equal(createdBook.Title, inputModel.Title);
        }

        [Fact]
        public void Edit()
        {
            //Arrange
            var genreService = new Mock<IGenreService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var bookService = new BookService(data,
                genreService,
                mapper);

            var author = new Author {
                Name = "TestName",
                DateOfBirth = DateTime.Parse("10.10.2010", CultureInfo.InvariantCulture),
                Details = TestDetails,
                PictureUrl = TestPictureUrl
            };

            var book = new Book
            {
                Title = "TestTitle",
                YearPublished = "2010",
                Description = TestDetails,
                CoverUrl = TestPictureUrl,
                Pages = 200,
                Author = author
            };

            var inputModel = new BookFormModel
            {
                Title = "EditedTestTitle",
                YearPublished = "2010",
                Description = TestDetails,
                CoverUrl = TestPictureUrl,
                Pages = 200,
            };

            //Act
            this.data.Authors.Add(author);
            this.data.Books.Add(book);
            this.data.SaveChanges();

            bookService.Edit(book.Id.ToString(), inputModel);
            var editedBook = this.data.Books.FirstOrDefault(b => b.Title == inputModel.Title);

            //Assert
            Assert.NotNull(editedBook);
            Assert.Equal("EditedTestTitle", editedBook.Title);
        }

        [Fact]
        public void Details()
        {
            //Arrange
            var genreService = new Mock<IGenreService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var bookService = new BookService(data,
                genreService,
                mapper);

            var author = new Author
            {
                Name = "TestName",
                DateOfBirth = DateTime.Parse("10.10.2010", CultureInfo.InvariantCulture),
                Details = TestDetails,
                PictureUrl = TestPictureUrl
            };

            var book = new Book
            {
                Title = "TestTitle",
                YearPublished = "2010",
                Description = TestDetails,
                CoverUrl = TestPictureUrl,
                Pages = 200,
                Author = author
            };

            //Act
            this.data.Authors.Add(author);
            this.data.Books.Add(book);
            this.data.SaveChanges();

            var details = bookService.BookDetails(book.Id.ToString());

            //Assert
            Assert.NotNull(details);
            Assert.Equal(details.Title, book.Title);
        }

        [Theory]
        [InlineData("TestTitle", "Any")]
        public void SearchBooks(string title, string genre)
        {
            var genreService = new Mock<IGenreService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var bookService = new BookService(data,
                genreService,
                mapper);

            var user = new User
            {
                UserName = "TestUsername"
            };

            var book = new Book
            {
                Title = "TestTitle",
                YearPublished = "2010",
                Description = TestDetails,
                CoverUrl = TestPictureUrl,
                Pages = 200,
                IsAccepted = true
            };

            var review = new Review
            {
                Stars = 5,
                Book = book,
                User = user
            };

            //Act
            this.data.Users.Add(user);
            this.data.Books.Add(book);
            this.data.Reviews.Add(review);
            this.data.SaveChanges();

            var searchBooks = bookService.SearchBooks(title, genre);

            //Assert
            Assert.NotNull(searchBooks);
        }
    }
}
