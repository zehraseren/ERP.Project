using MediatR;
using TS.Result;
using ERPServer.Domain.Dtos;

namespace ERPServer.Application.Features.Recipes.CreateRecipe;

public sealed record CreateRecipeCommand(
    Guid ProductId,
    List<RecipeDetailDto> Details) : IRequest<Result<string>>;
