namespace BookReviewer.Test.Routing
{
    using BookReviewer.Controllers;
    using BookReviewer.Models.Users;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class UsersControllerTest
    {
        [Theory]
        [InlineData("1")]
        public void ProfileRouteShouldBeMapped(string id)
          => MyRouting
          .Configuration()
          .ShouldMap($"/Users/Profile/{id}")
          .To<UsersController>(u => u
              .Profile(id));

        [Theory]
        [InlineData("1")]
        public void ChangeProfilePictureRouteShouldBeMapped(string id)
          => MyRouting
          .Configuration()
          .ShouldMap($"/Users/ChangeProfilePicture/{id}")
          .To<UsersController>(u => u
              .ChangeProfilePicture(id));

        [Theory]
        [InlineData("1")]
        public void ChangeProfilePictureWithPostMethodRouteShouldBeMapped(string id)
          => MyRouting
          .Configuration()
          .ShouldMap(request => request
               .WithPath($"/Users/ChangeProfilePicture/{id}")
               .WithMethod(HttpMethod.Post))
          .To<UsersController>(u => u
              .ChangeProfilePicture(id, new ChangeProfilePictureFormModel()));
    }
}
