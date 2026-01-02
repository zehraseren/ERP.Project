using MediatR;
using TS.Result;
using ERPServer.Domain.Dtos;

namespace ERPServer.Application.Features.Invoices.UpdateInvoice;

public sealed record UpdateInvoiceCommand(
    Guid Id,
    Guid CustomerId,
    int TypeValue,
    DateOnly Date,
    string InvoiceNumber,
    List<InvoiceDetailDto> InvoiceDetails) : IRequest<Result<string>>;
