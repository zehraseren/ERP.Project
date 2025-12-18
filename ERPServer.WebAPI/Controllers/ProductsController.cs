using MediatR;
using Microsoft.AspNetCore.Mvc;
using ERPServer.WebAPI.Abstractions;
using ERPServer.Application.Features.Products.GetAllProduct;

namespace ERPServer.WebAPI.Controllers;

public class ProductsController : ApiController
{
    public ProductsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}
