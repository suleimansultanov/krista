using System.ComponentModel.DataAnnotations;

namespace KristaShop.Common.Enums
{
    public enum AccessControlLevel
    {
        [Display(Name = "Гость")]
        Guest = -1,
        [Display(Name = "Пользователь")]
        User = 5,
        [Display(Name = "Клиент")]
        Client = 10,
        [Display(Name = "Модератор")]
        Operator = 15,
        [Display(Name = "Менеджер")]
        Manager = 20,
        [Display(Name = "Администратор")]
        Admin = 30
    }
}
