using MediatR;
using TS.Result;
using AutoMapper;
using GenericRepository;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;

namespace ERPServer.Application.Features.RecipeDetails.CreateRecipeDetail;

internal sealed class CreateRecipeDetailCommandHandler(
    IRecipeDetailRepository recipeDetailRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateRecipeDetailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateRecipeDetailCommand request, CancellationToken cancellationToken)
    {
        RecipeDetail? recipeDetail = await recipeDetailRepository.GetByExpressionWithTrackingAsync(
            p => p.RecipeId == request.RecipeId &&
            p.ProductId == request.ProductId, cancellationToken);

        if (recipeDetail is not null)
        {
            recipeDetail.Quantity += request.Quantity;
        }
        else
        {
            recipeDetail = mapper.Map<RecipeDetail>(request);
            await recipeDetailRepository.AddAsync(recipeDetail, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Reçete ürün kaydı başarıyla tamamlandı.";
    }
}