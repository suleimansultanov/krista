using System;

namespace KristaShop.Business.DTOs
{
    public class CatalogDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int OrderForm { get; set; }
        public int NomCount { get; set; }
        public string OrderFormName { get; set; }
        public string Uri { get; set; }
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public int Order { get; set; }
        public bool IsDiscount { get; set; }
        public bool IsVisible { get; set; }
    }
}
