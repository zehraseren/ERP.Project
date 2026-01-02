using MediatR;
using TS.Result;
using GenericRepository;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ERPServer.Application.Features.Invoices.DeleteInvoiceById;

internal sealed class DeleteInvoiceByIdCommandHandler(
    IInvoiceRepository invoiceRepository,
    IStockMovementRepository stockMovementRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteInvoiceByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteInvoiceByIdCommand request, CancellationToken cancellationToken)
    {
        Invoice? invoice = await invoiceRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);

        if (invoice is null) return Result<string>.Failure("Fatura bulunamadı!");

        List<StockMovement> movements = await stockMovementRepository
            .Where(p => p.InvoiceId == request.Id)
            .ToListAsync(cancellationToken);

        stockMovementRepository.DeleteRange(movements);

        invoiceRepository.Delete(invoice);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Fatura başarıyla silindi.";
    }
}