using MediatR;
using TS.Result;
using ERPServer.Domain.Entities;

namespace ERPServer.Application.Features.Orders.GetAllOrder;

public sealed record GetAllOrderQuery() :
    IRequest<Result<List<Order>>>;
