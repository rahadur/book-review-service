using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using BookReview.Dtos.WebApi;
using BookReview.WebApi;
using BookReview.WebApi.Exeptions;
using BookReview.Infrastructure;
using BookReview.Domain.Entities;
using BookReview.Infrastructure.DataContext;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = ModleStateValidator.GenerateErrorResponse;
});

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddIdentity<User, UserRole>()
	.AddEntityFrameworkStores<BookReviewContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperDtoProfile));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();



