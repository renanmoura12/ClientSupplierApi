using AutoMapper;
using ClientSupplierApi.Dtos;
using ClientSupplierApi.Models;
using ClientSupplierApi.Response;

namespace ClientSupplierApi.Controllers.Mappers
{
    public class UsersMapper : Profile
    {
        public UsersMapper()
        {
            CreateMap<Users, UserDto>().ReverseMap();
            CreateMap<Users, UsersResponse>().ReverseMap();
        }
    }
}
