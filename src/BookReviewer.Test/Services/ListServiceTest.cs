namespace BookReviewer.Test.Services
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Infrastructure;
    using BookReviewer.Models.Lists;
    using BookReviewer.Services.Books;
    using BookReviewer.Services.Lists;
    using BookReviewer.Services.Genres;

    using AutoMapper;
    using System.Linq;
    using Moq;
    using Xunit;

    using static Data.DataConstants;

    public class ListServiceTest
    {
        private readonly BookReviewerDbContext data;

        public ListServiceTest()
        {
            data = TestStartup.GetContext();
        }

        [Fact]
        public void Create()
        {
            //Arrange
            var bookService = new Mock<IBookService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var listService = new ListService(data,
                bookService,
                mapper);

            var user = new User { UserName = "TestUsername" };

            var inputModel = new ListFormModel
            {
                Name = "TestName"
            };

            //Act
            listService.Create(user.Id, inputModel);
            var createdList = this.data.Lists.FirstOrDefault(a => a.Name == inputModel.Name);

            //Assert
            Assert.NotNull(createdList);
            Assert.Equal(createdList.Name, inputModel.Name);
        }

        [Fact]
        public void GetListDetails()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var genreService = new GenreService(data);

            var bookService = new BookService(data,
                genreService,
                mapper);

            var listService = new ListService(data,
                bookService,
                mapper);

            var list = new List
            {
                Name = "TestName"
            };

            //Act
            this.data.Lists.Add(list);
            this.data.SaveChanges();

            var details = listService.GetListDetails(list.Id.ToString());

            //Assert
            Assert.NotNull(details);
        }

        [Fact]
        public void Delete()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var genreService = new Mock<IGenreService>().Object;

            var bookService = new BookService(data,
                genreService,
                mapper);

            var listService = new ListService(data,
                bookService,
                mapper);

            var list = new List
            {
                Name = "TestName"
            };

            //Act
            this.data.Lists.Add(list);
            this.data.SaveChanges();

            listService.Delete(list.Id.ToString());

            //Assert
            Assert.Null(this.data.Lists.FirstOrDefault(l => l.Id == list.Id));
        }

        [Fact]
        public void AddBook()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var genreService = new Mock<IGenreService>().Object;

            var bookService = new BookService(data,
                genreService,
                mapper);

            var listService = new ListService(data,
                bookService,
                mapper);

            var list = new List
            {
                Name = "TestName"
            };
          
            var book = new Book
            {
                Title = "TestTitle",
                YearPublished = "2010",
                Description = TestDetails,
                CoverUrl = TestPictureUrl,
                Pages = 200,
            };

            //Act
            this.data.Books.Add(book);
            this.data.Lists.Add(list);
            this.data.SaveChanges();

            listService.AddBook(list.Id.ToString(), book.Id.ToString());

            //Assert
            Assert.NotNull(this.data.BookLists.FirstOrDefault(bl => bl.ListId == list.Id && bl.BookId == book.Id));
        }

        [Fact]
        public void RemoveBook()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var genreService = new Mock<IGenreService>().Object;

            var bookService = new BookService(data,
                genreService,
                mapper);

            var listService = new ListService(data,
                bookService,
                mapper);

            var list = new List
            {
                Name = "TestName"
            };

            var book = new Book
            {
                Title = "TestTitle",
                YearPublished = "2010",
                Description = TestDetails,
                CoverUrl = TestPictureUrl,
                Pages = 200,
            };

            var bookList = new BookList
            {
                Book = book,
                List = list
            };

            //Act
            this.data.Books.Add(book);
            this.data.Lists.Add(list);
            this.data.BookLists.Add(bookList);
            this.data.SaveChanges();

            listService.RemoveBook(list.Id.ToString(), book.Id.ToString());

            //Assert
            Assert.Null(this.data.BookLists.FirstOrDefault(bl => bl.ListId == list.Id && bl.BookId == book.Id));
        }

        [Fact]
        public void UserOwnsList()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var genreService = new Mock<IGenreService>().Object;

            var bookService = new BookService(data,
                genreService,
                mapper);

            var listService = new ListService(data,
                bookService,
                mapper);

            var user = new User { UserName = "TestUsername" };

            var list = new List
            {
                Name = "TestName",
                User = user
            };

            //Act
            this.data.Users.Add(user);
            this.data.Lists.Add(list);
            this.data.SaveChanges();

            var isOwner = listService.UserOwnsList(user.Id, list.Id.ToString());

            //Assert
            Assert.True(isOwner);
        }

        [Fact]
        public void GetUserLists()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var genreService = new GenreService(data);

            var bookService = new BookService(data,
                genreService,
                mapper);

            var listService = new ListService(data,
                bookService,
                mapper);

            var user = new User { };

            var list = new List
            {
                Name = "TestName",
                User = user
            };

            //Act
            this.data.Users.Add(user);
            this.data.Lists.Add(list);
            this.data.SaveChanges();

            var userLists = listService.GetUserLists(user.Id);

            //Assert
            Assert.NotNull(userLists);
        }
    }
}
