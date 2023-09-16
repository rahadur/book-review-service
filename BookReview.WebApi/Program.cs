using Microsoft.EntityFrameworkCore;
using BookReview.WebApi.Context;
using BookReview.WebApi.Repositories;
using BookReview.Dtos.WebApi;
using Microsoft.AspNetCore.Mvc;
using BookReview.WebApi.Exeptions;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.InvalidModelStateResponseFactory = ModleStateValidator.GenerateErrorResponse;
});
builder.Services.AddDbContext<BookReviewContext>(
    options => options.UseSqlServer(config.GetConnectionString("BookReview"))
);
builder.Services.AddAutoMapper(typeof(AutoMapperDtoProfile));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
