namespace BookReviewer.Data
{
    public class DataConstants
    {
        public const int UsernameMaxValue = 20;
        public const int UserPasswordMaxValue = 20;

        public const int BookMaxTitle = 45;
        public const int BookMinTitle = 1;

        public const int BookMaxDescription = 2000;
        public const int BookMinDescription = 200;

        public const int BookMaxPages = 3000;
        public const int BookMinPages = 1;

        public const int BookYearMaxChars = 4;
        public const int BookYearMinChars = 3;

        public const int AuthorMaxName = 30;
        public const int AuthorMinName = 5;
        public const int AuthorMaxDetails = 1000;

        public const int ReviewMaxStars = 5;
        public const int ReviewTextMaxValue = 1000;

        public const int ListMaxName = 30;
        public const int ListMaxDescription = 300;

        public const string UserDefaultProfilePicture = "https://www.birchcommunityservices.org/wp-content/uploads/2020/04/no-profile-picture-icon-10.jpg";
    }
}
