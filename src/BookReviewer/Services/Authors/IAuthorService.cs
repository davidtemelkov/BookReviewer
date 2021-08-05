namespace BookReviewer.Services.Authors
{
    using BookReviewer.Models.Authors;

    public interface IAuthorService
    {
        void Create(string name,
            string dateOfBirth,
            string details,
            string pictureUrl);

        AuthorDetailsViewModel Details(string id);
    }
}
