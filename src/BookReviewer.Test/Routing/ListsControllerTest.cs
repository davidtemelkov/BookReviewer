namespace BookReviewer.Test.Routing
{
    using BookReviewer.Controllers;
    using BookReviewer.Models.Lists;

    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ListsControllerTest
    {
        [Theory]
        [InlineData("UserTestId")]
        public void UserListsRouteShouldbeMapped(string id)
           => MyRouting
           .Configuration()
           .ShouldMap($"/Lists/UserLists/{id}")
           .To<ListsController>(l => l
               .UserLists(id));

        [Fact]
        public void CreateRouteShouldbeMapped()
           => MyRouting
           .Configuration()
           .ShouldMap("/Lists/Create")
           .To<ListsController>(l => l
               .Create());

        [Fact]
        public void CreateRouteWithPostMethodShouldBeMapped()
           => MyRouting
           .Configuration()
           .ShouldMap(request => request
               .WithPath("/Lists/Create")
               .WithMethod(HttpMethod.Post))
           .To<ListsController>(l => l
               .Create(new ListFormModel()));

        [Theory]
        [InlineData("1")]
        public void DetailsRouteShouldBeMapped(string id)
          => MyRouting
          .Configuration()
          .ShouldMap($"/Lists/Details/{id}")
          .To<ListsController>(l => l
              .Details(id));

        [Theory]
        [InlineData("1")]
        public void EditRouteShouldBeMapped(string id)
          => MyRouting
          .Configuration()
          .ShouldMap($"/Lists/Edit/{id}")
          .To<ListsController>(l => l
              .Edit(id));


        [Theory]
        [InlineData("1")]
        public void DeleteRouteShouldBeMapped(string id)
          => MyRouting
          .Configuration()
          .ShouldMap($"/Lists/Delete/{id}")
          .To<ListsController>(l => l
              .Delete(id));

        [Theory]
        [InlineData("1")]
        public void AddToListRouteShouldBeMapped(string id)
          => MyRouting
          .Configuration()
          .ShouldMap($"/Lists/AddToList/{id}")
          .To<ListsController>(l => l
              .AddToList(id));

        [Theory]
        [InlineData("1")]
        public void RemoveFromListRouteShouldBeMapped(string id)
          => MyRouting
          .Configuration()
          .ShouldMap($"/Lists/RemoveFromList/{id}")
          .To<ListsController>(l => l
              .RemoveFromList(id));
    }
}
