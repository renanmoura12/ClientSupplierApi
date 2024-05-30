using AutoMapper;
using ClientSupplierApi.Dtos;
using ClientSupplierApi.Models;

namespace ClientSupplierApi.Controllers.Mappers
{
    public class CustomerSupplierMapper : Profile
    {
        public CustomerSupplierMapper()
        {
            CreateMap<Customer_supplier, CustomerSupplierDto>().ReverseMap();
            CreateMap<Customer_supplier_address, CustomerSupplierDto>().ReverseMap();
            CreateMap<Customer_supplier_contact, CustomerSupplierDto>().ReverseMap();
        }
    }
}
