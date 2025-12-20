using MediatR;
using TS.Result;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ERPServer.Application.Features.Recipes.GetAllRecipe;

internal sealed record GetAllRecipeQueryHandler(
    IRecipeRepository recipeRepository) : IRequestHandler<GetAllRecipeQuery, Result<List<Recipe>>>
{
    public async Task<Result<List<Recipe>>> Handle(GetAllRecipeQuery request, CancellationToken cancellationToken)
    {
        List<Recipe> recipes = await recipeRepository
            .GetAll()
            .Include(p => p.Product)
            .OrderBy(p => p.Product!.Name)
            .ToListAsync();

        return recipes;
    }
}