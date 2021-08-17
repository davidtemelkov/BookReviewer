namespace BookReviewer.Test.Controllers
{
    using BookReviewer.Controllers;
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Authors;
    using MyTested.AspNetCore.Mvc;
    using System;
    using System.Globalization;
    using System.Linq;
    using Xunit;

    using static Data.DataConstants;

    public class BooksControllerTest
    {
        //[Fact]
        //public void GetAddShouldReturnView()
        //   => MyMvc
        //       .Pipeline()
        //       .ShouldMap("/Books/Add")
        //       .To<BooksController>(b => b.Add())
        //       .Which()
        //        .ShouldHave()
        //        .ActionAttributes(attributes => attributes
        //            .RestrictingForAuthorizedRequests())
        //        .AndAlso()
        //       .ShouldReturn()
        //       .View();
    }
}
