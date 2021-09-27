using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KristaShop.WebUI.Models
{
    public class NomViewModel
    {
        public Guid NomId { get; set; }
        public string Articul { get; set; }
        public string ItemName { get; set; }
        public string VideoUrl { get; set; }
        [Display(Name = "Цена по умолчанию")]
        public decimal DefaultPrice { get; set; }
        [Display(Name = "Картинка")]
        public IFormFile Image { get; set; }
        public IFormFileCollection Photos { get; set; }
        public string ImagePath { get; set; }
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public string Description { get; set; }
        [Display(Name = "Каталоги")]
        public List<Guid> Catalogs { get; set; }
        [Display(Name = "Категории")]
        public List<Guid> Categories { get; set; }
        public List<Guid> Clients { get; set; }
        [Display(Name = "Мета заголовок")]
        public string MetaTitle { get; set; }
        [Display(Name = "Название в ссылке")]
        public string LinkName { get; set; }
        [Display(Name = "Мета слова")]
        public string MetaKeywords { get; set; }
        [Display(Name = "Мета описание")]
        public string MetaDescription { get; set; }
        [Display(Name = "Видимость")]
        public bool IsVisible { get; set; }
    }
}
