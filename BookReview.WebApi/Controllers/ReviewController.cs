
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
        var responses = mapper.Map<List<ReviewResponse>>(reviews);
        return Ok(responses);
    }


    [HttpGet("{id}")]
    public ActionResult<ReviewResponse> GetById(int id)
    {
        var review = reviewRepository.Includes(r => r.Book).FirstOrDefault(r => r.Id == id);

        if (review == null)
        {
            return NotFound(new { });
        }

        var response = mapper.Map<ReviewResponse>(review);
        return Ok(response);
    }


    [HttpPost]
    public ActionResult<ReviewResponse> Create([FromBody] ReviewRequest reviewReq)
    {
        var book = bookRepository
            .Includes(book => book.Reviews)
            .FirstOrDefault(book => book.Id == reviewReq.BookId);

        if (book == null)
        {
            return NotFound(new { });
        }

        // Add new review
        var review = mapper.Map<Review>(reviewReq);
        book.Reviews.Add(review);

        // Update Book Rating Field
        book.Rating = book.Reviews.Average(review => review.Rating);
        bookRepository.Update(book);
        SaveReview();

        var response = mapper.Map<ReviewResponse>(review);
        return Ok(response);
    }


    [HttpPatch("{id}/ReviewText")]
    public ActionResult<ReviewResponse> Update(int id, [FromBody] ReviewTextRequest reviewText)
    {
        if (id != reviewText.Id)
        {
            return BadRequest("Missmatch Review Ids");
        }

        var review = reviewRepository.FindById(id);
        if (review == null)
        {
            return NotFound(new { });
        }

        mapper.Map(reviewText, review);
        reviewRepository.Update(review);
        SaveReview();

        var response = mapper.Map<ReviewResponse>(review);
        return Ok(response);
    }


    private void SaveReview()
    {
        unitOfWork.Commit();
    }
}