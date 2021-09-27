using AutoMapper;
using KristaShop.Common.Enums;
using KristaShop.Common.Extensions;
using KristaShop.DataReadOnly.DTOs;
using KristaShop.DataReadOnly.Models;

namespace KristaShop.DataReadOnly.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.IsRoot, dest => dest.MapFrom(item => item.is_root))
                .ForMember(dest => dest.Login, dest => dest.MapFrom(item => item.login))
                .ForMember(dest => dest.Status, dest => dest.MapFrom(item => item.status))
                .ForMember(dest => dest.UserId, dest => dest.MapFrom(item => item.id));
            CreateMap<User, UserClientDTO>()
                .ForMember(dest => dest.ClientFullName, dest => dest.MapFrom(item => item.Counterparty.person))
                .ForMember(dest => dest.PhoneNumber, dest => dest.MapFrom(item => item.Counterparty.person_phone))
                .ForMember(dest => dest.Email, dest => dest.MapFrom(item => item.Counterparty.person_email))
                .ForMember(dest => dest.ShopName, dest => dest.MapFrom(item => item.Counterparty.mall_address))
                .ForMember(dest => dest.CityName, dest => dest.MapFrom(item => item.Counterparty.City.name))
                .ForMember(dest => dest.Login, dest => dest.MapFrom(item => item.login))
                .ForMember(dest => dest.Status, dest => dest.MapFrom(item => item.status))
                .ForMember(dest => dest.StatusName, dest => dest.MapFrom(item => EnumExtensions<UserStatus>.GetDisplayValue((UserStatus)item.status)))
                .ForMember(dest => dest.UserId, dest => dest.MapFrom(item => item.id));
        }
    }
}
