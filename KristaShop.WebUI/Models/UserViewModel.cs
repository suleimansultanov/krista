using KristaShop.Common.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace KristaShop.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Заполните поле {0}")]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Заполните поле {0}")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Заполните поле {0}")]
        [Display(Name = "ФИО")]
        public string ClientFullName { get; set; }

        [Display(Name = "Город")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public Guid CityId { get; set; }
        [Display(Name = "Название торгового центра")]
        public string ShopName { get; set; }
        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Электронная почта")]
        [EmailAddress(ErrorMessage = "Неправильно заполнено поле ")]
        public string Email { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class RegHashViewModel
    {
        public RegisterViewModel Reg { get; set; }
        public BaseHashModel Hash { get; set; }
    }
}
