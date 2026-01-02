using MediatR;
using TS.Result;
using ERPServer.Domain.Dtos;

namespace ERPServer.Application.Features.Invoices.CreateInvoice;

public sealed record CreateInvoiceCommand(
    Guid CustomerId,
    int TypeValue,
    DateOnly Date,
    string InvoiceNumber,
    List<InvoiceDetailDto> Details,
    Guid? OrderId) : IRequest<Result<string>>;
