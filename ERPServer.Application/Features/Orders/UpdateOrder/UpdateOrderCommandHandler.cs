using MediatR;
using TS.Result;
using AutoMapper;
using GenericRepository;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ERPServer.Application.Features.Orders.UpdateOrder;

internal sealed class UpdateOrderCommandHandler(
    IOrderRepository orderRepository,
    IOrderDetailRepository orderDetailRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateOrderCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        Order? order = await orderRepository
            .Where(p => p.Id == request.Id)
            .Include(p => p.OrderDetails)
            .FirstOrDefaultAsync();

        if (order is null) return Result<string>.Failure("Sipariş bulunamadı!");

        orderDetailRepository.DeleteRange(order.OrderDetails);
        List<OrderDetail> newDetails = request.OrderDetails.Select(s => new OrderDetail
        {
            OrderId = order.Id,
            Price = s.Price,
            ProductId = s.ProductId,
            Quantity = s.Quantity,
        }).ToList();

        await orderDetailRepository.AddRangeAsync(newDetails, cancellationToken);
        mapper.Map(request, order);
        orderRepository.Update(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Sipariş başarıyla güncellendi.";
    }
}