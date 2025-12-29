using MediatR;
using TS.Result;
using GenericRepository;
using ERPServer.Domain.Dtos;
using ERPServer.Domain.Enums;
using ERPServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ERPServer.Domain.Repositories;

namespace ERPServer.Application.Features.Orders.RequirementsPlanningByOrderId;

internal sealed class RequirementsPlanningByOrderIdCommandHandler(
    IOrderRepository orderRepository,
    IStockMovementRepository stockMovementRepository,
    IRecipeRepository recipeRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<RequirementsPlanningByOrderIdCommand, Result<RequirementsPlanningByOrderIdCommandResponse>>
{
    public async Task<Result<RequirementsPlanningByOrderIdCommandResponse>> Handle(RequirementsPlanningByOrderIdCommand request, CancellationToken cancellationToken)
    {
        // Siparişi detayları ve ürün bilgileriyle birlikte getirme
        Order? order = await orderRepository
            .Where(p => p.Id == request.OrderId)
            .Include(p => p.OrderDetails!)
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(cancellationToken);

        if (order is null) return Result<RequirementsPlanningByOrderIdCommandResponse>.Failure("Sipariş bulunamadı!");

        // Stok yetersiz olduğunda üretilmesi gereken ürünlerin listesi
        List<ProductDto> productionRequiredProducts = new();

        // Reçetelerden türeyen, ihtiyaç planlamasında kullanılacak ürün listesi
        List<ProductDto> planningRequiredProducts = new();

        if (order.OrderDetails is not null)
        {
            // 1. Adım: Siparişte yer alan ürünler için mevcut stok kontrolünün yapılması
            foreach (var orderDetail in order.OrderDetails)
            {
                var product = orderDetail.Product;

                // Ürüne ait tüm stok hareketlerinin alınması
                List<StockMovement> productMovements = await stockMovementRepository
                    .Where(p => p.ProductId == product!.Id)
                    .ToListAsync(cancellationToken);

                // Mevcut stok = girişler - çıkışlar
                decimal currentStock = productMovements.Sum(p => p.NumberOfEntries) - productMovements.Sum(p => p.NumberOfOutputs);

                // Stok yeterli değilse ürün üretim listesine eklenmesi
                if (currentStock < orderDetail.Quantity)
                {
                    productionRequiredProducts.Add(new ProductDto
                    {
                        Id = orderDetail.ProductId,
                        Name = product!.Name,
                        Quantity = orderDetail.Quantity - currentStock
                    });
                }
            }

            // 2. Adım: Üretilmesi gereken ürünler için reçete bazlı ihtiyaçlar hesaplanması
            foreach (var productionRequiredProduct in productionRequiredProducts)
            {
                // Ürüne ait reçete ve reçete detayları getirme
                Recipe? recipe = await recipeRepository
                    .Where(p => p.ProductId == productionRequiredProduct.Id)
                    .Include(p => p.Details!)
                    .ThenInclude(p => p.Product)
                    .FirstOrDefaultAsync(cancellationToken);

                if (recipe is not null && recipe.Details is not null)
                {
                    foreach (var recipeDetail in recipe.Details)
                    {
                        // İlgili hammaddenin stok hareketleri alınması
                        List<StockMovement> materialMovements = await stockMovementRepository
                            .Where(p => p.ProductId == recipeDetail.ProductId)
                            .ToListAsync(cancellationToken);

                        // Hammadde için mevcut stok miktarı hesaplanması
                        decimal materialStock = materialMovements.Sum(p => p.NumberOfEntries) - materialMovements.Sum(p => p.NumberOfOutputs);

                        decimal requiredMaterialQuantity = recipeDetail.Quantity * productionRequiredProduct.Quantity;

                        // Reçetede yer alan alt ürün / hammadde için stok yetersizliği kontrolü
                        if (materialStock < recipeDetail.Quantity)
                        {
                            planningRequiredProducts.Add(new ProductDto
                            {
                                Id = recipeDetail.ProductId,
                                Name = recipeDetail.Product!.Name,
                                Quantity = requiredMaterialQuantity - materialStock
                            });
                        }
                    }
                }
            }

        }

        // 3. Adım: Aynı ürünler gruplanarak toplam ihtiyaç miktarlarının hesaplanması
        planningRequiredProducts = planningRequiredProducts
            .GroupBy(p => p.Id)
            .Select(g => new ProductDto
            {
                Id = g.Key,
                Name = g.First().Name,
                Quantity = g.Sum(item => item.Quantity)
            }).ToList();

        // İhtiyaç planlaması tamamlandığı için sipariş durumu güncelleme
        order.Status = OrderStatusEnum.RequirementsPlanWorked;
        orderRepository.Update(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RequirementsPlanningByOrderIdCommandResponse(
            DateOnly.FromDateTime(DateTime.Now),
            $"{order.Number} Nolu Siparişin İhtiyaç Planlaması",
            planningRequiredProducts);
    }
}