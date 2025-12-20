using MediatR;
using TS.Result;
using GenericRepository;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;

namespace ERPServer.Application.Features.Recipes.CreateRecipe;

internal sealed class CreateRecipeCommandHandler(
    IRecipeRepository recipeRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateRecipeCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        bool isRecipeExists = await recipeRepository.AnyAsync(r => r.ProductId == request.ProductId, cancellationToken);

        if (isRecipeExists)
        {
            return Result<string>.Failure("Bu ürüne ait reçete daha önce oluşturulmuştur!");
        }

        Recipe recipe = new()
        {
            ProductId = request.ProductId,
            RecipeDetails = request.Details.Select(s => new RecipeDetail()
            {
                ProductId = s.ProductId,
                Quantity = s.Quantity
            }).ToList()
        };

        await recipeRepository.AddAsync(recipe, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Reçete kaydı başarıyla oluşturuldu.";
    }
}
