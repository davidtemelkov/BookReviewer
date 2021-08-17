namespace BookReviewer.Test.Routing
{
    using BookReviewer.Controllers;
    using BookReviewer.Models.Lists;
    using BookReviewer.Models.Reviews;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class ReviewsControllerTest
    {
        [Fact]
        public void AddRouteShouldbeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Reviews/Add")
            .To<ReviewsController>(r => r
                .Add());

        [Theory]
        [InlineData("1")]
        public void AddRouteWithPostMethodShouldBeMapped(string id)
           => MyRouting
           .Configuration()
           .ShouldMap(request => request
               .WithPath($"/Reviews/Add/{id}")
               .WithMethod(HttpMethod.Post))
           .To<ReviewsController>(r => r
               .Add(id, new ReviewFormModel()));

        [Theory]
        [InlineData("1")]
        public void EditRouteShouldBeMapped(string id)
           => MyRouting
           .Configuration()
           .ShouldMap($"/Reviews/Edit/{id}")
           .To<ReviewsController>(r => r
               .Edit(id));

        [Theory]
        [InlineData("1")]
        public void EditWithPostMethodRouteShouldBeMapped(string id)
           => MyRouting
           .Configuration()
           .ShouldMap(request => request
                .WithPath($"/Reviews/Edit/{id}")
                .WithMethod(HttpMethod.Post))
           .To<ReviewsController>(r => r
               .Edit(id, new ReviewFormModel()));

        [Theory]
        [InlineData("1")]
        public void DeleteouteShouldBeMapped(string id)
           => MyRouting
           .Configuration()
           .ShouldMap($"/Reviews/Delete/{id}")
           .To<ReviewsController>(r => r
               .Delete(id));

        [Theory]
        [InlineData("1")]
        public void UserReviewsShouldBeMapped(string id)
          => MyRouting
          .Configuration()
          .ShouldMap($"/Reviews/UserReviews/{id}")
          .To<ReviewsController>(r => r
              .UserReviews(id));
    }
}
