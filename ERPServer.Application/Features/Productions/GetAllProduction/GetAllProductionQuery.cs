using MediatR;
using TS.Result;
using ERPServer.Domain.Entities;

namespace ERPServer.Application.Features.Productions.GetAllProduction;

public sealed record GetAllProductionQuery() : IRequest<Result<List<Production>>>;
