using MediatR;
using Microsoft.AspNetCore.Mvc;
using ERPServer.WebAPI.Abstractions;
using ERPServer.Application.Features.Productions.GetAllProduction;
using ERPServer.Application.Features.Productions.CreateProduction;
using ERPServer.Application.Features.Productions.DeleteProductionById;

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

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductionCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteById(DeleteProductionByIdCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}
