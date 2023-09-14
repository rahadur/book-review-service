
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
	}
}

