
using Microsoft.AspNetCore.Mvc;
using BookReview.WebApi.Dtos;
using BookReview.WebApi.Repositories;
using AutoMapper;
using BookReview.Entities.Models;

namespace BookReview.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    private IReviewRepository reviewRepository;
    private IBookRepository bookRepository;
    private IUnitOfWork unitOfWork;
    private IMapper mapper;

    public ReviewController(IReviewRepository reviewRepository, IBookRepository bookRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.reviewRepository = reviewRepository;
        this.bookRepository = bookRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }


    [HttpGet(Name = "Reviews")]
    public ActionResult<IEnumerable<ReviewResponse>> GetAll()
    {
        var reviews = reviewRepository.FindAll();
        if(reviews == null)
        {
            return Ok(new List<ReviewResponse>());
        }
        var reviewsDto = mapper.Map<List<ReviewResponse>>(reviews);

        return Ok(reviewsDto);
    }


    [HttpPost]
    public ActionResult<ReviewResponse> Create([FromBody] ReviewRequest review)
    {
        var book = bookRepository
            .Includes(book => book.Reviews)
            .FirstOrDefault(book => book.Id == review.BookId);

        if(book == null) 
        {
            return NotFound(new {});
        }

        var newReview = mapper.Map<Review>(review);
        book.Reviews.Add(newReview);

        // Update Book Rating Field
        book.Rating = book.Reviews.Average(review => review.Rating);
        bookRepository.Update(book); 

        SaveReview();

        return Ok(mapper.Map<ReviewResponse>(newReview));
    }


    private void SaveReview()
    {
        unitOfWork.Commit();
    }
}