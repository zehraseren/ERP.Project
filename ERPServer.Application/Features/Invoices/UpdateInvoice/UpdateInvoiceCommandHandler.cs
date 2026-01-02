using MediatR;
using TS.Result;
using AutoMapper;
using GenericRepository;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ERPServer.Application.Features.Invoices.UpdateInvoice;

internal sealed class UpdateInvoiceCommandHandler(
    IInvoiceRepository invoiceRepository,
    IInvoiceDetailRepository invoiceDetailRepository,
    IStockMovementRepository stockMovementRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateInvoiceCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        Invoice? invoice = await invoiceRepository
            .WhereWithTracking(p => p.Id == request.Id).
            Include(p => p.InvoiceDetails)
            .FirstOrDefaultAsync(cancellationToken);

        if (invoice is null) return Result<string>.Failure("Fatura bulunamadı!");

        List<StockMovement> movements = await stockMovementRepository
            .Where(p => p.InvoiceId == invoice.Id)
            .ToListAsync(cancellationToken);

        stockMovementRepository.DeleteRange(movements);
        invoiceDetailRepository.DeleteRange(invoice.InvoiceDetails);

        invoice.InvoiceDetails = request.InvoiceDetails.Select(s => new InvoiceDetail
        {
            InvoiceId = invoice.Id,
            DepotId = s.DepotId,
            ProductId = s.ProductId,
            Price = s.Price,
            Quantity = s.Quantity,
        }).ToList();

        await invoiceDetailRepository.AddRangeAsync(invoice.InvoiceDetails, cancellationToken);
        mapper.Map(request, invoice);

        List<StockMovement> newMovements = new();
        foreach (var item in request.InvoiceDetails)
        {
            StockMovement movement = new()
            {
                InvoiceId = invoice.Id,
                NumberOfEntries = request.TypeValue == 1 ? item.Quantity : 0,
                NumberOfOutputs = request.TypeValue == 2 ? item.Quantity : 0,
                DepotId = item.DepotId,
                Price = item.Price,
                ProductId = item.ProductId
            };

            newMovements.Add(movement);
        }

        await stockMovementRepository.AddRangeAsync(newMovements, cancellationToken);

        mapper.Map(request, invoice);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Fatura başarıyla güncellendi";
    }
}