using MediatR;
using Microsoft.AspNetCore.Mvc;
using ERPServer.WebAPI.Abstractions;
using ERPServer.Application.Features.Productions.GetAllProduction;

namespace ERPServer.WebAPI.Controllers;

public class ProductionsController : ApiController
{
    public ProductionsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllProductionQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}
