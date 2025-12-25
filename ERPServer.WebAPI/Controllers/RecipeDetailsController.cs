using MediatR;
using Microsoft.AspNetCore.Mvc;
using ERPServer.WebAPI.Abstractions;
using ERPServer.Application.Features.RecipeDetails.CreateRecipeDetail;
using ERPServer.Application.Features.RecipeDetails.GetRecipeByIdWithDetails;

namespace ERPServer.WebAPI.Controllers;

public class RecipeDetailsController : ApiController
{
    public RecipeDetailsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetRecipeByIdWithDetails(GetRecipeByIdWithDetailsQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRecipeDetailCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}
