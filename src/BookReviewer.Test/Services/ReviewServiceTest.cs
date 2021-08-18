namespace BookReviewer.Test.Services
{
    using BookReviewer.Data;
    using BookReviewer.Data.Models;
    using BookReviewer.Infrastructure;
    using BookReviewer.Services.Reviews;
    using BookReviewer.Models.Reviews;

    using AutoMapper;
    using System.Linq;
    using Xunit;

    public class ReviewServiceTest
    {
        private readonly BookReviewerDbContext data;

        public ReviewServiceTest()
        {
            data = TestStartup.GetContext();
        }

        [Fact]
        public void Create()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var reviewService = new ReviewService(data,
                mapper);

            var user = new User { UserName = "TestUsername" };

            var book = new Book { };

            var inputModel = new ReviewFormModel
            {
                Stars = "1",
                Text = "TestName"
            };

            //Act
            reviewService.Create(book.Id.ToString(), user.Id, inputModel);
            var createdReview = this.data.Reviews.FirstOrDefault(r => r.Text == inputModel.Text);

            //Assert
            Assert.NotNull(createdReview);
            Assert.Equal(createdReview.Text, inputModel.Text);
        }

        [Fact]
        public void Edit()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var reviewService = new ReviewService(data,
                mapper);

            var book = new Book { };

            var review = new Review 
            { 
                Stars = 1,
                Text = "TestText", 
                Book = book 
            };

            var inputModel = new ReviewFormModel 
            { 
                Stars = "2",
                Text = "EditedTestText" 
            };

            //Act
            this.data.Books.Add(book);
            this.data.Reviews.Add(review);
            this.data.SaveChanges();

            reviewService.Edit(book.Id.ToString(), inputModel);
            var editedReview = this.data.Reviews.FirstOrDefault(r => r.Text == inputModel.Text);

            //Assert
            Assert.NotNull(editedReview);
            Assert.Equal(editedReview.Text, inputModel.Text);
            Assert.Equal(editedReview.Stars, int.Parse(inputModel.Stars));
        }

        [Fact]
        public void Details()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var reviewService = new ReviewService(data,
                mapper);

            var book = new Book { };

            var review = new Review
            {
                Stars = 1,
                Text = "TestText",
                Book = book
            };

            //Act
            this.data.Books.Add(book);
            this.data.Reviews.Add(review);
            this.data.SaveChanges();

            var details = reviewService.Details(review.Id.ToString());

            //Assert
            Assert.NotNull(details);
            Assert.Equal(details.Text, review.Text);
            Assert.Equal(details.Stars, review.Stars.ToString());
        }

        [Fact]
        public void GetUserReviews()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var reviewService = new ReviewService(data,
                mapper);

            var book = new Book { };

            var user = new User { };

            var review = new Review
            {
                Stars = 1,
                Text = "TestText",
                Book = book,
                User = user
            };

            //Act
            this.data.Books.Add(book);
            this.data.Users.Add(user);
            this.data.Reviews.Add(review);
            this.data.SaveChanges();

            var userReviews = reviewService.GetUserReviews(user.Id);

            //Assert
            Assert.NotNull(userReviews);
        }

        [Fact]
        public void UserOwnsReview()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var reviewService = new ReviewService(data,
                mapper);

            var book = new Book { };

            var user = new User { };

            var review = new Review
            {
                Stars = 1,
                Text = "TestText",
                Book = book,
                User = user
            };

            //Act
            this.data.Books.Add(book);
            this.data.Users.Add(user);
            this.data.Reviews.Add(review);
            this.data.SaveChanges();

            var userOwnsReview = reviewService.UserOwnsReview(user.Id, review.Id.ToString());

            //Assert
            Assert.True(userOwnsReview);
        }

        [Fact]
        public void Delete()
        {
            //Arrange
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var reviewService = new ReviewService(data,
                mapper);

            var book = new Book { };

            var review = new Review
            {
                Stars = 1,
                Text = "TestText",
                Book = book
            };

            //Act
            this.data.Books.Add(book);
            this.data.Reviews.Add(review);
            this.data.SaveChanges();

            reviewService.Delete(review.Id.ToString());

            //Assert
            Assert.Null(this.data.Reviews.Find(review.Id));
        }
    }
}
