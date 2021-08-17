namespace BookReviewer.Test.Routing
{
    using BookReviewer.Controllers;
    using BookReviewer.Models.Authors;

    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class AuthorsControllerTest
    {
        [Fact]
        public void AddRouteShouldbeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Authors/Add")
            .To<AuthorsController>(a => a
                .Add());

        [Fact]
        public void AddRouteWithPostMethodShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
                .WithPath("/Authors/Add")
                .WithMethod(HttpMethod.Post))
            .To<AuthorsController>(a => a
                .Add(new AuthorFormModel()));

        [Theory]
        [InlineData("1")]
        public void EditRouteShouldBeMapped(string id)
           => MyRouting
           .Configuration()
           .ShouldMap($"/Authors/Edit/{id}")
           .To<AuthorsController>(a => a
               .Edit(id));

        [Theory]
        [InlineData("1")]
        public void EditWithPostMethodRouteShouldBeMapped(string id)
           => MyRouting
           .Configuration()
           .ShouldMap(request => request 
                .WithPath($"/Authors/Edit/{id}")
                .WithMethod(HttpMethod.Post))
           .To<AuthorsController>(a => a
               .Edit(id, new AuthorFormModel()));

        [Theory]
        [InlineData("1")]
        public void DetailsRouteShouldBeMapped(string id)
          => MyRouting
          .Configuration()
          .ShouldMap($"/Authors/Details/{id}")
          .To<AuthorsController>(a => a
              .Details(id));
    }
}
