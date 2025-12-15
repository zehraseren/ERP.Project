using MediatR;
using TS.Result;
using ERPServer.Domain.Entities;

namespace ERPServer.Application.Features.Customers.GetAllCustomer;

public sealed record GetAllCustomerQuery() : IRequest<Result<List<Customer>>>;
