namespace BookReviewer.Test
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

    public class AuthorsControllerTest
    {
        [Fact]
        public void GetAddShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap("/Authors/Add")
                .To<AuthorsController>(a => a.Add())
                .Which()
                .ShouldReturn()
                .View();
        
        [Theory]
        [InlineData("TestName", "10.10.2010", TestDetails, TestPictureUrl)]
        public void PostAddShouldSaveAuhtorHaveValidModelStateAndRedirect(string name, string dateOfBirth, string details, string pictureUrl)
            => MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Authors/Add")
                    .WithMethod(HttpMethod.Post)
                    .WithFormFields(new
                    {
                        Name = name,
                        DateOfBirth = dateOfBirth,
                        Details = details,
                        PictureUrl = pictureUrl
                    })
                    .WithUser(u => u.WithIdentifier(UserTestId))
                    .WithAntiForgeryToken())
                .To<AuthorsController>(c => c.Add(new AuthorFormModel
                {
                    Name = name,
                    DateOfBirth = dateOfBirth,
                    Details = details,
                    PictureUrl = pictureUrl
                }))
                .Which()
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Author>(author => author
                        .Any(a =>
                            a.Name == name &&
                            a.DateOfBirth == DateTime.ParseExact(dateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture) &&
                            a.Details == details &&
                            a.PictureUrl == pictureUrl)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<HomeController>(c => c.Index()));


        //[Theory]
        //[InlineData("1")]
        //public void GetEditShouldBeForAuthorizedUsersAndReturnView(string id)
        //    => MyPipeline
        //        .Configuration()
        //        .ShouldMap(request => request
        //            .WithPath($"/Authors/Edit/{id}")
        //            .WithUser(u => u.WithIdentifier(UserTestId)))
        //        .To<AuthorsController>(a => a.Edit(id))
        //        .Which()
        //        .ShouldHave()
        //        .ActionAttributes(attrs => attrs
        //            .RestrictingForHttpMethod(HttpMethod.Get)
        //            .RestrictingForAuthorizedRequests())
        //        .AndAlso()
        //        .ShouldReturn()
        //        .View();

    }
}
