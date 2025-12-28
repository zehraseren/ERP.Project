using MediatR;
using TS.Result;
using ERPServer.Domain.Dtos;

namespace ERPServer.Application.Features.Orders.CreateOrder;

public sealed record CreateOrderCommand(
    Guid CustomerId,
    DateOnly Date,
    DateOnly DeliveryDate,
    List<OrderDetailDto> OrderDetails) : IRequest<Result<string>>;
