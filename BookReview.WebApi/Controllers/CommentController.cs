
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BookReview.Entities.Models;
using BookReview.WebApi.Dtos;
using BookReview.WebApi.Repositories;

namespace BookReview.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
	private ICommentRepository commentRepository;
	private IReviewRepository reviewRepository;
	private IUnitOfWork unitOfWork;
	private IMapper mapper;

	public CommentController(ICommentRepository commentRepository, IReviewRepository reviewRepository, IUnitOfWork unitOfWork, IMapper mapper)
	{
		this.commentRepository = commentRepository;
		this.reviewRepository = reviewRepository;
		this.unitOfWork = unitOfWork;
		this.mapper = mapper;
	}
	

	[HttpGet(Name = "GetComments")]
	public ActionResult<IEnumerable<CommentResponse>> GetComments([FromQuery] FilterQuery query)
	{
		var comments = commentRepository.GetPage(query.currentPage, query.pageSize, query.orderBy, query.sort);
		var response = mapper.Map<List<CommentResponse>>(comments);
		return Ok(response);
	}


	[HttpPost]
	[Authorize]
	public ActionResult<CommentResponse> Create([FromBody] CommentRequest commentRequest)
	{
		var review = reviewRepository.Includes(r => r.Comments).FirstOrDefault(r => r.Id == commentRequest.ReviewId);

		if (review == null)
		{
			return NotFound(new { });
		}

		var comment = mapper.Map<Comment>(commentRequest);
		review.Comments.Add(comment);
		SaveComment();

		var response = mapper.Map<CommentResponse>(comment);
		return Ok(response);
	}


	[HttpPut("{id}")]
	[Authorize]
	public ActionResult<CommentResponse> Update(int id, [FromBody] CommentRequest commentReq)
	{
		if (id != commentReq.Id)
		{
			return BadRequest("Missmatch Comment Ids");
		}

		var comment = commentRepository.FindById(id);
		if(comment == null)
		{
			return NotFound(new { });
		}

		mapper.Map(commentReq, comment);
		commentRepository.Update(comment);
		SaveComment();

		var response = mapper.Map<CommentResponse>(comment);
		return Ok(response);
	}


	[HttpDelete("{id}")]
	[Authorize]
	public ActionResult Delete(int id)
	{
		var comment = commentRepository.FindById(id);

		if(comment == null)
		{
			return NotFound(new { });
		}

		commentRepository.Remove(comment);
		SaveComment();

		return NoContent();
	}


	private void SaveComment()
	{
		unitOfWork.Commit();
	}
}

