using MediatR;
using TS.Result;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ERPServer.Application.Features.RecipeDetails.GetRecipeByIdWithDetails;

internal sealed class GetRecipeByIdWithDetailsQueryHandler(
    IRecipeRepository recipeRepository) : IRequestHandler<GetRecipeByIdWithDetailsQuery, Result<Recipe>>
{
    public async Task<Result<Recipe>> Handle(GetRecipeByIdWithDetailsQuery request, CancellationToken cancellationToken)
    {
        Recipe? recipe = await recipeRepository
            .Where(p => p.Id == request.Id)
            .Include(p => p.Product)
            .Include(p => p.Details!.OrderBy(p => p.Product!.Name))
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync();

        if (recipe is null) return Result<Recipe>.Failure("Ürüne aile reçete bulunamadı!");

        return recipe;
    }
}