namespace BookReviewer.Test.Routing
{
    using BookReviewer.Controllers;

    using BookReviewer.Models.Books;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class BooksControllerTest
    {
        [Fact]
        public void AddRouteShouldbeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Books/Add")
            .To<BooksController>(b => b
                .Add());

        [Fact]
        public void AddRouteWithPostMethodShouldBeMapped()
           => MyRouting
           .Configuration()
           .ShouldMap(request => request
               .WithPath("/Books/Add")
               .WithMethod(HttpMethod.Post))
           .To<BooksController>(b => b
               .Add(new BookFormModel()));

        [Theory]
        [InlineData("1")]
        public void EditRouteShouldBeMapped(string id)
          => MyRouting
          .Configuration()
          .ShouldMap($"/Books/Edit/{id}")
          .To<BooksController>(b => b
              .Edit(id));

        [Theory]
        [InlineData("1")]
        public void EditWithPostMethodRouteShouldBeMapped(string id)
           => MyRouting
           .Configuration()
           .ShouldMap(request => request
                .WithPath($"/Books/Edit/{id}")
                .WithMethod(HttpMethod.Post))
           .To<BooksController>(b => b
               .Edit(id, new BookFormModel()));

        [Theory]
        [InlineData("1")]
        public void DetailsRouteShouldBeMapped(string id)
          => MyRouting
          .Configuration()
          .ShouldMap($"/Books/Details/{id}")
          .To<BooksController>(b => b
              .Details(id));

        [Theory]
        [InlineData("TitleTest", "GenreTest")]
        public void SearchRouteShouldBeMapped(string title, string genre)
          => MyRouting
          .Configuration()
          .ShouldMap($"/Books/Search?SearchTerm={title}&Genre={genre}")
          .To<BooksController>(b => b
              .Search(title, genre));
    }
}
