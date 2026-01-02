using MediatR;
using TS.Result;
using AutoMapper;
using GenericRepository;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;

namespace ERPServer.Application.Features.Invoices.CreateInvoice;

internal sealed class CreateInvoiceCommandHandler(
    IInvoiceRepository invoiceRepository,
    IStockMovementRepository stockMovementRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateInvoiceCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        Invoice? invoice = mapper.Map<Invoice>(request);

        if (invoice.InvoiceDetails is not null)
        {
            List<StockMovement> movements = new();

            foreach (var item in invoice.InvoiceDetails)
            {
                StockMovement movement = new()
                {
                    InvoiceId = invoice.Id,
                    NumberOfEntries = request.TypeValue == 1 ? item.Quantity : 0,
                    NumberOfOutputs = request.TypeValue == 2 ? item.Quantity : 0,
                    DepotId = item.DepotId,
                    Price = item.Price,
                    ProductId = item.ProductId,
                };

                movements.Add(movement);
            }

            await stockMovementRepository.AddRangeAsync(movements, cancellationToken);
        }

        await invoiceRepository.AddAsync(invoice);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Fatura başarıyla oluşturuldu.";
    }
}