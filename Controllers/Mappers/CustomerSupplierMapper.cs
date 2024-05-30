using AutoMapper;
using ClientSupplierApi.Dtos;
using ClientSupplierApi.Models;
using ClientSupplierApi.Response;

namespace ClientSupplierApi.Controllers.Mappers
{
    public class CustomerSupplierMapper : Profile
    {
        public CustomerSupplierMapper()
        {
            CreateMap<Customer_supplier, CustomerSupplierDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Contact.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Contact.Phone))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.Address))
                .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Address.Complement))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ReverseMap()
                .ForPath(src => src.Contact.Email, opt => opt.MapFrom(dest => dest.Email))
                .ForPath(src => src.Contact.Phone, opt => opt.MapFrom(dest => dest.Phone))
                .ForPath(src => src.Address.Country, opt => opt.MapFrom(dest => dest.Country))
                .ForPath(src => src.Address.Address, opt => opt.MapFrom(dest => dest.Address))
                .ForPath(src => src.Address.Complement, opt => opt.MapFrom(dest => dest.Complement))
                .ForPath(src => src.Address.City, opt => opt.MapFrom(dest => dest.City))
                .ForPath(src => src.Address.State, opt => opt.MapFrom(dest => dest.State))
                .ForPath(src => src.Address.PostalCode, opt => opt.MapFrom(dest => dest.PostalCode));
            CreateMap<Customer_supplier, CustomerSupplierResponse>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Contact.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Contact.Phone))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address.Address))
                .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Address.Complement))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address.State))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ReverseMap()
                .ForPath(src => src.Contact.Email, opt => opt.MapFrom(dest => dest.Email))
                .ForPath(src => src.Contact.Phone, opt => opt.MapFrom(dest => dest.Phone))
                .ForPath(src => src.Address.Country, opt => opt.MapFrom(dest => dest.Country))
                .ForPath(src => src.Address.Address, opt => opt.MapFrom(dest => dest.Address))
                .ForPath(src => src.Address.Complement, opt => opt.MapFrom(dest => dest.Complement))
                .ForPath(src => src.Address.City, opt => opt.MapFrom(dest => dest.City))
                .ForPath(src => src.Address.State, opt => opt.MapFrom(dest => dest.State))
                .ForPath(src => src.Address.PostalCode, opt => opt.MapFrom(dest => dest.PostalCode));
        }
    }
}
