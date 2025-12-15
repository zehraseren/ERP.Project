using MediatR;
using TS.Result;
using GenericRepository;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;

namespace ERPServer.Application.Features.Customers.DeleteCustomerById;

public sealed class DeleteCustomerByIdDeleteCustomerByIdCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteCustomerByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteCustomerByIdCommand request, CancellationToken cancellationToken)
    {
        Customer customer = await customerRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);

        if (customer is null)
        {
            return Result<string>.Failure("Müşteri bulunamadı!");
        }

        customerRepository.Delete(customer);
        await unitOfWork.SaveChangesAsync();

        return "Müşteri başarıyla silindi.";
    }
}
