using System;
using System.ComponentModel.DataAnnotations;

namespace KristaShop.WebUI.Models
{
    public class CatalogViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public string Name { get; set; }
        [Display(Name = "Форма заказа")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public int OrderForm { get; set; }
        [Display(Name = "ЧПУ")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public string Uri { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Мета-заглавие")]
        public string MetaTitle { get; set; }
        [Display(Name = "Мета-слова")]
        public string MetaKeywords { get; set; }
        [Display(Name = "Мета-описание")]
        public string MetaDescription { get; set; }
        [Display(Name = "Порядок")]
        public int Order { get; set; }
        [Display(Name = "Скидка")]
        public bool IsDiscount { get; set; }
        [Display(Name = "Видимость")]
        public bool IsVisible { get; set; }
    }
}
