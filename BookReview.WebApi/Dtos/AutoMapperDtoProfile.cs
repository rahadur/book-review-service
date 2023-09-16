
using AutoMapper;
using BookReview.Entities.Models;
using BookReview.WebApi.Dtos;

namespace BookReview.Dtos.WebApi;

public class AutoMapperDtoProfile : Profile
{
	public AutoMapperDtoProfile()
	{
		CreateMap<AuthorRequest, Author>();
		CreateMap<Author, AuthorResponse>();

		// Book
		CreateMap<BookRequest, Book>();
		CreateMap<Book, BookResponse>()
			.ForCtorParam("Title", o => o.MapFrom(src => src.Title))
			.ForCtorParam("Genre", o => o.MapFrom(src => src.Genre));

		// Review
		CreateMap<ReviewRequest, Review>();
		CreateMap<ReviewTextRequest, Review>();
		CreateMap<Review, ReviewResponse>()
			.ForCtorParam("ReviewText", o => o.MapFrom(src => src.ReviewText))
			.ForCtorParam("Rating", o => o.MapFrom(src => src.Rating))
			.ForCtorParam("BookId", o => o.MapFrom(src => src.BookId));

		// Comment
		CreateMap<CommentRequest, Comment>();
		CreateMap<Comment, CommentResponse>()
			.ForCtorParam("CommentText", o => o.MapFrom(src => src.CommentText));
	}
}

