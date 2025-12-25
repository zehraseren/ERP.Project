using MediatR;
using TS.Result;
using ERPServer.Domain.Entities;

namespace ERPServer.Application.Features.RecipeDetails.GetRecipeByIdWithDetails;

public sealed record GetRecipeByIdWithDetailsQuery(Guid RecipeId) : IRequest<Result<Recipe>>;
