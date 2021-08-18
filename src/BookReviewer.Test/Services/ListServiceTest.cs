namespace BookReviewer.Test.Services
{
    using AutoMapper;
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Infrastructure;
    using BookReviewer.Models.Lists;
    using BookReviewer.Services.Books;
    using BookReviewer.Services.Lists;

    using System.Linq;
    using Moq;
    using Xunit;
    using BookReviewer.Services.Genres;

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

        //[Fact]
        //public void GetListDetails()
        //{
        //    //Arrange
        //    var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
        //    var mapper = mapperConfig.CreateMapper();

        //    var genreService = new GenreService(data);

        //    var bookService = new BookService(data,
        //        genreService,
        //        mapper);

        //    var listService = new ListService(data,
        //        bookService,
        //        mapper);

        //    var list = new List
        //    {
        //        Name = "TestName"
        //    };

        //    //Act
        //    var details = listService.GetListDetails(list.Id.ToString());

        //    //Assert
        //    Assert.NotNull(details);
        //}

    }
}
