using GenericRepository;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using ERPServer.Infrastructure.Context;

namespace ERPServer.Infrastructure.Repositories;

internal class OrderDetailRepository : Repository<OrderDetail, ApplicationDbContext>, IOrderDetailRepository
{
    public OrderDetailRepository(ApplicationDbContext context) : base(context)
    {
    }
}
