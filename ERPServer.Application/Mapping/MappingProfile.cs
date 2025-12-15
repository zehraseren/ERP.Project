using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Application.Features.Customers.CreateCustomer;

namespace ERPServer.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCustomerCommand, Customer>();
        }
    }
}
