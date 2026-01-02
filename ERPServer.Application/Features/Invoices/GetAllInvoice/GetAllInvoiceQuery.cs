using MediatR;
using TS.Result;
using ERPServer.Domain.Entities;

namespace ERPServer.Application.Features.Invoices.GetAllInvoice;

public sealed record GetAllInvoiceQuery(int Type) : IRequest<Result<List<Invoice>>>;
