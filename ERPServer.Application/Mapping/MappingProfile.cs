using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Application.Features.Depots.CreateDepot;
using ERPServer.Application.Features.Customers.CreateCustomer;
using ERPServer.Application.Features.Customers.UpdateCustomer;

namespace ERPServer.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCustomerCommand, Customer>();
            CreateMap<UpdateCustomerCommand, Customer>();

            CreateMap<CreateDepotCommand, Depot>();
        }
    }
}
