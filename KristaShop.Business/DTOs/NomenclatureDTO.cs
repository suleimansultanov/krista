using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace KristaShop.Business.DTOs
{
    public class NomModelDTO
    {
        public Guid NomId { get; set; }
        public string Articul { get; set; }
        public string ItemName { get; set; }
        public decimal DefaultPrice { get; set; }
        public string VideoUrl { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public List<Guid> Catalogs { get; set; }
        public List<Guid> Categories { get; set; }
        public List<Guid> Clients { get; set; }
        public List<string> PhotoPaths { get; set; } = new List<string>();
        public string MetaTitle { get; set; }
        public string LinkName { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public bool IsVisible { get; set; }
    }

    public class NomUserDTO
    {
        public Guid UserId { get; set; }
        public string ClientFullName { get; set; }
        public string CityName { get; set; }
        public string ClientLogin { get; set; }
        public string MallAddress { get; set; }
        public bool NotVisible { get; set; }
    }
}
