using MediatR;
using TS.Result;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ERPServer.Application.Features.Products.GetAllProduct;

internal sealed class GetllProductQueryHandler(
    IProductRepository productRepository) : IRequestHandler<GetAllProductQuery, Result<List<Product>>>
{
    public async Task<Result<List<Product>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        List<Product> product = await productRepository.GetAll().OrderBy(p => p.Name).ToListAsync(cancellationToken);

        return product;
    }
}