using AutoMapper;
using KristaShop.Business.DTOs;
using KristaShop.DataReadOnly.DTOs;
using KristaShop.WebUI.Models;

namespace KristaShop.WebUI.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SettingCreateViewModel, SettingDTO>();
            CreateMap<SettingEditViewModel, SettingDTO>()
                .ForMember(dest => dest.Id, dest => dest.MapFrom(item => item.IdEdit))
                .ForMember(dest => dest.Key, dest => dest.MapFrom(item => item.KeyEdit))
                .ForMember(dest => dest.Value, dest => dest.MapFrom(item => item.ValueEdit));


            CreateMap<MContentViewModel, MenuContentDTO>();
            CreateMap<MenuContentDTO, MContentViewModel>();
            
            CreateMap<MenuItemViewModel, MenuItemDTO>();
            CreateMap<MenuItemDTO, MenuItemViewModel>();
            
            CreateMap<UrlAccessViewModel, UrlAccessDTO>();
            CreateMap<UrlAccessDTO, UrlAccessViewModel>();

            CreateMap<CategoryViewModel, CategoryDTO>();
            CreateMap<CategoryDTO, CategoryViewModel>();

            CreateMap<CatalogViewModel, CatalogDTO>();
            CreateMap<CatalogDTO, CatalogViewModel>();

            CreateMap<CModelDTO, CModelViewModel>();

            CreateMap<NomViewModel, NomModelDTO>();
            CreateMap<NomModelDTO, NomViewModel>();

            CreateMap<DiscountViewModel, DiscountDTO>();
            CreateMap<DiscountDTO, DiscountViewModel>();
        }
    }
}
