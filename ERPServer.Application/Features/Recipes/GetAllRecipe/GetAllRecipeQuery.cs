using MediatR;
using TS.Result;
using ERPServer.Domain.Entities;

namespace ERPServer.Application.Features.Recipes.GetAllRecipe;

public sealed class GetAllRecipeQuery() : IRequest<Result<List<Recipe>>>;