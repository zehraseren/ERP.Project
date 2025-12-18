using MediatR;
using TS.Result;
using ERPServer.Domain.Entities;

namespace ERPServer.Application.Features.Products.GetAllProduct;

public sealed record GetAllProductQuery : IRequest<Result<List<Product>>>;
