
namespace BookReview.WebApi.Dtos;

public record FilterQuery(int currentPage = 1, int pageSize = 10, string? orderBy = null, string? sort = null);