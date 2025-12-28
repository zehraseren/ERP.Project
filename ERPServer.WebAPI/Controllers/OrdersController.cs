using MediatR;
using Microsoft.AspNetCore.Mvc;
using ERPServer.WebAPI.Abstractions;
using ERPServer.Application.Features.Orders.GetAllOrder;
using ERPServer.Application.Features.Orders.CreateOrder;

namespace ERPServer.WebAPI.Controllers;

public class OrdersController : ApiController
{
    public OrdersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}
