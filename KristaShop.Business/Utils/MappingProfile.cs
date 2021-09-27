using AutoMapper;
using KristaShop.Business.DTOs;
using KristaShop.Common.Enums;
using KristaShop.Common.Extensions;
using KristaShop.DataAccess.Entities;
using Newtonsoft.Json;
using System;

namespace KristaShop.Business.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MenuItem, MenuItemDTO>()
                .ForMember(dest => dest.Id, dest => dest.MapFrom(item => item.id))
                .ForMember(dest => dest.MenuType, dest => dest.MapFrom(item => item.menu_type))
                .ForMember(dest => dest.ControllerName, dest => dest.MapFrom(item => item.controller_name))
                .ForMember(dest => dest.ActionName, dest => dest.MapFrom(item => item.action_name))
                .ForMember(dest => dest.Title, dest => dest.MapFrom(item => item.title))
                .ForMember(dest => dest.URL, dest => dest.MapFrom(item => item.url))
                .ForMember(dest => dest.Params, dest => dest.MapFrom(item => item.parameters))
                .ForMember(dest => dest.Icon, dest => dest.MapFrom(item => item.icon))
                .ForMember(dest => dest.Order, dest => dest.MapFrom(item => item.order));
            CreateMap<MenuItemDTO, MenuItem>()
                .ForMember(dest => dest.id, dest => dest.MapFrom(item => item.Id))
                .ForMember(dest => dest.menu_type, dest => dest.MapFrom(item => item.MenuType))
                .ForMember(dest => dest.controller_name, dest => dest.MapFrom(item => item.ControllerName))
                .ForMember(dest => dest.action_name, dest => dest.MapFrom(item => item.ActionName))
                .ForMember(dest => dest.title, dest => dest.MapFrom(item => item.Title))
                .ForMember(dest => dest.url, dest => dest.MapFrom(item => item.URL))
                .ForMember(dest => dest.parameters, dest => dest.MapFrom(item => item.Params))
                .ForMember(dest => dest.icon, dest => dest.MapFrom(item => item.Icon))
                .ForMember(dest => dest.order, dest => dest.MapFrom(item => item.Order));


            CreateMap<MenuContent, MenuContentDTO>()
                .ForMember(dest => dest.Id, dest => dest.MapFrom(item => item.id))
                .ForMember(dest => dest.URL, dest => dest.MapFrom(item => item.url))
                .ForMember(dest => dest.Title, dest => dest.MapFrom(item => item.title))
                .ForMember(dest => dest.Body, dest => dest.MapFrom(item => item.body))
                .ForMember(dest => dest.Layout, dest => dest.MapFrom(item => item.layout))
                .ForMember(dest => dest.MetaTitle, dest => dest.MapFrom(item => item.meta_title))
                .ForMember(dest => dest.MetaKeywords, dest => dest.MapFrom(item => item.meta_keywords))
                .ForMember(dest => dest.MetaDescription, dest => dest.MapFrom(item => item.meta_description));
            CreateMap<MenuContentDTO, MenuContent>()
                .ForMember(dest => dest.id, dest => dest.MapFrom(item => item.Id))
                .ForMember(dest => dest.url, dest => dest.MapFrom(item => item.URL))
                .ForMember(dest => dest.title, dest => dest.MapFrom(item => item.Title))
                .ForMember(dest => dest.body, dest => dest.MapFrom(item => item.Body))
                .ForMember(dest => dest.layout, dest => dest.MapFrom(item => item.Layout))
                .ForMember(dest => dest.meta_title, dest => dest.MapFrom(item => item.MetaTitle))
                .ForMember(dest => dest.meta_keywords, dest => dest.MapFrom(item => item.MetaKeywords))
                .ForMember(dest => dest.meta_description, dest => dest.MapFrom(item => item.MetaDescription));


            CreateMap<Setting, SettingDTO>()
                .ForMember(dest => dest.Id, dest => dest.MapFrom(item => item.id))
                .ForMember(dest => dest.Key, dest => dest.MapFrom(item => item.key))
                .ForMember(dest => dest.Value, dest => dest.MapFrom(item => item.value));
            CreateMap<SettingDTO, Setting>()
                .ForMember(dest => dest.id, dest => dest.MapFrom(item => item.Id))
                .ForMember(dest => dest.key, dest => dest.MapFrom(item => item.Key))
                .ForMember(dest => dest.value, dest => dest.MapFrom(item => item.Value));


            CreateMap<UrlAccess, UrlAccessDTO>()
                .ForMember(dest => dest.Id, dest => dest.MapFrom(item => item.id))
                .ForMember(dest => dest.URL, dest => dest.MapFrom(item => item.url))
                .ForMember(dest => dest.Description, dest => dest.MapFrom(item => item.description))
                .ForMember(dest => dest.Acl, dest => dest.MapFrom(item => item.acl))
                .ForMember(dest => dest.AccessGroupsJson, dest => dest.MapFrom(item => JsonConvert.DeserializeObject(item.access_groups_json)))
                .ForMember(dest => dest.DeniedGroupsJson, dest => dest.MapFrom(item => JsonConvert.DeserializeObject(item.denied_groups_json)));
            CreateMap<UrlAccessDTO, UrlAccess>()
                .ForMember(dest => dest.id, dest => dest.MapFrom(item => item.Id))
                .ForMember(dest => dest.url, dest => dest.MapFrom(item => item.URL))
                .ForMember(dest => dest.description, dest => dest.MapFrom(item => item.Description))
                .ForMember(dest => dest.acl, dest => dest.MapFrom(item => item.Acl))
                .ForMember(dest => dest.access_groups_json, dest => dest.MapFrom(item => JsonConvert.SerializeObject(item.AccessGroupsJson)))
                .ForMember(dest => dest.denied_groups_json, dest => dest.MapFrom(item => JsonConvert.SerializeObject(item.DeniedGroupsJson)));


            CreateMap<Category, CategoryDTO>()
                .ForMember(dest => dest.Id, dest => dest.MapFrom(item => item.id))
                .ForMember(dest => dest.Name, dest => dest.MapFrom(item => item.name))
                .ForMember(dest => dest.IsVisible, dest => dest.MapFrom(item => item.is_visible));
            CreateMap<CategoryDTO, Category>()
                .ForMember(dest => dest.id, dest => dest.MapFrom(item => item.Id))
                .ForMember(dest => dest.name, dest => dest.MapFrom(item => item.Name))
                .ForMember(dest => dest.is_visible, dest => dest.MapFrom(item => item.IsVisible));


            CreateMap<Catalog, CatalogDTO>()
                .ForMember(dest => dest.Id, dest => dest.MapFrom(item => item.id))
                .ForMember(dest => dest.Name, dest => dest.MapFrom(item => item.name))
                .ForMember(dest => dest.Uri, dest => dest.MapFrom(item => item.uri))
                .ForMember(dest => dest.Description, dest => dest.MapFrom(item => item.description))
                .ForMember(dest => dest.OrderForm, dest => dest.MapFrom(item => item.order_form))
                .ForMember(dest => dest.OrderFormName, dest => dest.MapFrom(item => EnumExtensions<OrderFormType>.GetDisplayValue((OrderFormType)item.order_form)))
                .ForMember(dest => dest.MetaTitle, dest => dest.MapFrom(item => item.meta_title))
                .ForMember(dest => dest.MetaKeywords, dest => dest.MapFrom(item => item.meta_keywords))
                .ForMember(dest => dest.MetaDescription, dest => dest.MapFrom(item => item.meta_description))
                .ForMember(dest => dest.Order, dest => dest.MapFrom(item => item.order))
                .ForMember(dest => dest.IsDiscount, dest => dest.MapFrom(item => item.is_discount))
                .ForMember(dest => dest.IsVisible, dest => dest.MapFrom(item => item.is_visible));
            CreateMap<CatalogDTO, Catalog>()
                .ForMember(dest => dest.id, dest => dest.MapFrom(item => item.Id))
                .ForMember(dest => dest.name, dest => dest.MapFrom(item => item.Name))
                .ForMember(dest => dest.uri, dest => dest.MapFrom(item => item.Uri))
                .ForMember(dest => dest.description, dest => dest.MapFrom(item => item.Description))
                .ForMember(dest => dest.order_form, dest => dest.MapFrom(item => item.OrderForm))
                .ForMember(dest => dest.meta_title, dest => dest.MapFrom(item => item.MetaTitle))
                .ForMember(dest => dest.meta_keywords, dest => dest.MapFrom(item => item.MetaKeywords))
                .ForMember(dest => dest.meta_description, dest => dest.MapFrom(item => item.MetaDescription))
                .ForMember(dest => dest.order, dest => dest.MapFrom(item => item.Order))
                .ForMember(dest => dest.is_discount, dest => dest.MapFrom(item => item.IsDiscount))
                .ForMember(dest => dest.is_visible, dest => dest.MapFrom(item => item.IsVisible));


            CreateMap<Feedback, FeedbackDTO>()
                .ForMember(dest => dest.Id, dest => dest.MapFrom(item => item.id))
                .ForMember(dest => dest.Person, dest => dest.MapFrom(item => item.person))
                .ForMember(dest => dest.Phone, dest => dest.MapFrom(item => item.phone))
                .ForMember(dest => dest.Message, dest => dest.MapFrom(item => item.message))
                .ForMember(dest => dest.Email, dest => dest.MapFrom(item => item.email))
                .ForMember(dest => dest.Viewed, dest => dest.MapFrom(item => item.viewed))
                .ForMember(dest => dest.RecordTimeStamp, dest => dest.MapFrom(item => item.record_time_stamp));
            CreateMap<FeedbackDTO, Feedback>()
                .ForMember(dest => dest.id, dest => dest.MapFrom(item => Guid.NewGuid()))
                .ForMember(dest => dest.person, dest => dest.MapFrom(item => item.Person))
                .ForMember(dest => dest.phone, dest => dest.MapFrom(item => item.Phone))
                .ForMember(dest => dest.message, dest => dest.MapFrom(item => item.Message))
                .ForMember(dest => dest.email, dest => dest.MapFrom(item => item.Email))
                .ForMember(dest => dest.viewed, dest => dest.MapFrom(item => false))
                .ForMember(dest => dest.record_time_stamp, dest => dest.MapFrom(item => DateTime.Now));


            CreateMap<Nomenclature, NomModelDTO>()
                .ForMember(dest => dest.NomId, dest => dest.MapFrom(item => item.id))
                .ForMember(dest => dest.Articul, dest => dest.MapFrom(item => item.articul))
                .ForMember(dest => dest.ItemName, dest => dest.MapFrom(item => item.name))
                .ForMember(dest => dest.DefaultPrice, dest => dest.MapFrom(item => item.default_price))
                .ForMember(dest => dest.VideoUrl, dest => dest.MapFrom(item => item.youtube_link))
                .ForMember(dest => dest.Description, dest => dest.MapFrom(item => item.description))
                .ForMember(dest => dest.ImagePath, dest => dest.MapFrom(item => item.image_path))
                .ForMember(dest => dest.MetaTitle, dest => dest.MapFrom(item => item.meta_title))
                .ForMember(dest => dest.LinkName, dest => dest.MapFrom(item => item.link_name))
                .ForMember(dest => dest.MetaKeywords, dest => dest.MapFrom(item => item.meta_keywords))
                .ForMember(dest => dest.MetaDescription, dest => dest.MapFrom(item => item.meta_description))
                .ForMember(dest => dest.IsVisible, dest => dest.MapFrom(item => item.is_visible));
            CreateMap<NomModelDTO, Nomenclature>()
                .ForMember(dest => dest.id, dest => dest.MapFrom(item => item.NomId))
                .ForMember(dest => dest.articul, dest => dest.MapFrom(item => item.Articul))
                .ForMember(dest => dest.name, dest => dest.MapFrom(item => item.ItemName))
                .ForMember(dest => dest.default_price, dest => dest.MapFrom(item => item.DefaultPrice))
                .ForMember(dest => dest.youtube_link, dest => dest.MapFrom(item => item.VideoUrl))
                .ForMember(dest => dest.description, dest => dest.MapFrom(item => item.Description))
                .ForMember(dest => dest.image_path, dest => dest.MapFrom(item => item.ImagePath))
                .ForMember(dest => dest.meta_title, dest => dest.MapFrom(item => item.MetaTitle))
                .ForMember(dest => dest.link_name, dest => dest.MapFrom(item => item.LinkName))
                .ForMember(dest => dest.meta_keywords, dest => dest.MapFrom(item => item.MetaKeywords))
                .ForMember(dest => dest.meta_description, dest => dest.MapFrom(item => item.MetaDescription))
                .ForMember(dest => dest.is_visible, dest => dest.MapFrom(item => item.IsVisible));
            

            CreateMap<CatalogDiscount, DiscountDTO>()
                .ForMember(dest => dest.Id, dest => dest.MapFrom(item => item.id))
                .ForMember(dest => dest.DiscountId, dest => dest.MapFrom(item => item.catalog_id))
                .ForMember(dest => dest.DiscountPrice, dest => dest.MapFrom(item => item.discount_price))
                .ForMember(dest => dest.IsActive, dest => dest.MapFrom(item => item.is_active))
                .ForMember(dest => dest.StartDate, dest => dest.MapFrom(item => item.start_date))
                .ForMember(dest => dest.EndDate, dest => dest.MapFrom(item => item.end_date));
            CreateMap<DiscountDTO, CatalogDiscount>()
                .ForMember(dest => dest.id, dest => dest.MapFrom(item => item.Id))
                .ForMember(dest => dest.catalog_id, dest => dest.MapFrom(item => item.DiscountId))
                .ForMember(dest => dest.discount_price, dest => dest.MapFrom(item => item.DiscountPrice))
                .ForMember(dest => dest.is_active, dest => dest.MapFrom(item => item.IsActive))
                .ForMember(dest => dest.start_date, dest => dest.MapFrom(item => item.StartDate))
                .ForMember(dest => dest.end_date, dest => dest.MapFrom(item => item.EndDate));

            CreateMap<NomDiscount, DiscountDTO>()
                .ForMember(dest => dest.Id, dest => dest.MapFrom(item => item.id))
                .ForMember(dest => dest.DiscountId, dest => dest.MapFrom(item => item.nom_id))
                .ForMember(dest => dest.DiscountPrice, dest => dest.MapFrom(item => item.discount_price))
                .ForMember(dest => dest.IsActive, dest => dest.MapFrom(item => item.is_active))
                .ForMember(dest => dest.StartDate, dest => dest.MapFrom(item => item.start_date))
                .ForMember(dest => dest.EndDate, dest => dest.MapFrom(item => item.end_date));
            CreateMap<DiscountDTO, NomDiscount>()
                .ForMember(dest => dest.id, dest => dest.MapFrom(item => item.Id))
                .ForMember(dest => dest.nom_id, dest => dest.MapFrom(item => item.DiscountId))
                .ForMember(dest => dest.discount_price, dest => dest.MapFrom(item => item.DiscountPrice))
                .ForMember(dest => dest.is_active, dest => dest.MapFrom(item => item.IsActive))
                .ForMember(dest => dest.start_date, dest => dest.MapFrom(item => item.StartDate))
                .ForMember(dest => dest.end_date, dest => dest.MapFrom(item => item.EndDate));

            CreateMap<UserDiscount, DiscountDTO>()
                .ForMember(dest => dest.Id, dest => dest.MapFrom(item => item.id))
                .ForMember(dest => dest.DiscountId, dest => dest.MapFrom(item => item.user_id))
                .ForMember(dest => dest.DiscountPrice, dest => dest.MapFrom(item => item.discount_price))
                .ForMember(dest => dest.IsActive, dest => dest.MapFrom(item => item.is_active))
                .ForMember(dest => dest.StartDate, dest => dest.MapFrom(item => item.start_date))
                .ForMember(dest => dest.EndDate, dest => dest.MapFrom(item => item.end_date));
            CreateMap<DiscountDTO, UserDiscount>()
                .ForMember(dest => dest.id, dest => dest.MapFrom(item => item.Id))
                .ForMember(dest => dest.user_id, dest => dest.MapFrom(item => item.DiscountId))
                .ForMember(dest => dest.discount_price, dest => dest.MapFrom(item => item.DiscountPrice))
                .ForMember(dest => dest.is_active, dest => dest.MapFrom(item => item.IsActive))
                .ForMember(dest => dest.start_date, dest => dest.MapFrom(item => item.StartDate))
                .ForMember(dest => dest.end_date, dest => dest.MapFrom(item => item.EndDate));
        }
    }
}
