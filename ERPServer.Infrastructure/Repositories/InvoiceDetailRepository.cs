using GenericRepository;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using ERPServer.Infrastructure.Context;

namespace ERPServer.Infrastructure.Repositories;

internal sealed class InvoiceDetailRepository : Repository<InvoiceDetail, ApplicationDbContext>, IInvoiceDetailRepository
{
    public InvoiceDetailRepository(ApplicationDbContext context) : base(context)
    {
    }
}
