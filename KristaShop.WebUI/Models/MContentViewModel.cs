using KristaShop.WebUI.Filters;
using System;
using System.ComponentModel.DataAnnotations;

namespace KristaShop.WebUI.Models
{
    public class MContentViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Алиас")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public string URL { get; set; }

        [Display(Name = "Заглавие")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        [RegularExpression("^[А-Яа-я_\\s]*$", ErrorMessage = "Неверный формат: {0}")]
        public string Title { get; set; }
        [Display(Name = "Тело")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public string Body { get; set; }
        [Display(Name = "Макет")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public string Layout { get; set; }
        [Display(Name = "Мета заглавие")]
        public string MetaTitle { get; set; }
        [Display(Name = "Мета описание")]
        public string MetaDescription { get; set; }
        [Display(Name = "Мета слова")]
        public string MetaKeywords { get; set; }
    }
}
