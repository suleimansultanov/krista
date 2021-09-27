using System;
using System.ComponentModel.DataAnnotations;

namespace KristaShop.WebUI.Models
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Наименование")]
        [RegularExpression("^[А-Яа-я_\\w\\s]*$", ErrorMessage = "Неверный формат: {0}")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public string Name { get; set; }
        [Display(Name = "Видимость")]
        public bool IsVisible { get; set; }
    }
}
