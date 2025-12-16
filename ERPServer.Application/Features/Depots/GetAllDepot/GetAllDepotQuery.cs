using MediatR;
using TS.Result;
using ERPServer.Domain.Entities;

namespace ERPServer.Application.Features.Depots.GetAllDepot;

public sealed record GetAllDepotQuery() : IRequest<Result<List<Depot>>>;
