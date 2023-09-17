using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookReview.Infrastructure.Common;
using BookReview.Infrastructure.DataContext;
using BookReview.Infrastructure.Repositories;

namespace BookReview.Infrastructure;

public static class Infrastructure
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{

		services.AddDbContext<BookReviewContext>(options =>
		{
			options.UseSqlServer(
				configuration.GetConnectionString("BookReview"),
				b => b.MigrationsAssembly("BookReview.WebApi")
			);
		});

		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IAuthorRepository, AuthorRepository>();
		services.AddScoped<IBookRepository, BookRepository>();
		services.AddScoped<IReviewRepository, ReviewRepository>();
		services.AddScoped<ICommentRepository, CommentRepository>();

		return services;
	}
}

