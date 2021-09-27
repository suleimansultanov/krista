using System;
using System.ComponentModel.DataAnnotations;

namespace KristaShop.WebUI.Models
{
    public class MenuItemViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Тип меню")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public int MenuType { get; set; }
        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        [RegularExpression("^[А-Яа-я_\\w\\s]*$", ErrorMessage = "Неверный формат: {0}")]
        public string Title { get; set; }
        [Display(Name = "Контроллер")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public string ControllerName { get; set; }
        [Display(Name = "Экшен")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public string ActionName { get; set; }
        [Display(Name = "Алиас")]
        public string URL { get; set; }
        [Display(Name = "Параметры")]
        public string Params { get; set; }
        [Display(Name = "Иконка")]
        public string Icon { get; set; }
        [Display(Name = "Порядок")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public int Order { get; set; }
    }
}
