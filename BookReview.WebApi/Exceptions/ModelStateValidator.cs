using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.WebApi.Exeptions;


public class ModleStateValidator
{
    public static IActionResult GenerateErrorResponse(ActionContext context)
    {
        var result = new {
            Status = (int) HttpStatusCode.BadRequest,
            StatusPhase = "Bad Request - Invalid Parameters or Request Body",
            Errors = Enumerable.Empty<string>().ToList()
        };

        foreach(var state in context.ModelState.AsEnumerable())
        {
            foreach(var error in state.Value!.Errors)
            {
                result.Errors.Add(error.ErrorMessage);
            }
        }

        return new BadRequestObjectResult(result);
    }
}