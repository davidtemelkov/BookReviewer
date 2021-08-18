namespace BookReviewer.Infrastructure
{
    using BookReviewer.Data.Models;
    using BookReviewer.Models.Authors;
    using BookReviewer.Models.Books;
    using BookReviewer.Models.Lists;
    using BookReviewer.Models.Reviews;
    using BookReviewer.Models.Users;

    using AutoMapper;
    using System;
    using System.Globalization;
    using System.Linq;

    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            this.CreateMap<AuthorFormModel, Author>()
                .ForMember(a => a.DateOfBirth, cfg => cfg.MapFrom(a => DateTime.ParseExact(a.DateOfBirth, "dd.MM.yyyy", CultureInfo.InvariantCulture)));

            this.CreateMap<Author, AuthorDetailsViewModel>()
               .ForMember(a => a.DateOfBirth, cfg => cfg.MapFrom(a => a.DateOfBirth.ToString("dd.MM.yyyy")));

            this.CreateMap<AuthorDetailsViewModel, AuthorFormModel>();

            this.CreateMap<BookFormModel, Book>()
                .ForMember(b => b.Author, cfg => cfg.Ignore())
                .ForMember(b => b.BookGenres, cfg => cfg.Ignore());

            this.CreateMap<Book, BookGridViewModel>()
                .ForMember(b => b.Author, cfg => cfg.MapFrom(b => b.Author.Name))
                .ForMember(b => b.Genres, cfg => cfg.MapFrom(b => string.Join(",", b.BookGenres.Select(g => g.Genre.Name))));

            this.CreateMap<Book, BookDetailsViewModel>()
                 .ForMember(b => b.Genres, cfg => cfg.MapFrom(g => string.Join(", ", g.BookGenres.Select(g => g.Genre.Name))));

            this.CreateMap<BookDetailsViewModel, BookFormModel>()
                .ForMember(b => b.Author, cfg => cfg.MapFrom(b => b.AuthorName))
                .ForMember(b => b.Genres, cfg => cfg.Ignore());

            this.CreateMap<ListFormModel, List>();

            this.CreateMap<List, ListDetailsViewModel>();

            this.CreateMap<ReviewFormModel, Review>()
                .ForMember(r => r.Stars, cfg => cfg.MapFrom(r => int.Parse(r.Stars)))
                .ReverseMap();

            this.CreateMap<User, UserProfileViewModel>()
                 .ForMember(u => u.ProfilePictureUrl, cfg => cfg.MapFrom(u => u.ProfilePicture));
        }
    }
}
