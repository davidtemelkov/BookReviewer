namespace BookReviewer.Test.Routing
{
    using BookReviewer.Controllers;

    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShouldbeMapped()
           => MyRouting
           .Configuration()
           .ShouldMap("/Home/Index")
           .To<HomeController>(h => h
               .Index());
    }
}
