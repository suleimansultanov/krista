using System;
using System.ComponentModel.DataAnnotations;

namespace KristaShop.WebUI.Models
{
    public class SettingCreateViewModel
    {
        [Display(Name = "Ключ")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public string Key { get; set; }
        [Display(Name = "Значение")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public string Value { get; set; }
    }

    public class SettingEditViewModel
    {
        [Display(Name = "№")]
        public Guid IdEdit { get; set; }
        [Display(Name = "Ключ")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public string KeyEdit { get; set; }
        [Display(Name = "Значение")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public string ValueEdit { get; set; }
    }
}
