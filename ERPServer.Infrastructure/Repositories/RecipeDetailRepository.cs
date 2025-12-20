using GenericRepository;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using ERPServer.Infrastructure.Context;

namespace ERPServer.Infrastructure.Repositories;

internal sealed class RecipeDetailRepository : Repository<RecipeDetail, ApplicationDbContext>, IRecipeDetailRepository
{
    public RecipeDetailRepository(ApplicationDbContext context) : base(context)
    {
    }
}