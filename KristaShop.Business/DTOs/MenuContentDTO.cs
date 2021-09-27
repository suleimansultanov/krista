using System;

namespace KristaShop.Business.DTOs
{
    public class MenuContentDTO
    {
        public Guid Id { get; set; }
        public string URL { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Layout { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
    }
}
