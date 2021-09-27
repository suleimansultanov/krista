using AutoMapper;
using KristaShop.WebAPI.Data;
using KristaShop.WebAPI.ViewModels;
using System;

namespace KristaShop.WebAPI.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationVM, Counterparty>()
                .ForMember(dest => dest.id, dest => dest.MapFrom(item => Guid.NewGuid()))
                .ForMember(dest => dest.city_id, dest => dest.MapFrom(item => item.CityId))
                .ForMember(dest => dest.person, dest => dest.MapFrom(item => item.ClientFullName))
                .ForMember(dest => dest.title, dest => dest.MapFrom(item => item.ClientFullName))
                .ForMember(dest => dest.person_email, dest => dest.MapFrom(item => item.Email))
                .ForMember(dest => dest.mall_address, dest => dest.MapFrom(item => item.ShopName))
                .ForMember(dest => dest.person_phone, dest => dest.MapFrom(item => item.PhoneNumber));
        }
    }
}
