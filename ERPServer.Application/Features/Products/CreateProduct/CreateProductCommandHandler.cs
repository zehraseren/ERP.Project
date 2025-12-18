using MediatR;
using TS.Result;
using AutoMapper;
using GenericRepository;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;

namespace ERPServer.Application.Features.Products.CreateProduct;

internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool isProductExists = await productRepository.AnyAsync(p => p.Name == request.Name, cancellationToken);

        if (isProductExists)
        {
            return Result<string>.Failure("Ürün adı daha önce kullanılmış.");
        }

        Product product = mapper.Map<Product>(request);
        await productRepository.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Ürün başarıyla oluşturuldu.";
    }
}