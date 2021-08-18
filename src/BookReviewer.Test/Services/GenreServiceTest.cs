namespace BookReviewer.Test.Services
{
    using BookReviewer.Data;
    using BookReviewer.Services.Genres;

    using Xunit;

    public class GenreServiceTest
    {
        private readonly BookReviewerDbContext data;

        public GenreServiceTest()
        {
            data = TestStartup.GetContext();
        }

        [Fact]
        public void GetGenres()
        {
            //Arrange
            var genreService = new GenreService(data);

            //Act
            var genres = genreService.GetGenres();

            //Assert
            Assert.NotNull(genres);
        }
    }
}
